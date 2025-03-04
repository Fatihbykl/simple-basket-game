using System;
using TigerForge;
using UnityEngine;

public class BasketCollider : MonoBehaviour
{
    private bool _isCollided;
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) { return; }
        if (_isCollided) { return; }

        BasketSpawner.spawnedBaskets.Remove(transform.parent.gameObject);
        EventManager.EmitEvent(EventNames.Basket);
        _isCollided = true;
        Destroy(transform.parent.gameObject);
    }
}
