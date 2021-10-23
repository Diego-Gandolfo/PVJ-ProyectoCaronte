using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Dispersion", true);
    }    
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Dispersion", false);
    }
}
