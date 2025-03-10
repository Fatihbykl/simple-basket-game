using System;
using TigerForge;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Boom");
            EventManager.EmitEvent(EventNames.BombExploded);
        }
    }
}
