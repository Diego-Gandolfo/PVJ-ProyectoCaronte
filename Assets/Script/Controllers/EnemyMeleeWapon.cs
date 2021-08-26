using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWapon : EnemyMeleeManagement
{
    [SerializeField] private int damage;

    public override void AttackPlayer()
    {
        base.AttackPlayer();
        player.GetComponent<HealthController>().TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AttackPlayer();
        }
    }
}
