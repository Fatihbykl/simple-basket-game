using System;
using TigerForge;
using UnityEngine;

public class GearWheel : MonoBehaviour
{
    private float _wheelRotationSpeed = 400f;
    private float _bounceForce = 10f;

    private void Start()
    {
        EventManager.StartListening(EventNames.Basket, OnLevelUpgraded);
    }

    void Update()
    {
        gameObject.transform.Rotate(0f, 0f,_wheelRotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            var ballRb = other.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                Vector2 hitDirection = (other.transform.position - transform.position).normalized;

                ballRb.AddForce(hitDirection * _bounceForce, ForceMode.Impulse);
            }
        }
    }

    private void OnLevelUpgraded()
    {
        Destroy(gameObject);
    }
}
