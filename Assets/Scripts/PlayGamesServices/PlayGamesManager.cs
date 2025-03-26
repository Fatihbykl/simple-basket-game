using GooglePlayGames;
using UnityEngine;

namespace PlayGamesServices
{
    public class PlayGamesManager : MonoBehaviour
    {
        private static PlayGamesManager instance;

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
        
        void Start()
        {
            PlayGamesPlatform.Activate();
            SignIn();
        }

        void SignIn()
        {
            PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
                if (success)
                {
                    Debug.Log("Google Play Hizmetlerine giriş başarılı!");
                }
                else
                {
                    Debug.Log("Google Play Hizmetlerine giriş başarısız.");
                }
            });
        }
    
        public void SubmitScore(int score)
        {
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportScore(score, Keys.LEADERBOARD_ID, (bool success) => {
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


    }
}
