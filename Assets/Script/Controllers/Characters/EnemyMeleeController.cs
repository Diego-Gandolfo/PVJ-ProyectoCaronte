using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMeleeWeapon))]
public class EnemyMeleeController : EnemyController
{
    #region Serialized Field

    [SerializeField] [Range(0, 50)] protected float _attackRadius;
    [SerializeField] private float minimumDetectionDistance = 10f; //Esta seria la distancia para detectarlo cuando camina. 
    
    [SerializeField] private AudioSource defaultSounds;
    #endregion

    #region Private

    // Componentes
    private EnemyFootstepsSound footstepsAudioSrc;
    private EnemyMeleeWeapon weapon;
    private Rigidbody _rigidbody;

    // Parameters
    private bool canFollow = false;
    private bool canPlaySound = false;
    private bool playerInRange = false;
    private bool _isAttackAnimationRunning;

    //Sound parameters
    private float timeToPlaySound = 0.5f;
    private float currentTimeToPlaySound;
    #endregion

    #region Unity Methods

    private void Start()
    {
        weapon = GetComponent<EnemyMeleeWeapon>();
        _rigidbody = GetComponent<Rigidbody>();
        footstepsAudioSrc = GetComponent<EnemyFootstepsSound>();

        weapon.SetStats(_attackStats);
        animator.speed = _actorStats.OriginalAnimatorSpeed;
    }

    protected void Update()
    {
        if (!HealthController.IsDead) 
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

            #region FootStepsSound Count

            currentTimeToPlaySound += Time.deltaTime;
            if (currentTimeToPlaySound >= timeToPlaySound)
                canPlaySound = true;
            
            else canPlaySound = false;

            #endregion FootStepsSound Count
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
            {
                CheckPlayerDistance(player);
            }
        }
        else
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

                if(!_isAttackAnimationRunning && !weapon.IsAttacking)
                {
                    _isAttackAnimationRunning = true;
                    weapon.Attack(player);
                    animator.SetTrigger("Stab Attack");
                }
            }        
        } 
        else
        {
            playerInRange = false;
        }
    }

    private void FollowPlayer(PlayerController player)
    {
        if (!HealthController.IsDead)
        {
            if (!playerInRange)
            {
                canFollow = true;

                var xzPlayerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(xzPlayerPosition);

                var direction = (xzPlayerPosition - transform.position).normalized;
                _rigidbody.velocity = direction * _actorStats.OriginalSpeed;

                PlayFootstepsSound();

                // Esto es lo que estaba antes, lo dejo para que se vea el cambio
                //transform.LookAt(player.transform.position, player.transform.up);
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
            }

            CanAttack();
        }

    }

    private void PlayFootstepsSound()
    {
        if (canPlaySound && !HealthController.IsDead)
        {
            footstepsAudioSrc.PlayFootstepsSound();
            currentTimeToPlaySound = 0.0f;
        }
    }

    private void CheckVisibleData()
    {
        if (!HealthController.IsDead)
        {
            if (weapon.IsAttacking || canFollow)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
            }

            if (HealthController.CurrentHealth != HealthController.MaxHealth)
            {
                lifeBar.SetBarVisible(canFollow || weapon.IsAttacking);
            }
        }
    }

    private void CheckPlayerDistance(PlayerController player)
    {
        if (canFollow) //Si ya estaba persiguiendo, seguilo. 
        {
            FollowPlayer(player);
        }

        else
        {
            if (!player.IsSprinting)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) <= minimumDetectionDistance)
                    FollowPlayer(player);
            }
            else //Si el player esta en la zona de deteccion y esta sprinteando 
            {
                FollowPlayer(player);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(transform.position, _detectionArea);
        //Gizmos.DrawWireSphere(weapon.AttackPoint.position, _attackRadius);
    }

    #endregion

    #region Public Methods

    public void OnFinishAttackAnimationTrigger()
    {
        _isAttackAnimationRunning = false;
    }

    protected override void OnDie()
    {
        base.OnDie();
        defaultSounds.Stop();
    }
    #endregion
}
