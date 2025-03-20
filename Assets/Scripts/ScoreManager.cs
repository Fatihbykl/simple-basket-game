using System;
using TigerForge;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int baseScore;
    public float maxComboTime;
    private int _score = 0;
    private int _combo = 0;
    private float _lastComboTime = 0;

    private void Start()
    {
        EventManager.StartListening(EventNames.Score, AddScore);
        EventManager.StartListening(EventNames.Combo, AddCombo);
    }

    private void AddScore()
    {
        var isPerfect = EventManager.GetBool("isPerfect");
        var comboBonus = _combo * 2;
        var perfectBonus = isPerfect ? 5 : 0;

        var totalScore = baseScore + comboBonus + perfectBonus;
        _score += totalScore;

        UpdateScoreText();
        Debug.Log($"🏀 Skor: {_score} (Combo: {_combo}, Perfect: {isPerfect})");
    }

    private void AddCombo()
    {
        if (Time.time - _lastComboTime > maxComboTime)
        {
            _combo = 0;
        }
        _combo++;
        _lastComboTime = Time.time;

        if (_combo >= 2)
        {
            var sender = (GameObject)EventManager.GetSender(EventNames.Combo);
            CreateFloatingText(sender.transform.position);
        }
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
