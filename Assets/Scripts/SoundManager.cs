using System;
using UnityEngine;


[Serializable]
public class SoundClip
{
    public AudioClip audioClip;
    [Range(0, 1)] public float volume;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioObject;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource PlaySoundFXClip(SoundClip clip, Transform spawnTransform, float pitch = 1f, bool loop = false)
    {
        if (clip == null)
        {
            return null;
        }

        AudioSource audioSource = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip.audioClip;
        audioSource.volume = clip.volume;
        audioSource.pitch = Mathf.Clamp(pitch, 0.5f, 2.0f);
        audioSource.loop = loop;
        audioSource.Play();

        if (loop)
        {
            return audioSource;
        }
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        return null;
    }

    public void PlayRandomSoundFXClip(SoundClip[] clips, Transform spawnTransform)
    {
        if (clips.Length == 0)
        {
            return;
        }

        int rand = UnityEngine.Random.Range(0, clips.Length);
        var clip = clips[rand];

        AudioSource audioSource = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = clip.audioClip;
        audioSource.volume = clip.volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXWithSource(AudioSource audioSource, SoundClip[] clips)
    {
        if (clips.Length == 0 || audioSource == null)
        {
            return;
        }

        int rand = UnityEngine.Random.Range(0, clips.Length);
        var clip = clips[rand];

        audioSource.clip = clip.audioClip;
        audioSource.volume = clip.volume;
        audioSource.Play();
    }
}