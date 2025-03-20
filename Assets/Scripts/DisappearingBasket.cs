using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class DisappearingBasket : MonoBehaviour
{
    public float disappearDelay = 5f;
    private GameObject _disappearEffect;
    private Vector3 _normalScale = new Vector3(0.55f, 0.55f, 0.5f);

    public void StartDisappearingBasket(GameObject effectPrefab)
    {
        _disappearEffect = Instantiate(effectPrefab);
        _disappearEffect.SetActive(false);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(disappearDelay);
            transform.DOScale(0f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            var pos = GetRandomPosition();
            transform.position = pos;
            _disappearEffect.transform.position = pos;
            transform.DOScale(_normalScale, 0.2f);
            _disappearEffect.SetActive(true);
        }
    }
    
    private static Vector3 GetRandomPosition()
    {
        var x = Random.Range(CameraBounds.leftWallPos.x + 1f, CameraBounds.rightWallPos.x - 1f);
        var y = Random.Range(CameraBounds.bottomWallPos.y + 1f, CameraBounds.topWallPos.y - 1f);
        return new Vector3(x, y, 0);
    }

    private void OnDestroy()
    {
        Destroy(_disappearEffect);
    }
}
