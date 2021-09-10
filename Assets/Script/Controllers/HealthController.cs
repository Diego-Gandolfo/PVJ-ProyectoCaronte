using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IDamageable
{
    #region Serialize Fields

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    #endregion

    #region Private Fields

    private LifeBarController lifeBar;

    #endregion

    #region Events
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnTakeDamage = new UnityEvent();
    public Action<int, int> OnUpdateLife; //currentLife, MaxLife
    #endregion

    #region Propertys

    public int MaxHealth => maxHealth;
    public int CurrentHealth  => currentHealth;

    #endregion

    #region Unity Methods

    private void Start()
    {
        currentHealth = maxHealth;
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
            OnUpdateLife?.Invoke(currentHealth, MaxHealth);
            OnTakeDamage?.Invoke();
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        if (lifeBar != null)
        {
            if (!lifeBar.IsVisible)
                lifeBar.SetBarVisible(true);

            lifeBar.UpdateLifeBar(currentHealth, maxHealth);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(10); //TODO: BORRAR 
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
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
