using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DisappearingBasket : MonoBehaviour
{
    public float disappearDelay = 5f;
    private void Start()
    {
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(disappearDelay);
            transform.position = GetRandomPosition();
        }
    }
    
    private static Vector3 GetRandomPosition()
    {
        var x = Random.Range(CameraBounds.leftWallPos.x + 1f, CameraBounds.rightWallPos.x - 1f);
        var y = Random.Range(CameraBounds.bottomWallPos.y + 1f, CameraBounds.topWallPos.y - 1f);
        return new Vector3(x, y, 0);
    }
}
