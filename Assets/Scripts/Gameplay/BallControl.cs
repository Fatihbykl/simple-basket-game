using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{
    [SerializeField] private float forceMagnitude = 5f;
    [SerializeField] private SoundClip[] hopSounds;
    [SerializeField] private SoundClip[] collisionSounds;
    private TouchControls _touchControls;
    private Rigidbody _rigidbody;
    private bool _moveRight;

    private void Awake()
    {
        _touchControls = new TouchControls();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _touchControls.Enable();
        _touchControls.Ball.Tap.performed += BallJump;
    }

    private void OnDisable()
    {
        _touchControls.Disable();
        _touchControls.Ball.Tap.performed -= BallJump;
    }

    private void OnCollisionEnter(Collision other)
    {
        SoundManager.Instance.PlayRandomSoundFXClip(collisionSounds, transform);
    }

    private void BallJump(InputAction.CallbackContext context)
    {
        var forceDirection = _moveRight ? new Vector2(0.5f,1) : new Vector2(-0.5f,1);
        forceDirection.Normalize();

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        SoundManager.Instance.PlayRandomSoundFXClip(hopSounds, transform);
        _moveRight = !_moveRight;
    }
}
