using System;
using Cysharp.Threading.Tasks;
using TigerForge;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public BasketSpawner spawner;
    [SerializeField] private int _currentLevel = 92;

    private void Start()
    {
        EventManager.StartListening(EventNames.Basket, OnBasket);
        LoadLevel();
    }

    private void OnBasket()
    {
        LoadLevel();
    }

    private async UniTask LoadLevel()
    {
        Debug.Log(BasketSpawner.spawnedBaskets.Count);
        if (BasketSpawner.spawnedBaskets.Count != 0) { return; }

        _currentLevel++;
        EventManager.EmitEventData(EventNames.LevelUpgraded, _currentLevel);
        
        int basketCount = 1;
        
        if (_currentLevel >= 10) basketCount = 2;
        if (_currentLevel >= 20) basketCount = 3;

        await spawner.SpawnPot(basketCount);
        EventManager.EmitEvent(EventNames.LevelLoaded);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.Basket, OnBasket);
    }
}
