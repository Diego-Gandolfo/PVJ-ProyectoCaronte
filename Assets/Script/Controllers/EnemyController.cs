using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeBarController))]
[RequireComponent(typeof(Outline))]
public abstract class EnemyController : ActorController
{
    #region Protected Fields
    protected PlayerController player;
    protected Outline outline;
    protected LifeBarController lifeBar;
    #endregion

    #region Protected Methods

    public virtual void Start()
    {
        
        outline = GetComponent<Outline>();
        lifeBar = GetComponent<LifeBarController>();
        if(lifeBar != null) 
            lifeBar.SetBarVisible(false); //Empiezan con la barra oculta y solo se activa si reciben daño
    }
    #endregion
    public virtual void Update()
    {
        if(player == null)
        {
            player = GameManager.instance.Player;
        }
    }
    #region Public Methods

    protected override void OnTakeDamage()
    {
        if (animator != null) //TODO: Verificar si todos los enemigos van a tener un animator
            animator.SetTrigger("TakeDamage");
    }

    protected override void OnDie()
    {
        base.OnDie();
        float delay = 0.1f;
        Destroy(gameObject, delay);
    }
    #endregion
}
