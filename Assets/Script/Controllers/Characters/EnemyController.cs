using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeBarController))]
[RequireComponent(typeof(Outline))]
public abstract class EnemyController : ActorController
{
    [SerializeField] protected Vector3 _detectionArea = new Vector3(30f, 5f, 30f); //Area de deteccion completa del enemigo. 

    #region Protected Fields
    protected Outline outline;
    protected LifeBarController lifeBar;
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
    #endregion

    #region Public Methods

    protected override void OnTakeDamage()
    {
        AudioManager.instance.PlaySound(SoundClips.AlienWound);
        if (animator != null)
        {
            //TODO: Verificar si todos los enemigos van a tener un animator
            animator.SetTrigger("TakeDamage");
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        float delay = 0.1f;
        Destroy(gameObject, delay);
    }
    #endregion
}
