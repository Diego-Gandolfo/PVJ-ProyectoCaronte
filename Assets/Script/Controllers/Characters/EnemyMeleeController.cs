using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMeleeWeapon))]
public class EnemyMeleeController : EnemyController
{
    [SerializeField] [Range(1, 50)] protected float _attackRadius;
    [SerializeField] private float minimumDetectionDistance = 10f; //Esta seria la distancia para detectarlo cuando camina. 

    #region Private

    private EnemyMeleeWeapon weapon;
    private bool canFollow = false;
    private bool playerInRange = false;

    #endregion

    #region Unity Methods
    private void Start()
    {
        weapon = GetComponent<EnemyMeleeWeapon>();
        weapon.SetStats(_attackStats);
        animator.speed = _actorStats.OriginalAnimatorSpeed;
    }

    protected void Update()
    {
        DetectTarget();
        CheckVisibleData();

        if(canFollow && !playerInRange)
        {
            animator.SetBool("Walk Forward", true);
        }
        else
        {
            
            animator.SetBool("Walk Forward", false);
        }
    }

    #endregion

    #region Private Methods

    private void DetectTarget()
    {
        Collider[] _collisions = Physics.OverlapBox(transform.position, _detectionArea, Quaternion.identity, _attackStats.TargetList);

        if (_collisions.Length > 0)
        {
            PlayerController player = _collisions[0].GetComponent<PlayerController>();
            if (player != null)
                CheckPlayerDistance(player);
        } else
            canFollow = false;

    }

    private void CanAttack()
    {
        Collider[] _collisions = Physics.OverlapSphere(weapon.AttackPoint.position, _attackRadius, _attackStats.TargetList);
        if (_collisions.Length > 0)
        {
            PlayerController player = _collisions[0].GetComponent<PlayerController>();
            if(player != null)
            {
                playerInRange = true;
                weapon.Attack(player);
                animator.SetTrigger("Stab Attack");
            }        
        } 
        else
            playerInRange = false;
    }

    private void FollowPlayer(PlayerController player)
    {
        if (!playerInRange)
        {
            canFollow = true;
            transform.LookAt(player.transform.position, player.transform.up);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
        }
        CanAttack();
    }

    private void CheckVisibleData()
    {
        if(weapon.IsAttacking || canFollow)
            outline.enabled = true;
        else 
            outline.enabled = false;

        if(HealthController.CurrentHealth != HealthController.MaxHealth)
            lifeBar.SetBarVisible(canFollow || weapon.IsAttacking);
    }

    private void CheckPlayerDistance(PlayerController player)
    {
        if (canFollow) //Si ya estaba persiguiendo, seguilo. 
            FollowPlayer(player);
        else
        {
            if (!player.IsSprinting)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) <= minimumDetectionDistance)
                    FollowPlayer(player);

            }
            else //Si el player esta en la zona de deteccion y esta sprinteando 
                FollowPlayer(player);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _detectionArea);
        //Gizmos.DrawWireSphere(weapon.AttackPoint.position, _attackRadius);
    }

    #endregion
}
