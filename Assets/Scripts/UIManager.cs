using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameOverTransparent;

    private void Start()
    {
        EventManager.StartListening(EventNames.GameOver, OnGameOver);
        EventManager.StartListening(EventNames.Revived, OnRevived);
    }

    private void OnRevived()
    {
        AnimateScreenMoveOut();
    }

    private void OnGameOver()
    {
        AnimateScreenMoveIn();
    }

    private async UniTask AnimateScreenMoveIn()
    {
        await UniTask.WaitForSeconds(1f, true);
        await gameOverScreen.transform.DOLocalMoveX(0f, 0.5f).SetUpdate(true).ToUniTask();
        await gameOverTransparent.GetComponent<Image>().DOFade(200f / 255f, 0.5f).SetUpdate(true).ToUniTask();
    }
    
    private async UniTask AnimateScreenMoveOut()
    {
        await gameOverScreen.transform.DOLocalMoveX(-1500f, 0.5f).SetUpdate(true).ToUniTask();
        await gameOverTransparent.GetComponent<Image>().DOFade(0f, 0.5f).SetUpdate(true).ToUniTask();
    }
}
