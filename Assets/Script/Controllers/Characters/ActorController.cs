using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Animator))]
public class ActorController : MonoBehaviour
{
    [SerializeField] protected ActorStats _actorStats;
    [SerializeField] protected AttackStats _attackStats;
    
    protected Animator animator;

    public AttackStats AttackStats => _attackStats;
    public HealthController HealthController { get; protected set; }

    protected virtual void Awake()
    {
        HealthController = GetComponent<HealthController>();
        animator = GetComponent<Animator>();
        InitStats();
    }

    private void Start()
    {
    }

    protected void InitStats()
    {
        HealthController.SetStats(_actorStats);

        HealthController.OnDie += OnDie;
        HealthController.OnTakeDamage += OnTakeDamage;
    }

    protected virtual void OnTakeDamage()
    {
        //animator.SetTrigger("TakeDamage");
    }

    protected virtual void OnDie()
    {
        //if (animator != null)
        //    animator.SetTrigger("Die");
    }
}
