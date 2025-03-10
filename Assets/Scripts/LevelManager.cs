using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public BasketSpawner spawner;
    private int _currentLevel = 9;

    private void Start()
    {
        EventManager.StartListening(EventNames.Basket, OnBasket);
        LoadLevel();
    }

    private void OnBasket()
    {
        _currentLevel++;
        levelText.text = _currentLevel.ToString();
        EventManager.EmitEventData(EventNames.LevelUpgraded, _currentLevel);
        CheckSpawnedPots();
        GameTimer.Instance.ResetTimer();
    }

    private void CheckSpawnedPots()
    {
        if (BasketSpawner.spawnedBaskets.Count == 0)
        {
            LoadLevel();
            EventManager.EmitEvent(EventNames.LevelLoaded);
        }
    }

    private void LoadLevel()
    {
        int basketCount = 1;
        
        if (_currentLevel >= 10) basketCount = 2;
        if (_currentLevel >= 20) basketCount = 3;

        spawner.SpawnPot(basketCount);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.Basket, OnBasket);
    }
}
