using System;
using TigerForge;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EventManager.EmitEventData(EventNames.BombExploded, gameObject.transform.position);
            EventManager.EmitEvent(EventNames.BombExploded);
            Destroy(gameObject);
        }
    }
}
