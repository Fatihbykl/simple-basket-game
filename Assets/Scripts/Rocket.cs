using System;
using TigerForge;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EventManager.EmitEventData(EventNames.RocketCollided, gameObject.transform.position);
            EventManager.EmitEvent(EventNames.RocketCollided);
            transform.position = new Vector3(0f, 100f, 0f);
        }
    }
}
