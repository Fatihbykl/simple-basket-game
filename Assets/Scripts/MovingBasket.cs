using System;
using UnityEngine;

public class MovingBasket : MonoBehaviour
{
    public float speed = 2f;
    private float _direction = 1f;

    void Update()
    {
        transform.position += new Vector3(_direction * speed * Time.deltaTime, 0, 0);

        if (transform.position.x > CameraBounds.rightWallPos.x || transform.position.x < CameraBounds.leftWallPos.x)
        {
            _direction *= -1f;
        }
    }
}
