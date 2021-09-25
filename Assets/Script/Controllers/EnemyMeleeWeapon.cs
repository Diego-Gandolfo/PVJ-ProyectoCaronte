using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;

    private AttackStats _attackStats;
    private float timerCD;

    public bool IsAttacking { get; private set; }
    public Transform AttackPoint => attackPoint;
    
    private void Update()
    {
        if (IsAttacking)
        {
            timerCD -= Time.deltaTime;
            if(timerCD <= 0)
            {
                IsAttacking = false;
            }
        }
    }

    #region Public Methods
    public void Attack(PlayerController player)
    {
        if (!IsAttacking)
        {
            print("attack");
            IsAttacking = true;
            player.HealthController.TakeDamage(_attackStats.Damage);
            timerCD = _attackStats.Cooldown;
        }
    }

    public void SetStats(AttackStats stats)
    {
        _attackStats = stats;
    }

    #endregion
}
