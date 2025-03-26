using System;
using Managers;
using UnityEngine;

namespace UI
{
    public class QuestUIManager : MonoBehaviour
    {
        public GameObject questPanel;
        public GameObject questParent;

        private void Start()
        {
            var quests = GameDataManager.instance.data.quests;
            foreach (var quest in quests)
            {
                var panel = Instantiate(questPanel, questParent.transform).GetComponent<QuestRow>();
                panel.rewardName.text = quest.rewardDisplayName;
                panel.rewardIcon.sprite = quest.rewardIcon;
                panel.description.text = quest.description;
                panel.progressSlider.value = (float)quest.progress / quest.goal;
                panel.progressText.text = quest.progress.ToString();
                if (quest.isCompleted)
                {
                    panel.sliderFill.sprite = panel.sliderFillGreen;
                }
            }
        }
    }
}
