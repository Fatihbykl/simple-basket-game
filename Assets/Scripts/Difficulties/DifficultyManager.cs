using System;
using System.Collections.Generic;
using System.Linq;
using Difficulties;
using Gameplay;
using Managers;
using TigerForge;
using UnityEngine;
using Random = UnityEngine.Random;

public class DifficultyManager : MonoBehaviour
{
    public RocketManager rocketManager;
    public GameObject bombPrefab;
    public SoundClip bombLoop;
    public GameObject fireBarrierPrefab;
    public GameObject barrierPreparePrefab;
    public float barrierPrepareTime;
    public float barrierFireTime;
    public GameObject wheelPrefab;
    public GameObject disappearEffectPrefab;
    private List<Difficulty> _availableDifficulties = new List<Difficulty>();
    private Difficulty _activeRotation;
    private int _level;

    private void Start()
    {
        EventManager.StartListening(EventNames.LevelUpgraded, OnLevelUpgrade);
        EventManager.StartListening(EventNames.LevelLoaded, OnLevelLoaded);
    }

    private void OnLevelUpgrade()
    {
        _level = EventManager.GetInt(EventNames.LevelUpgraded);
        GameTimer.Instance.ResetTimer();
        
        CheckDifficulties();
    }

    private void OnLevelLoaded()
    {
        PickDifficulty();
    }

    private void PickDifficulty()
    {
        _activeRotation?.ApplyDifficulty(BasketSpawner.spawnedBaskets);
        if (_availableDifficulties.Count == 0)
        {
            return;
        }
        if (_level < 100)
        {
            _availableDifficulties.Last().ApplyDifficulty(BasketSpawner.spawnedBaskets);
        }
        else
        {
            _availableDifficulties[Random.Range(0, _availableDifficulties.Count)].ApplyDifficulty(BasketSpawner.spawnedBaskets);
        }
    }

    private void CheckDifficulties()
    {
        switch (_level)
        {
            case 3:
                _availableDifficulties.Add(new BombDifficulty(bombPrefab));
                _activeRotation = new RotationDifficulty();
                rocketManager.ActivateRocket();
                break;
            case 6:
                _availableDifficulties.Add(new MovingDifficulty());
                break;
            case 9:
                _availableDifficulties.Add(new DisappearDifficulty(disappearEffectPrefab));
                break;
            case 12:
                _availableDifficulties.Add(new WheelDifficulty(wheelPrefab));
                break;
            case 15:
                _availableDifficulties.Add(new ShrinkingBasketsDifficulty());
                break;
            case 18:
                _availableDifficulties.Add(new FireBarrierDifficulty(fireBarrierPrefab, barrierPreparePrefab, barrierPrepareTime, barrierFireTime));
                break;
            case 21:
                GameTimer.Instance.StartTimer();
                break;
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.LevelUpgraded, OnLevelUpgrade);
        EventManager.StopListening(EventNames.LevelLoaded, OnLevelLoaded);
    }
}
