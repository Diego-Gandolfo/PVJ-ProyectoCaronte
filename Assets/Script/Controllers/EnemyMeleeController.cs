using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMeleeWeapon))]
public class EnemyMeleeController : EnemyController
{
    #region Serialize Fields
    [SerializeField] [Range(1, 50)] protected float _attackRadius;
    [SerializeField] private EnemyMeleeWeapon weapon;

    //[SerializeField] private float whenPlayerMoving = 10f;
    //[SerializeField] private float whenPlayerSprinting = 25f;

    #endregion

    private bool canFollow;


    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<EnemyMeleeWeapon>();
        weapon.SetStats(_attackStats);
    }

    protected void Update()
    {
        DetectTarget();
        CheckVisibleData();

        if(canFollow)
        {
            print("canFollow: " + canFollow + " IsAttacking" + weapon.IsAttacking);
            if (weapon.IsAttacking)
            {
                animator.SetBool("Walk Forward", false);
            }
            else
            {
                animator.SetBool("Walk Forward", true);
            }
        } else
        {
            animator.SetBool("Walk Forward", false);
        }

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
                CanAttack();
                print("detected");
            }
        }
    }

    private void CanAttack()
    {
        Collider[] _collisions = Physics.OverlapSphere(weapon.transform.position, _attackRadius, _attackStats.TargetList);
        print("canAttack: " + _collisions.Length);
        if (_collisions.Length > 0)
        {
            PlayerController player = _collisions[0].GetComponent<PlayerController>();
            if(player != null)
            {
                print(player);
                weapon.Attack(player);
                //TODO: trigger enemy animation attack?
            }        
        } 
    }

    private void FollowPlayer(PlayerController player)
    {
        transform.LookAt(player.transform.position, player.transform.up);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
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
        //    mustFollow = true;
        //}

        //if (Vector3.Distance(player.transform.position, this.transform.position) >= whenPlayerSprinting)
        //{
        //    mustFollow = false;
        //}
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weapon.transform.position, _attackRadius);
        Gizmos.DrawWireCube(transform.position, _detectionRadius);
    }

    #endregion
}
