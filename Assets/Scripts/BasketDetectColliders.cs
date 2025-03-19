using System;
using UnityEngine;

public class BasketDetectColliders : MonoBehaviour
{
    private BasketCollider _bc;
    private void Start()
    {
        _bc = GetComponentInParent<BasketCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) { return; }
        
        _bc.TriggerEnter(gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) { return; }
        
        _bc.TriggerExit(gameObject.name);
    }
}
