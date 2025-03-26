using System;
using Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectBallRow : MonoBehaviour
    {
        public TextMeshProUGUI ballName;
        public Image ballIcon;
        public GameObject selectText;
        public GameObject blackBg;
        public GameObject iconLock;
        public GameObject iconSelected;
        public Button selectButton;
        [HideInInspector]
        public SelectBallManager manager;
        [HideInInspector]
        public bool isUnlocked;
        [HideInInspector]
        public RewardType ballType;

        private void Start()
        {
            selectButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            manager.SelectBall(ballType);
        }

        public void SelectBallRowSettings(string title, Sprite sprite, bool isSelected, bool isUnlocked)
        {
            ballName.text = title;
            ballIcon.sprite = sprite;
            this.isUnlocked = isUnlocked;
            UpdateUI(isSelected);
        }

        public void UpdateUI(bool isSelected)
        {
            ResetUI();   
            if (isUnlocked)
            {
                selectButton.interactable = true;
                if (isSelected)
                {
                    blackBg.SetActive(true);
                    iconSelected.SetActive(true);
                }
                else
                {
                    selectText.gameObject.SetActive(true);
                }
            }
            else
            {
                selectButton.interactable = false;
                blackBg.SetActive(true);
                iconLock.SetActive(true);
            }
        }

        private void ResetUI()
        {
            iconLock.SetActive(false);
            iconSelected.SetActive(false);
            selectText.SetActive(false);
            blackBg.SetActive(false);
        }
    }
}
