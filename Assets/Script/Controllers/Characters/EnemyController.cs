using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeBarController))]
[RequireComponent(typeof(Outline))]
public abstract class EnemyController : ActorController
{
    [SerializeField] protected Vector3 _detectionArea = new Vector3(30f, 5f, 30f); //Area de deteccion completa del enemigo. 
    [SerializeField] protected float activeDamageTimer = 15f;

    #region Protected Fields
    
    protected Outline outline;
    protected LifeBarController lifeBar;
    protected bool hasTakenDamage;
    protected float damageTimer;

    #endregion

    #region Protected Methods

    protected override void Awake()
    {
        base.Awake();
        
        outline = GetComponent<Outline>();
        lifeBar = GetComponent<LifeBarController>();

        if (lifeBar != null)
            lifeBar.SetBarVisible(false); //Empiezan con la barra oculta y solo se activa si reciben daño
    }

    protected virtual void Update()
    {
        if (hasTakenDamage && !HealthController.IsDead)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                hasTakenDamage = false;
                outline.enabled = false;
                lifeBar.SetBarVisible(false);
            }
        }
    }

    #endregion

    #region Public Methods

    protected override void OnTakeDamage()
    {
        if (!HealthController.IsDead)
        {
            AudioManager.instance.PlaySound(SoundClips.AlienWound);

            hasTakenDamage = true;
            outline.enabled = true;
            damageTimer = activeDamageTimer;

            if (animator != null)
                animator.SetTrigger("TakeDamage");
        }
    }

    protected override void OnDie()
    {
        if (HealthController.IsDead && animator != null)
            animator.SetTrigger("Die");

        if (lifeBar != null) lifeBar.SetBarVisible(false);
        if (outline != null) outline.enabled = false;

        
    }

    #endregion
}
