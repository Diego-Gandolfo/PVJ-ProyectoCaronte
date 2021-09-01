using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWapon : EnemyMeleeManagement
{
    #region Serialize Fields

    [SerializeField] private int damage;

    #endregion

    #region Unity Methods

    public override void Start() { }
    public override void Update() { }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        AttackPlayer();
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AttackPlayer();
        }
    }

    #endregion

    #region Public Methods

    public override void AttackPlayer()
    {
        if (canDamage)
        {
            canDamage = false;
            base.AttackPlayer();
            player.GetComponent<HealthController>().TakeDamage(damage);
        }

    }

    #endregion
}
