using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public BasketSpawner spawner;
    private int _currentLevel = 74;

    private void Start()
    {
        EventManager.StartListening(EventNames.Basket, OnBasket);
        LoadLevel();
    }

    private void OnBasket()
    {
        _currentLevel++;
        levelText.text = _currentLevel.ToString();
        CheckSpawnedPots();
        GameTimer.Instance.ResetTimer();
    }

    private void CheckSpawnedPots()
    {
        if (BasketSpawner.spawnedBaskets.Count == 0)
        {
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        int basketCount = 1; // Varsayılan 1 pota
        bool hasBomb = false;
        bool basketsMove = false;
        bool basketsDisappear = false;
        bool hasTimer = false;
        
        // Seviye kuralları
        if (_currentLevel >= 10) basketCount = 2;
        if (_currentLevel >= 10) hasBomb = true;
        if (_currentLevel >= 20) basketCount = 3;
        if (_currentLevel >= 30) basketsMove = true;
        if (_currentLevel >= 50) basketsDisappear = true;
        if (_currentLevel >= 75) hasTimer = true;

        spawner.SpawnPot(basketCount, basketsMove, basketsDisappear);
        
        if (hasTimer)
        {
            GameTimer.Instance.StartTimer();
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.Basket, OnBasket);
    }
}
