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
    RunningSteps,
    //MachineGunLoad,
    Overheat,
    Negative,
    OxygenRecover,
    CapsuleActivated,
    UIPopUp,
    InteractableClick,
    AlienWound,
    AttackSound,
    PlayerTakesDamage
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
    [SerializeField] private List<AudioClip> jumpsSounds;
    //[SerializeField] private AudioClip jumpOne;
    //[SerializeField] private AudioClip jumpTwo;
    //[SerializeField] private AudioClip jumpThree;
    [SerializeField] private AudioClip aim;
    [SerializeField] private List<AudioClip> footStepsSounds;
    //[SerializeField] private AudioClip machineGunLoad;
    [SerializeField] private AudioClip overheat;
    [SerializeField] private AudioClip negative;
    [SerializeField] private AudioClip oxygenRecover;
    [SerializeField] private AudioClip capsuleActivated;
    [SerializeField] private AudioClip uiPopUp;
    [SerializeField] private AudioClip interactableClick;
    [SerializeField] private AudioClip alienWound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip takeDamage;

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
                
            case SoundClips.RunningSteps:
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

            case SoundClips.CapsuleActivated:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(capsuleActivated);
                break;

            case SoundClips.UIPopUp:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(uiPopUp);
                break;

            case SoundClips.InteractableClick:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(interactableClick);
                break;

            case SoundClips.AlienWound:
                soundsAudioSource.volume = 0.7f;
                soundsAudioSource.PlayOneShot(alienWound);
                break;

            case SoundClips.AttackSound:
                soundsAudioSource.volume = 0.5f;
                soundsAudioSource.PlayOneShot(attackSound);
                break;

            case SoundClips.PlayerTakesDamage:
                soundsAudioSource.volume = 0.8f;
                soundsAudioSource.PlayOneShot(takeDamage);
                break;

            default:
                break;
        }
    }

    private void PlaySoundSteps()
    {
        int randomStep = Random.Range(0, footStepsSounds.Count);
        soundsAudioSource.PlayOneShot(footStepsSounds[randomStep]);
    }

    private void PlayJump()
    {
        int randomJumps = Random.Range(0, jumpsSounds.Count);
        soundsAudioSource.PlayOneShot(jumpsSounds[randomJumps]);
    }
}
