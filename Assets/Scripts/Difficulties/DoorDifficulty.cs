using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;

public class DoorDifficulty : Difficulty
{
    private GameObject _barrierPrefab;
    private GameObject _barrier;
    private float _offset = 3f;
    private CancellationTokenSource _cts;

    public DoorDifficulty(GameObject barrier)
    {
        _barrierPrefab = barrier;
        EventManager.StartListening(EventNames.LevelUpgraded, OnLevelFinished);
    }
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        _cts = new CancellationTokenSource();
        var x = CameraBounds.leftWallPos.x;
        var y = Random.Range(CameraBounds.topWallPos.y - _offset, CameraBounds.bottomWallPos.y + _offset);
        _barrier = GameObject.Instantiate(_barrierPrefab, new Vector3(x, y, 0f), _barrierPrefab.transform.rotation);
        BarrierAnimation(_cts.Token).Forget();
    }

    private void OnLevelFinished()
    {
        _cts.Cancel();
        GameObject.Destroy(_barrier);
    }

    private async UniTask BarrierAnimation(CancellationToken token)
    {
        var targetX = CameraBounds.rightWallPos.x - CameraBounds.leftWallPos.x + 0.5f;
        while (!token.IsCancellationRequested)
        {
            await _barrier.transform.DOScaleX(targetX, 0.5f).ToUniTask(cancellationToken: token);
            await UniTask.WaitForSeconds(2f, cancellationToken: token);
            await _barrier.transform.DOScaleX(0f, 1f).ToUniTask(cancellationToken: token);
            await UniTask.WaitForSeconds(2f, cancellationToken: token);
        }
    }
}
