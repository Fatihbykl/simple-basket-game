using System;
using TigerForge;
using UnityEngine;

public class BasketCollider : MonoBehaviour
{
    public GameObject bomb;
    private bool _isCollided;
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) { return; }
        if (_isCollided) { return; }

        BasketSpawner.spawnedBaskets.Remove(transform.parent.gameObject);
        EventManager.EmitEvent(EventNames.Basket);
        _isCollided = true;
        GetComponentInParent<Animator>().SetTrigger("Die");
        Destroy(transform.parent.gameObject, 1f);

        if (bomb)
        {
            bomb.GetComponent<Animator>().SetTrigger("Die");
            Destroy(bomb, 1f);
        }
    }
}
