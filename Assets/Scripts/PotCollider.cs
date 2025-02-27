using System;
using TigerForge;
using UnityEngine;

public class PotCollider : MonoBehaviour
{
    private bool _isCollided;
    private void OnTriggerExit(Collider other)
    {
        if (_isCollided) { return; }

        PotSpawner.spawnedPots.Remove(transform.parent.gameObject);
        EventManager.EmitEvent(EventNames.Basket);
        _isCollided = true;
        Destroy(transform.parent.gameObject);
    }
}
