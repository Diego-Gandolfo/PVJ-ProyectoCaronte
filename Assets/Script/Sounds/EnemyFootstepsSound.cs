using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepsSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footsteps;

    [SerializeField] private AudioSource footstepsAudioSource;

    public void PlayFootstepsSound()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            int randomStep = Random.Range(0, footsteps.Count);
            footstepsAudioSource.PlayOneShot(footsteps[randomStep]);

            footstepsAudioSource.volume = 0.4f;
            footstepsAudioSource.pitch = 1.5f;
        }
    }
}
