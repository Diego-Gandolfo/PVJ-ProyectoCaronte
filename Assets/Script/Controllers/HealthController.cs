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
    private LifeBarController lifeBar;

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
        lifeBar = GetComponent<LifeBarController>();

        if (lifeBar != null)
            lifeBar.UpdateLifeBar(currentHealth, maxHealth);
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

        if (currentHealth <= 0)
        {
            Die();
        }

        if (lifeBar != null)
        {
            if (!lifeBar.IsVisible)
            {
                lifeBar.SetBarVisible(true);
            }

            lifeBar.UpdateLifeBar(currentHealth, maxHealth);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(10);
    }

    public virtual void Die()
    {
        if (isPlayer)
        {
            RespawnManager.instance.Respawn();
            ResetValues();
        }

        else if (!isPlayer)
        {
            float delay = 0.1f;
            Destroy(gameObject, delay);
        }
    }

    public void SetLifeBar(LifeBarController controller)
    {
        lifeBar = controller;
        lifeBar.UpdateLifeBar(currentHealth, maxHealth);
    }

    public void ResetValues()
    {
        currentHealth = maxHealth;
    }
    #endregion
}
