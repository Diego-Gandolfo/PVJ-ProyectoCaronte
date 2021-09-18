using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraspassParticlesRender : MonoBehaviour
{
    private ParticleSystem particlesToDeactivate;
    void OnBecameInvisible()
    {
        particlesToDeactivate.Stop();
        print("Desactivado");
    }
    void OnBecameVisible()
    {
        particlesToDeactivate.Play();
        print("activado");
    }
    private void Start()
    {
        particlesToDeactivate = GetComponent<ParticleSystem>();
    }
}
