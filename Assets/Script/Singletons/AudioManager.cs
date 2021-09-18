using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClips
{
    Shoot,
    Jump
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField, Range(0, 1)] private float musicInitialVolumen;
    [SerializeField] private AudioClip music;

    [Header("Sounds")]
    [SerializeField] private AudioSource soundsAudioSource;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip jumpOne;
    [SerializeField] private AudioClip jumpTwo;
    [SerializeField] private AudioClip jumpThree;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        musicAudioSource.volume = musicInitialVolumen;
        musicAudioSource.clip = music;
        musicAudioSource.Play();
    }

    public void PlaySound(SoundClips soundClip)
    {
        switch (soundClip)
        {
            case SoundClips.Shoot:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(shoot);
                break;

            case SoundClips.Jump:
                soundsAudioSource.volume = 1f;

                int random = Random.Range(0, 3);
                if (random == 1)
                    soundsAudioSource.PlayOneShot(jumpOne);
                else if (random == 2)
                    soundsAudioSource.PlayOneShot(jumpTwo);
                else if (random == 3)
                    soundsAudioSource.PlayOneShot(jumpThree);
                else
                    soundsAudioSource.PlayOneShot(jump);
                break;

            default:
                break;
        }
    }
}
