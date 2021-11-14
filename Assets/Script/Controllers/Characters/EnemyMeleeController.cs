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
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private float _jumpForce;
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

    // Jump parameters
    private bool _canJump;
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

    protected override void Update()
    {
        base.Update();
        if (!HealthController.IsDead)
        {
            DetectTarget();
            DoSound();
            DoAnimation();
            CheckVisibleData();

            if (ObstacleDetection())
                Jump();

            if (!_canJump)
                CheckIsGrounded();
        }
    }

    #endregion

    #region Private Methods

    private void DoSound()
    {
        currentTimeToPlaySound += Time.deltaTime;
        if (currentTimeToPlaySound >= timeToPlaySound)
            canPlaySound = true;

        else canPlaySound = false;
    }

    private void DoAnimation()
    {
        if (canFollow && !playerInRange)
        {
            animator.SetBool("Walk Forward", true);
        }
        else
        {
            animator.SetBool("Walk Forward", false);
        }
    }

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
            if (player != null)
            {
                playerInRange = true;

                if (!_isAttackAnimationRunning && !weapon.IsAttacking)
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

                transform.LookAt(player.transform.position);
                var direction = transform.forward * _actorStats.OriginalSpeed + new Vector3(0f, _rigidbody.velocity.y, 0f);
                _rigidbody.velocity = direction;

                PlayFootstepsSound();
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
        if (!HealthController.IsDead && !hasTakenDamage)
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
            if (!player.IsSprinting || hasTakenDamage)
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

    private bool ObstacleDetection()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 1f, _obstacleLayers))
        {
            if (hit.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    private void Jump()
    {
        if (_canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _canJump = false;
        }
    }

    private void CheckIsGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, 0.1f, _groundLayers))
        {
            if (hit.collider != null)
            {
                _canJump = true;
            }
        }
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
        Destroy(gameObject, 5f);
    }
    #endregion
}
