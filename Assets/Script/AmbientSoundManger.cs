using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManger : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameFreeze)
            //audioSource.volume = 0.3f;
            audioSource.mute = true;
        else
            audioSource.mute = false;
    }
}
