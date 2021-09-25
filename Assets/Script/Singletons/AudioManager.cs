using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClips
{
    Shoot,
    Jump,
    Heartbeat,
    Aim,
    Steps,
    MachineGunLoad,
    Overheat
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
    [SerializeField] private AudioClip heartbeat;
    [SerializeField] private AudioClip jumpOne;
    [SerializeField] private AudioClip jumpTwo;
    [SerializeField] private AudioClip jumpThree;
    [SerializeField] private AudioClip aim;
    [SerializeField] private AudioClip footstepsOne;
    [SerializeField] private AudioClip footstepsTwo;
    [SerializeField] private AudioClip footstepsThree;
    [SerializeField] private AudioClip footstepsFour;
    [SerializeField] private AudioClip machineGunLoad;
    [SerializeField] private AudioClip overheat;


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
                soundsAudioSource.volume = 0.6f;
                soundsAudioSource.PlayOneShot(shoot);
                break;

            case SoundClips.Heartbeat:
                soundsAudioSource.volume = 1.8f;
                soundsAudioSource.PlayOneShot(heartbeat);
                break;

            case SoundClips.Jump:
                soundsAudioSource.volume = 1f;

                int random = Random.Range(0, 3);
                if (random == 0)
                    soundsAudioSource.PlayOneShot(jumpOne);
                else if (random == 1)
                    soundsAudioSource.PlayOneShot(jumpTwo);
                else if (random == 2)
                    soundsAudioSource.PlayOneShot(jumpThree);
                else
                    soundsAudioSource.PlayOneShot(jumpOne);
                break;

            case SoundClips.Aim:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(aim);
                break;

            case SoundClips.Steps:
                soundsAudioSource.volume = 1f;

                int randomStep = Random.Range(0, 4);

                if (randomStep == 0)
                    soundsAudioSource.PlayOneShot(footstepsOne);

                else if (randomStep == 1)
                    soundsAudioSource.PlayOneShot(footstepsTwo);

                else if (randomStep == 2)
                    soundsAudioSource.PlayOneShot(footstepsThree);

                else if (randomStep == 3) 
                    soundsAudioSource.PlayOneShot(footstepsFour);
                break;

            case SoundClips.MachineGunLoad:
                soundsAudioSource.volume = 1.8f;
                soundsAudioSource.PlayOneShot(machineGunLoad);
                break;

            case SoundClips.Overheat:
                soundsAudioSource.volume = 2f;
                soundsAudioSource.PlayOneShot(overheat);
                break;

            default:
                break;
        }
    }
}
