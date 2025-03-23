using System.Collections.Generic;
using System.Linq;
using Quests;
using TigerForge;
using Newtonsoft.Json;
using UnityEngine;

namespace Managers
{
    [System.Serializable]
    public class Data
    {
        private int _maxScore;

        public int MaxScore
        {
            get => _maxScore;
            set => _maxScore = Mathf.Max(value, _maxScore);
        }

        private int _gamesPlayed;

        public int GamesPlayed
        {
            get => _gamesPlayed;
            set => _gamesPlayed++;
        }
        
        private int _maxCombo;

        public int MaxCombo
        {
            get => _maxCombo;
            set => _maxCombo = Mathf.Max(value, _maxCombo);
        }

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
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.GameOver, OnGameOver);
        }

        private void OnGameOver()
        {
            UpdateQuest("Play Games!", 1, true);
            var json = ToJson();
            Debug.Log(json);
        }

        public void UpdateQuest(string questName, int amount, bool isAdditive)
        {
            foreach (var quest in data.quests.Where(quest => quest.questName == questName && !quest.isCompleted))
            {
                quest.progress = isAdditive ? quest.progress += amount : Mathf.Max(quest.progress, amount);

                if (quest.progress >= quest.goal)
                {
                    quest.isCompleted = true;
                    GrantReward(quest.reward);
                }

                Debug.Log(quest.questName + " ilerleme: " + quest.progress + "/" + quest.goal);
            }
        }

        void GrantReward(string reward)
        {
            Debug.Log("Ödül kazandın: " + reward);
        }
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(data);
        }  

        public static Data FromJson(string json)  
        {  
            return JsonConvert.DeserializeObject<Data>(json);  
        } 

        private void OnApplicationQuit()
        {
            Debug.Log("Saved.");
        }
    }
}
