using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraspassParticlesRender : MonoBehaviour
{
    private ParticleSystem particlesToDeactivate;
    void OnBecameInvisible()
    {
        particlesToDeactivate.Stop();
    }
    void OnBecameVisible()
    {
        particlesToDeactivate.Play();
    }
    private void Start()
    {
        particlesToDeactivate = GetComponent<ParticleSystem>();
    }
}
