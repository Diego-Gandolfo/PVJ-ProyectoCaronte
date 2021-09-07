using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWapon : MonoBehaviour
{
    [SerializeField] private int damage;

    private PlayerController player;
    private bool canDoDamage;

    private float timeToDamageAgain = 2.0f;
    private float timeToDamage = 0.0f;
    

    private void Start() 
    {
        player = GameManager.instance.Player;
        canDoDamage = true;
        timeToDamage = timeToDamageAgain;
    }

    private void Update()
    {
        if (canDoDamage == false)
        {
            timeToDamage += Time.deltaTime;
            if (timeToDamage >= timeToDamageAgain)
            {
                canDoDamage = true;
                timeToDamage = 0.0f;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AttackPlayer();
        }
    }

    #region Public Methods

    private void AttackPlayer()
    {
        if (canDoDamage)
        {
             HealthController hp = player.GetComponent<HealthController>();
             hp.TakeDamage(10);
             canDoDamage = false;
        }
    }

    #endregion
}
