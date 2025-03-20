using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;

public class FireBarrierDifficulty : Difficulty
{
    private GameObject _fireBarrierPrefab;
    private GameObject _preparePrefab;
    private GameObject _barrier;
    private GameObject _prepareBarrier;
    private float _prepareTime;
    private float _barrierLength;
    private float _offset = 3f;
    private CancellationTokenSource _cts;

    public FireBarrierDifficulty(GameObject fireBarrierPrefab, GameObject preparePrefab, float prepareTime, float barrierLength)
    {
        _fireBarrierPrefab = fireBarrierPrefab;
        _preparePrefab = preparePrefab;
        _prepareTime = prepareTime;
        _barrierLength = barrierLength;
        EventManager.StartListening(EventNames.LevelUpgraded, OnLevelFinished);
    }
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        _cts = new CancellationTokenSource();
        BarrierAnimation(_cts.Token).Forget();
    }

    private void OnLevelFinished()
    {
        _cts.Cancel();
        GameObject.Destroy(_barrier);
        GameObject.Destroy(_prepareBarrier);
    }

    private async UniTask BarrierAnimation(CancellationToken token)
    {
        var x = CameraBounds.leftWallPos.x;
        var y = Random.Range(CameraBounds.topWallPos.y - _offset, CameraBounds.bottomWallPos.y + _offset);
        
        _prepareBarrier = GameObject.Instantiate(_preparePrefab, new Vector3(x, y, 0f), _preparePrefab.transform.rotation);
        _barrier = GameObject.Instantiate(_fireBarrierPrefab, new Vector3(x, y, 0f), _fireBarrierPrefab.transform.rotation);

        _prepareBarrier.SetActive(false);
        _barrier.SetActive(false);
        while (!token.IsCancellationRequested)
        {
            _prepareBarrier.SetActive(true);
            await UniTask.WaitForSeconds(_prepareTime, cancellationToken: token);
            _barrier.SetActive(true);
            _prepareBarrier.SetActive(false);
            await UniTask.WaitForSeconds(_barrierLength, cancellationToken: token);
            _barrier.SetActive(false);
        }
    }
}
