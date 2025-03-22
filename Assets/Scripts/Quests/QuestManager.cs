using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public List<Quest> quests = new List<Quest>();

        void Start()
        {
            LoadQuests();
        }

        void LoadQuests()
        {
            foreach (var quest in quests)
            {
                quest.progress = PlayerPrefs.GetInt(quest.questName + "_progress", 0);
                quest.isCompleted = PlayerPrefs.GetInt(quest.questName + "_completed", 0) == 1;
            }
        }

        public void UpdateQuest(string questName, int amount, bool isAdditive)
        {
            foreach (var quest in quests.Where(quest => quest.questName == questName && !quest.isCompleted))
            {
                quest.progress = isAdditive ? quest.progress += amount : Mathf.Max(quest.progress, amount);

                if (quest.progress >= quest.goal)
                {
                    quest.isCompleted = true;
                    GrantReward(quest.reward);
                }

                PlayerPrefs.SetInt(quest.questName + "_progress", quest.progress);
                PlayerPrefs.SetInt(quest.questName + "_completed", quest.isCompleted ? 1 : 0);
                PlayerPrefs.Save();

                Debug.Log(quest.questName + " ilerleme: " + quest.progress + "/" + quest.goal);
            }
        }

        void GrantReward(string reward)
        {
            Debug.Log("Ödül kazandın: " + reward);
            PlayerPrefs.SetInt("Has_" + reward, 1);
            PlayerPrefs.Save();
        }
    }
}