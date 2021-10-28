using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVignette : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssign;
        animator = GetComponent<Animator>();
    }

    private void OnPlayerAssign(PlayerController player)
    {
        LevelManager.instance.OnPlayerAssing -= OnPlayerAssign;
        player.GetComponent<HealthController>().OnTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        animator.SetTrigger("IsTakingDamage");
    }
}
