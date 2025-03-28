using TigerForge;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public SoundClip basketSound;
        public SoundClip perfectBasketSound;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI gameOverscoreText;
        public TextMeshProUGUI gameOverMaxScoreText;
        public int baseScore;
        public float maxComboTime;
        [HideInInspector] public int scoreMultiplier = 1;
        private int _combo;
        private int _maxCombo;
        private float _lastComboTime;
        private int _score;

        private void Start()
        {
            EventManager.StartListening(EventNames.Score, AddScore);
            EventManager.StartListening(EventNames.Combo, AddCombo);
            EventManager.StartListening(EventNames.NoHealth, OnNoHealth);
            EventManager.StartListening(EventNames.GameOver, OnGameOver);
        }

        private void OnNoHealth()
        {
            GameDataManager.instance.data.BestScore = _score;
            GameDataManager.instance.data.BestCombo = _maxCombo;
            GameDataManager.instance.data.TotalScore = _score;
        }

        private void OnGameOver()
        {
            gameOverscoreText.text = _score.ToString();
            gameOverMaxScoreText.text = GameDataManager.instance.data.BestScore.ToString();
        }

        private void AddScore()
        {
            var isPerfect = EventManager.GetBool(EventNames.Score);
            var comboBonus = _combo * 2;
            var perfectBonus = isPerfect ? 5 : 0;

            var totalScore = baseScore + comboBonus + perfectBonus;
            _score += totalScore * scoreMultiplier;

            if (isPerfect)
            {
                var sender = (GameObject)EventManager.GetSender(EventNames.Score);
                SoundManager.Instance.PlaySoundFXClip(perfectBasketSound, sender.transform);
            }
        
            UpdateScoreText();
        }

        private void AddCombo()
        {
            if (Time.time - _lastComboTime > maxComboTime)
            {
                _maxCombo = Mathf.Max(_combo, _maxCombo);
                _combo = 0;
            }
            _combo++;
            _lastComboTime = Time.time;

            var sender = (GameObject)EventManager.GetSender(EventNames.Combo);
            if (_combo >= 2)
            {
                CreateFloatingText(sender.transform.position);
            }
            var pitchMultiplier = 1f + (_combo * 0.05f);
            SoundManager.Instance.PlaySoundFXClip(basketSound, sender.transform, pitchMultiplier);
        }
    
        private void CreateFloatingText(Vector3 position)
        {
            var destination = position;
            destination.x += (Random.value - 0.5f) / 3f;
            destination.y += Random.value;
            destination.z += (Random.value - 0.5f) / 3f;
        
            DynamicTextManager.CreateText(destination, $"COMBO x{_combo.ToString()}", DynamicTextManager.basketData);
        }

        private void UpdateScoreText()
        {
            scoreText.text = _score.ToString();
        } 
    
        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.Score, AddScore);
            EventManager.StopListening(EventNames.Combo, AddCombo);
        }
    }
}
