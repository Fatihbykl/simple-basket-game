using System;
using Gameplay;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUp
{
    public class PowerUpUIManager : MonoBehaviour
    {
        public Ball ball;
        public ScoreManager scoreManager;
        public ShieldPowerUp shieldPowerUp;
        public DoubleScorePowerUp doubleScorePowerUp;
        public GameObject[] powerUpButtonIcons;
        private PowerUp[] _powerUps;

        private void Start()
        {
            _powerUps = new PowerUp[] {shieldPowerUp, doubleScorePowerUp};
            for (var i = 0; i < powerUpButtonIcons.Length; i++)
            {
                var buttonIcon = powerUpButtonIcons[i];
                var count = buttonIcon.transform.GetChild(0);
                var adIcon = buttonIcon.transform.GetChild(1);
                var powerUp = _powerUps[i];
                
                buttonIcon.GetComponent<Button>().onClick.AddListener(() => powerUp.Activate(new SkillContext(ball, scoreManager)));
                buttonIcon.GetComponent<Button>().onClick.AddListener(() => UpdateUI(i));
                buttonIcon.GetComponent<Image>().sprite = powerUp.icon;
                count.GetComponent<TextMeshProUGUI>().text = powerUp.count.ToString();
                adIcon.gameObject.SetActive(powerUp.count == 0);
            }
        }

        private void UpdateUI(int index)
        {
            var buttonIcon = powerUpButtonIcons[index];
            var count = buttonIcon.transform.GetChild(0);
            var adIcon = buttonIcon.transform.GetChild(1);
            var powerUp = _powerUps[index];
            
            count.GetComponent<TextMeshProUGUI>().text = powerUp.count.ToString();
            adIcon.gameObject.SetActive(powerUp.count == 0);
        }
    }
}
