using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Quests;
using TigerForge;
using Newtonsoft.Json;
using PlayGamesServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    [System.Serializable]
    public class Data
    {
        private int _totalScore;
        
        public int TotalScore
        {
            get => _totalScore;
            set => _totalScore += value;
        }
        
        private int _bestScore;

        public int BestScore
        {
            get => _bestScore;
            set
            {
                if (_bestScore >= value) { return; }
                _bestScore = value;
                #if UNITY_ANDROID
                PlayGamesManager.instance.SubmitScore(_bestScore);
                #endif
            }
        }

        private int _gamesPlayed;

        public int GamesPlayed
        {
            get => _gamesPlayed;
            set => _gamesPlayed++;
        }

        private int _bestCombo;

        public int BestCombo
        {
            get => _bestCombo;
            set => _bestCombo = Mathf.Max(value, _bestCombo);
        }

        public RewardType selectedBall = RewardType.Default;
        public List<Quest> quests;
    }
    public class GameDataManager : MonoBehaviour
    {
        public Data data;

        [JsonIgnore]
        public static GameDataManager instance;

        private void Awake()
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
            EventManager.StartListening(EventNames.GameOver, OnGameOver);
            EventManager.StartListening(EventNames.DataLoaded, OnDataLoaded);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.GameOver, OnGameOver);
            EventManager.StopListening(EventNames.DataLoaded, OnDataLoaded);
        }

        private void OnDataLoaded()
        {
            Data loadedData = (Data)EventManager.GetData(EventNames.DataLoaded);
            Debug.Log("Unity: Loading data");
            Debug.Log(loadedData);
            if (loadedData == null) { return; }
            
            data.BestScore = loadedData.BestScore;
            data.TotalScore = loadedData.TotalScore;
            data.BestCombo = loadedData.BestCombo;
            data.GamesPlayed = loadedData.GamesPlayed;
            data.selectedBall = loadedData.selectedBall;

            foreach (var quest in loadedData.quests)
            {
                var q = data.quests.FirstOrDefault(q => q.rewardType == quest.rewardType);
                q.progress = quest.progress;
                q.isCompleted = quest.isCompleted;
            }
        }

        private void OnGameOver()
        {
            UpdateQuests();
#if UNITY_ANDROID
            PlayGamesManager.instance.SaveData(ToJson());
#endif
        }

        public void UpdateQuests()
        {
            foreach (var quest in data.quests)
            {
                if (quest.isCompleted){ continue; }
                
                quest.progress = quest.target switch
                {
                    TargetData.BestScore => data.BestScore,
                    TargetData.BestCombo => data.BestCombo,
                    TargetData.TotalScore => data.TotalScore,
                    TargetData.GameCount => data.GamesPlayed,
                    _ => quest.progress
                };
                
                if (quest.progress >= quest.goal)
                {
                    quest.isCompleted = true;
                }

                Debug.Log(quest.rewardDisplayName + " ilerleme: " + quest.progress + "/" + quest.goal);
            }
        }
        
        private string ToJson()
        {
            return JsonConvert.SerializeObject(data);
        }  
    }
}
