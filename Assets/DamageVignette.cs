using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVignette : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssign;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

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
