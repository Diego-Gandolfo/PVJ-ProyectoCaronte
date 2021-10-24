using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Transform attackPoint;

    #endregion

    #region Private Fields

    // Components
    private AttackStats _attackStats;
    private PlayerController _player;

    // Parameters
    private float timerCD;

    #endregion

    #region Propertys

    public bool IsAttacking { get; private set; }
    public Transform AttackPoint => attackPoint;

    #endregion

    #region Unity Methods

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

    #endregion

    #region Public Methods

    public void Attack(PlayerController player)
    {
        if (!IsAttacking)
        {
            _player = player;
            IsAttacking = true;
            timerCD = _attackStats.Cooldown;
        }
    }

    public void SetStats(AttackStats stats)
    {
        _attackStats = stats;
    }

    public void OnAttackHitAnimationTrigger()
    {
        if (_player != null)
        {
            _player.HealthController.TakeDamage(_attackStats.Damage);
            AudioManager.instance.PlaySound(SoundClips.AttackSound);
        }
    }

    #endregion
}
