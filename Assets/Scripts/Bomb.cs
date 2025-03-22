using System;
using TigerForge;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public SoundClip bombExplosion;
    public SoundClip bombLoop;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = SoundManager.Instance.PlaySoundFXClip(bombLoop, transform, loop:true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EventManager.EmitEventData(EventNames.BombExploded, gameObject.transform.position);
            EventManager.EmitEvent(EventNames.BombExploded);
            SoundManager.Instance.PlaySoundFXClip(bombExplosion, transform);
            Destroy(_audioSource.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_audioSource != null)
            Destroy(_audioSource.gameObject);
    }
}
