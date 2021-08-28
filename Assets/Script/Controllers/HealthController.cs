using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    #region Serialize Fields

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isPlayer; //este booleano lo uso para que solo me debuguee la vida del player y no la de los enemigos. 

    #endregion

    #region Private Fields

    private Animator animator;

    #endregion

    #region Propertys

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get => currentHealth; }

    #endregion

    #region Unity Methods

    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponentInChildren<Animator>();
    }

    #endregion

    #region Public Methods

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (!isPlayer) animator.SetTrigger("TakeDamage"); // TODO: hacer la animacion del Player para TakeDamage
        }

        if (currentHealth <= 0) Die();
    }

    public virtual void Die()
    {
        //animator.SetTrigger("Die");
        float delay = 0.1f;
        if (isPlayer) GameManager.instance?.GameOver();
        else Destroy(gameObject, delay);
    }

    #endregion
}
