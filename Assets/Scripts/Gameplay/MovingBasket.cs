using System;
using UnityEngine;

public class MovingBasket : MonoBehaviour
{
    public float speed = 2f;
    private float _direction = 1f;
    private float _lastDirectionChange;

    void Update()
    {
        transform.position += new Vector3(_direction * speed * Time.deltaTime, 0, 0);

        if (Time.time - _lastDirectionChange < 0.5f)
        {
            return;
        }
        if (transform.position.x > CameraBounds.rightWallPos.x || transform.position.x < CameraBounds.leftWallPos.x)
        {
            _direction *= -1f;
            _lastDirectionChange = Time.time;
        }
    }
}
