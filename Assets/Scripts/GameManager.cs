using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.StartListening(EventNames.GameOver, OnGameOver);
        EventManager.StartListening(EventNames.Revived, OnRevived);
    }

    private void OnGameOver()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, 1f).SetUpdate(true);
    }

    private void OnRevived()
    {
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.GameOver, OnGameOver);
        EventManager.StopListening(EventNames.Revived, OnRevived);
    }
}
