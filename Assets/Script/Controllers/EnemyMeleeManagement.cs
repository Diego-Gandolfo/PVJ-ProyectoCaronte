using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeManagement : EnemyController
{
    #region Serialize Fields

    [Header("Detection Distance")]
    [SerializeField] private float whenPlayerMoving = 10f;
    [SerializeField] private float whenPlayerSprinting = 25f;

    [Header("Movement")]
    [SerializeField] private float speed;

    #endregion

    #region Protected Fields

    protected bool canDamage;

    #endregion

    #region Private Fields

    // Components
    private Animator animator;
    private PlayerController playerController;
    private Outline outline;
    private LifeBarController lifeBar;
    private HealthController healthController;

    // Parameters
    private float timeToDamageAgain = 2.0f;
    private float currentTimeToDamage = 0.0f;
    private float distance;
    private bool mustFollow;

    #endregion

    #region Unity Methods

    private void Start()
    {
        RecognizePlayer();
        animator = GetComponentInChildren<Animator>();
        playerController = player.GetComponent<PlayerController>();
        lifeBar = GetComponent<LifeBarController>();
        outline = GetComponent<Outline>();
        healthController = GetComponent<HealthController>();
        lifeBar.SetBarVisible(false); //Empiezan con la barra oculta y solo se activa si reciben daño
        canDamage = true;
    }

    private void Update()
    {
        CheckPlayerDistance();
        FollowPlayer();
        CheckVisibleData();

        if (!canDamage) AttackCooldown();
    }

    #endregion

    #region Private Methods

    private void CheckPlayerDistance()
    {
        distance = playerController.CurrentSpeed == playerController.MoveSpeed ? whenPlayerMoving : whenPlayerSprinting;

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
            transform.LookAt(player.transform.position);
            this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            if (animator != null) animator.SetBool("Walk Forward", true);
        }
        else
        {
            if (animator != null) animator.SetBool("Walk Forward", false);
        }
    }

    private void CheckVisibleData()
    {
        outline.enabled = mustFollow;
        if(healthController.CurrentHealth != healthController.MaxHealth)
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

    #region Public Methods

    public override void AttackPlayer()
    {
        base.AttackPlayer();
    }

    #endregion
}
