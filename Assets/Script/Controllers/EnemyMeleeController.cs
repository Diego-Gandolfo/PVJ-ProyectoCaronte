using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMeleeWeapon))]
public class EnemyMeleeController : EnemyController
{
    #region Serialize Fields
    [SerializeField] [Range(1, 50)] protected float _attackRadius;
    [SerializeField] private bool canDrawWires;
    [SerializeField] private float whenPlayerMoving = 10f;
    [SerializeField] private float whenPlayerSprinting = 25f;
    #endregion


    #region Private
    private EnemyMeleeWeapon weapon;
    private bool canFollow;
    private bool playerInRange;
    private float distance;
    #endregion

    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<EnemyMeleeWeapon>();
        print(weapon.name);
        weapon.SetStats(_attackStats);
    }

    protected void Update()
    {
        DetectTarget();
        CheckVisibleData();

        print(weapon.IsAttacking + " can follow: " + canFollow);
        if(canFollow && !playerInRange)
            animator.SetBool("Walk Forward", true);
        else
            animator.SetBool("Walk Forward", false);
    }

    #endregion

    #region Private Methods

    private void DetectTarget()
    {
        Collider[] _collisions = Physics.OverlapBox(transform.position, _detectionRadius, Quaternion.identity, _attackStats.TargetList);

        if (_collisions.Length > 0)
        {
            PlayerController player = _collisions[0].GetComponent<PlayerController>();
            if (player != null)
            {
                FollowPlayer(player);
            }
        } else
        {
            canFollow = false;
        }
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
                //TODO: trigger enemy animation attack?
            }        
        } 
        else
        {
            playerInRange = false;
        }
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
        //distance = !player.IsSprinting ? whenPlayerMoving : whenPlayerSprinting;
        //if (Vector3.Distance(player.transform.position, this.transform.position) <= distance)
        //{
        //    canFollow = true;
        //}

        //if (Vector3.Distance(player.transform.position, this.transform.position) >= whenPlayerSprinting)
        //{
        //    canFollow = false;
        //}
    }


    private void OnDrawGizmos()
    {
        if (canDrawWires)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(weapon.AttackPoint.position, _attackRadius);
            Gizmos.DrawWireCube(transform.position, _detectionRadius);
        }
    }

    #endregion
}
