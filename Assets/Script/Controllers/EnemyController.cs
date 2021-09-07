using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    #region Protected Fields
    protected PlayerController player;
    protected HealthController healthController;
    protected Animator animator;
    protected Outline outline;
    protected LifeBarController lifeBar;
    #endregion

    #region Protected Methods
    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        outline = GetComponent<Outline>();
        lifeBar = GetComponent<LifeBarController>();
        healthController = GetComponent<HealthController>();
        healthController.OnDie.AddListener(OnDieListener);
        healthController.OnTakeDamage.AddListener(OnTakeDamage);
        if(lifeBar != null) lifeBar.SetBarVisible(false); //Empiezan con la barra oculta y solo se activa si reciben daño
        player = GameManager.instance.Player;
    }
    #endregion

    #region Public Methods
    public virtual void AttackPlayer()
    {
    }

    public virtual void OnTakeDamage()
    {
        if (animator != null) //TODO: Verificar si todos los enemigos van a tener un animator
            animator.SetTrigger("TakeDamage");
    }

    public virtual void OnDieListener()
    {
        float delay = 0.1f;
        Destroy(gameObject, delay);
    }
    #endregion
}
