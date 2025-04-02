using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Managers;
using TigerForge;
using UnityEngine;

namespace PlayGamesServices
{
    public class PlayGamesManager : MonoBehaviour
    {
#if UNITY_ANDROID
        public static PlayGamesManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializePlayGames();
        }

        private void InitializePlayGames()
        {
            PlayGamesPlatform.Activate();

            PlayGamesPlatform.Instance.localUser.Authenticate(success =>
            {
                if (success)
                {
                    LoadData();
                }
                else
                {
                    Debug.LogError("Failed to Authenticate Google Play");
                }
            });
        }

        private void LoadData()
        {
            Data myData = null;
            if (!PlayGamesPlatform.Instance.localUser.authenticated)
            {
                Debug.LogError("User not logged in!");
                EventManager.EmitEventData(EventNames.DataLoaded, myData);
                return;
            }

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            savedGameClient.OpenWithAutomaticConflictResolution("player_data",
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                (status, metadata) =>
                {
                    if (status == SavedGameRequestStatus.Success)
                    {
                        savedGameClient.ReadBinaryData(metadata, (readStatus, data) =>
                        {
                            if (readStatus == SavedGameRequestStatus.Success)
                            {
                                if (data.Length == 0)
                                {
                                    SaveData("{}");
                                    EventManager.EmitEventData(EventNames.DataLoaded, myData);
                                    return;
                                }

                                string json = Encoding.UTF8.GetString(data);
                                Debug.Log("Saved data loaded " + json);

                                myData = JsonUtility.FromJson<Data>(json);
                                Debug.Log(myData);
                                EventManager.EmitEventData(EventNames.DataLoaded, myData);
                            }
                            else
                            {
                                Debug.LogError("Failed to read data");
                            }
                        });
                    }
                    else
                    {
                        Debug.LogError("Failed to open saved games");
                    }
                });
        }

        public void SaveData(string jsonData)
        {
            if (!PlayGamesPlatform.Instance.localUser.authenticated)
            {
                Debug.LogError("User not logged in!");
                return;
            }

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            savedGameClient.OpenWithAutomaticConflictResolution("player_data",
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                (status, metadata) =>
                {
                    if (status == SavedGameRequestStatus.Success)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(jsonData);
                        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder()
                            .WithUpdatedDescription("Last save date: " + System.DateTime.Now)
                            .Build();

                        savedGameClient.CommitUpdate(metadata, update, data, (commitStatus, meta) =>
                        {
                            if (commitStatus == SavedGameRequestStatus.Success)
                            {
                                Debug.Log("Data updated");
                            }
                            else
                            {
                                Debug.LogError("Failed to write data");
                            }
                        });
                    }
                    else
                    {
                        Debug.LogError("Failed to open saved games");
                    }
                });
        }

        public void SubmitScore(int score)
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportScore(score, Keys.LEADERBOARD_ID, (bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("Skor başarıyla gönderildi!");
                    }
                    else
                    {
                        Debug.Log("Skor gönderme başarısız.");
                    }
                });
            }
        }

        public void ShowLeaderboard()
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI(Keys.LEADERBOARD_ID);
            }
        }
#endif
        
    }
}