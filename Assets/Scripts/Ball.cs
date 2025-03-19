using System;
using TigerForge;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem bombCollideEffect;
    public ParticleSystem rocketCollideEffect;
    private Rigidbody _rb;

    private void Start()
    {
        EventManager.StartListening(EventNames.BombExploded, OnBombExploded);
        EventManager.StartListening(EventNames.RocketCollided, OnRocketCollided);

        _rb = GetComponent<Rigidbody>();
    }

    public void ResetBallPosition()
    {
        transform.gameObject.transform.position = new Vector3(0, 4f, 0);
        _rb.linearVelocity = Vector3.zero;
    }

    private void OnBombExploded()
    {
        bombCollideEffect.transform.position = new Vector3(transform.position.x, transform.position.y, 2f);
        bombCollideEffect.gameObject.SetActive(true);
    }

    private void OnRocketCollided()
    {
        rocketCollideEffect.transform.position = new Vector3(transform.position.x, transform.position.y, 2f);
        rocketCollideEffect.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.BombExploded, OnBombExploded);
        EventManager.StopListening(EventNames.RocketCollided, OnRocketCollided);
    }
}
