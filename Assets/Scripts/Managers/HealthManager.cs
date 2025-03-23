using TigerForge;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private int health;
        private Ball _ball;

        private void Start()
        {
            EventManager.StartListening(EventNames.BallFell, OnGetDamage);
            EventManager.StartListening(EventNames.BombExploded, OnGetDamage);
            EventManager.StartListening(EventNames.RocketCollided, OnGetDamage);
            EventManager.StartListening(EventNames.fireBarrierCollided, OnGetDamage);
            EventManager.StartListening(EventNames.Revived, OnRevived);
            UpdateHealthUI();
            _ball = GetComponent<Ball>();
        }

        private void OnGetDamage()
        {
            health--;
            if (health <= 0)
            {
                EventManager.EmitEvent(EventNames.NoHealth);
                UpdateHealthUI();
                return;
            }
        
            _ball.ResetBallPosition();
        }

        private void OnRevived()
        {
            var healthAmount = EventManager.GetInt(EventNames.Revived);
            health += healthAmount;
            _ball.ResetBallPosition();
            UpdateHealthUI();
        }

        private void UpdateHealthUI()
        {
            healthText.text = health.ToString();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.BallFell, OnGetDamage);
            EventManager.StopListening(EventNames.BombExploded, OnGetDamage);
            EventManager.StopListening(EventNames.RocketCollided, OnGetDamage);
            EventManager.StopListening(EventNames.fireBarrierCollided, OnGetDamage);
            EventManager.StopListening(EventNames.Revived, OnRevived);
        }
    }
}
