using System;
using System.Collections.Generic;
using Managers;
using Quests;
using UnityEngine;

namespace UI
{
    public class SelectBallManager : MonoBehaviour
    {
        public GameObject selectBallRow;
        public GameObject selectBallParent;
        private List<SelectBallRow> _rows;
        private void Start()
        {
            _rows = new List<SelectBallRow>();
            foreach (var quest in GameDataManager.instance.data.quests)
            {
                var row = Instantiate(selectBallRow, selectBallParent.transform).GetComponent<SelectBallRow>();
                var title = quest.rewardDisplayName;
                var icon = quest.rewardIcon;
                var isUnlocked = quest.isCompleted;
                var isSelected = GameDataManager.instance.data.selectedBall == quest.rewardType;
                
                row.SelectBallRowSettings(title, icon, isSelected, isUnlocked);
                row.manager = this;
                row.ballType = quest.rewardType;
                _rows.Add(row);
            }
        }

        public void SelectBall(RewardType ballType)
        {
            GameDataManager.instance.data.selectedBall = ballType;

            foreach (var row in _rows)
            {
                var isSelected = GameDataManager.instance.data.selectedBall == row.ballType;
                row.UpdateUI(isSelected);
            }
        }
    }
}
