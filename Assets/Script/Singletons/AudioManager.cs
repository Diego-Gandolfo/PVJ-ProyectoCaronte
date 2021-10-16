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
    //MachineGunLoad,
    Overheat,
    Negative,
    OxygenRecover
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
    //[SerializeField] private AudioClip machineGunLoad;
    [SerializeField] private AudioClip overheat;
    [SerializeField] private AudioClip negative;
    [SerializeField] private AudioClip oxygenRecover;

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
        //musicAudioSource.Play(); //TODO: Poner musica
    }

    public void PlaySound(SoundClips soundClip)
    {
        switch (soundClip)
        {
            case SoundClips.Shoot:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(shoot);
                break;

            case SoundClips.Heartbeat:
                soundsAudioSource.volume = 1.5f;
                soundsAudioSource.PlayOneShot(heartbeat);
                break;

            case SoundClips.Jump:
                soundsAudioSource.volume = 0.5f;
                PlayJump();
                break;

            case SoundClips.Aim:
                soundsAudioSource.volume = 0.1f;
                soundsAudioSource.PlayOneShot(aim);
                break;

            case SoundClips.Steps:
                soundsAudioSource.volume = 0.3f;
                PlaySoundSteps();
                break;

            //case SoundClips.MachineGunLoad:
            //    soundsAudioSource.volume = 1.8f;
            //    //soundsAudioSource.PlayOneShot(machineGunLoad); //TODO: PONER SONIDO LOAD MACHINEGUN
            //    break;

            case SoundClips.Overheat:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(overheat); 
                break;

            case SoundClips.Negative:
                soundsAudioSource.volume = 2f;
                soundsAudioSource.PlayOneShot(negative);
                break;

            case SoundClips.OxygenRecover:
                soundsAudioSource.volume = 1f;
                soundsAudioSource.PlayOneShot(oxygenRecover);
                break;

            default:
                break;
        }
    }

    private void PlaySoundSteps()
    {
        int randomStep = Random.Range(0, 4);

        if (randomStep == 0)
            soundsAudioSource.PlayOneShot(footstepsOne);
        else if (randomStep == 1)
            soundsAudioSource.PlayOneShot(footstepsTwo);
        else if (randomStep == 2)
            soundsAudioSource.PlayOneShot(footstepsThree);
        else if (randomStep == 3)
            soundsAudioSource.PlayOneShot(footstepsFour);
    }

    private void PlayJump()
    {

        int random = Random.Range(0, 3);
        if (random == 0)
            soundsAudioSource.PlayOneShot(jumpOne);
        else if (random == 1)
            soundsAudioSource.PlayOneShot(jumpTwo);
        else if (random == 2)
            soundsAudioSource.PlayOneShot(jumpThree);
        else
            soundsAudioSource.PlayOneShot(jumpOne);
    }
}
