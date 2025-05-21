using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ShrinkingBasketsDifficulty : Difficulty
{
    private Vector3 _normalScale = new Vector3(0.55f, 0.55f, 0.5f);
    private Vector3 _shrinkedScale = new Vector3(0.3f, 0.3f, 0.27f);
    private CancellationTokenSource _cts;
    private List<GameObject> _baskets;
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        _cts = new CancellationTokenSource();
        _baskets = baskets;
        StartShrinking(_cts.Token).Forget();
    }

    private async UniTask StartShrinking(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitForSeconds(3f, cancellationToken: token);
            await ScaleBaskets(_shrinkedScale, 0.25f);
            await UniTask.WaitForSeconds(1f, cancellationToken: token);
            await ScaleBaskets(_normalScale, 0.75f);
        }
    }

    private async UniTask ScaleBaskets(Vector3 scale, float duration)
    {
        if (_baskets.Count == 0)
        {
            _cts.Cancel();
        }
        foreach (var basket in _baskets)
        {
            await basket.gameObject.transform.DOScale(scale, duration).ToUniTask();
        }
    }
}
