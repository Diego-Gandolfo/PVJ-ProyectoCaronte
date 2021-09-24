using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeManagement : EnemyController
{
    #region Serialize Fields
    [Header("Detection Distance")]
    [SerializeField] private float whenPlayerMoving = 10f;
    [SerializeField] private float whenPlayerSprinting = 25f;

    #endregion

    #region Private Fields
    private bool canDamage;
    private float timeToDamageAgain = 2.0f;
    private float currentTimeToDamage = 0.0f;
    private float distance;
    private bool mustFollow;
    #endregion

    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        canDamage = true;
    }

    protected void Update()
    {
        if(player != null)
        {
            CheckPlayerDistance();
            FollowPlayer();
            CheckVisibleData();

            if (!canDamage) AttackCooldown();
        }
    }

    #endregion

    #region Private Methods
    private void CheckPlayerDistance()
    {
        distance = !player.IsSprinting ? whenPlayerMoving : whenPlayerSprinting;
        if (Vector3.Distance(player.transform.position, this.transform.position) <= distance)
        {
            mustFollow = true;
        }

        if (Vector3.Distance(player.transform.position, this.transform.position) >= whenPlayerSprinting)
        {
            mustFollow = false;
        }
    }

    private void FollowPlayer()
    {
        if (mustFollow)
        {
            transform.LookAt(player.transform.position, player.transform.up);
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, _actorStats.OriginalSpeed * Time.deltaTime);
        }

        if (animator != null) animator.SetBool("Walk Forward", mustFollow);
    }

    private void CheckVisibleData()
    {
        outline.enabled = mustFollow;

        if(HealthController.CurrentHealth != HealthController.MaxHealth)
            lifeBar.SetBarVisible(mustFollow);
    }

    private void AttackCooldown()
    {
        currentTimeToDamage += Time.deltaTime;
        if (currentTimeToDamage >= timeToDamageAgain)
        {
            canDamage = true;
            currentTimeToDamage = 0.0f;
        }
    }

    #endregion
}
