using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public PotSpawner spawner;
    private int _currentLevel;

    private void Start()
    {
        EventManager.StartListening(EventNames.Basket, OnBasket);
        spawner.SpawnPot();
    }

    private void OnBasket()
    {
        _currentLevel++;
        levelText.text = _currentLevel.ToString();
        CheckSpawnedPots();
    }

    private void CheckSpawnedPots()
    {
        if (PotSpawner.spawnedPots.Count == 0)
        {
            spawner.SpawnPot();
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.Basket, OnBasket);
    }
}
