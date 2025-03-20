using System;
using TigerForge;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem bombCollideEffect;
    public ParticleSystem rocketCollideEffect;
    public ParticleSystem fireBarrierCollideEffect;
    private Rigidbody _rb;

    private void Start()
    {
        EventManager.StartListening(EventNames.BombExploded, OnBombExploded);
        EventManager.StartListening(EventNames.RocketCollided, OnRocketCollided);
        EventManager.StartListening(EventNames.fireBarrierCollided, OnFireBarrierCollided);

        _rb = GetComponent<Rigidbody>();
    }

    public void ResetBallPosition()
    {
        transform.gameObject.transform.position = new Vector3(0, 4f, 0);
        _rb.linearVelocity = Vector3.zero;
    }

    private void OnBombExploded()
    {
        var pos = (Vector3)EventManager.GetData(EventNames.BombExploded);
        bombCollideEffect.transform.position = pos;
        bombCollideEffect.gameObject.SetActive(true);
    }

    private void OnRocketCollided()
    {
        var pos = (Vector3)EventManager.GetData(EventNames.RocketCollided);
        rocketCollideEffect.transform.position = pos;
        rocketCollideEffect.gameObject.SetActive(true);
    }

    private void OnFireBarrierCollided()
    {
        var pos = (Vector3)EventManager.GetData(EventNames.fireBarrierCollided);
        fireBarrierCollideEffect.transform.position = pos;
        fireBarrierCollideEffect.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.BombExploded, OnBombExploded);
        EventManager.StopListening(EventNames.RocketCollided, OnRocketCollided);
        EventManager.StopListening(EventNames.fireBarrierCollided, OnFireBarrierCollided);
    }
}
