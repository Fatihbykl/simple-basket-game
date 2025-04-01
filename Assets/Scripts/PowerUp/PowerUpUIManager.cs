using System;
using Ads;
using Gameplay;
using Managers;
using TigerForge;
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
        public TinyBallPowerUp tinyBallPowerUp;
        public GameObject[] powerUpButtonIcons;
        public GameObject[] durations;
        private PowerUp[] _powerUps;
        private float _lastUpdate;

        private void Start()
        {
            EventManager.StartListening(EventNames.AddShield, OnAddShield);
            EventManager.StartListening(EventNames.AddDoubleScore, OnAddDoubleScore);
            EventManager.StartListening(EventNames.AddTinyBall, OnAddTinyBall);

            _powerUps = new PowerUp[] { shieldPowerUp, doubleScorePowerUp, tinyBallPowerUp };
            for (var i = 0; i < powerUpButtonIcons.Length; i++)
            {
                var buttonIcon = powerUpButtonIcons[i];
                var count = buttonIcon.transform.GetChild(0);
                var adIcon = buttonIcon.transform.GetChild(1);
                var powerUp = _powerUps[i];
                var index = i;

                buttonIcon.GetComponent<Button>().onClick
                    .AddListener(() => powerUp.Activate(new SkillContext(ball, scoreManager)));
                buttonIcon.GetComponent<Button>().onClick.AddListener(() => UpdateUI(index));
                buttonIcon.GetComponent<Image>().sprite = powerUp.icon;
                buttonIcon.GetComponent<Image>().preserveAspect = true;
                count.GetComponent<TextMeshProUGUI>().text = powerUp.count.ToString();
                adIcon.gameObject.SetActive(powerUp.count == 0);
            }
        }

        private void Update()
        {
            if (Time.time - _lastUpdate < 1f)
            {
                return;
            }

            for (var index = 0; index < durations.Length; index++)
            {
                var powerUp = _powerUps[index];
                var duration = durations[index];

                if (!powerUp.isActive)
                {
                    duration.SetActive(false);
                    continue;
                }

                duration.GetComponent<Image>().fillAmount = (float)powerUp.remainingTime / powerUp.duration;
                duration.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = powerUp.remainingTime.ToString();
            }
            _lastUpdate = Time.time;
        }

        private void UpdateUI(int index)
        {
            var powerUp = _powerUps[index];
            if (powerUp.count == 0 && !powerUp.isActive)
            {
                switch (index)
                {
                    case 0:
                        AdManager.instance.ShieldAd();
                        break;
                    case 1:
                        AdManager.instance.DoubleScoreAd();
                        break;
                    case 2:
                        AdManager.instance.TinyBallAd();
                        break;
                }
                return;
            }
            
            durations[index].SetActive(true);
            var buttonIcon = powerUpButtonIcons[index];
            var count = buttonIcon.transform.GetChild(0);
            var adIcon = buttonIcon.transform.GetChild(1);

            count.GetComponent<TextMeshProUGUI>().text = powerUp.count.ToString();
            adIcon.gameObject.SetActive(powerUp.count == 0);
        }

        private void OnAddShield()
        {
            _powerUps[0].count++;
            UpdateUI(0);
        }

        private void OnAddDoubleScore()
        {
            _powerUps[1].count++;
            UpdateUI(1);
        }

        private void OnAddTinyBall()
        {
            _powerUps[2].count++;
            UpdateUI(2);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.AddShield, OnAddShield);
            EventManager.StopListening(EventNames.AddDoubleScore, OnAddDoubleScore);
            EventManager.StopListening(EventNames.AddTinyBall, OnAddTinyBall);
        }
    }
}