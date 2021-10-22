using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour, IDamageable
{
    #region Private Fields
    private ActorStats _actorStats;
    private LifeBarController lifeBar;
    #endregion

    #region Events
    public Action OnDie;
    public Action OnTakeDamage;
    public Action<int> OnUpdateLife; //currentLife, MaxLife
    #endregion

    #region Propertys
    public int MaxHealth => _actorStats.MaxLife;
    public int CurrentHealth { get; private set; }
    #endregion

    #region Public Methods

    public void Heal(int heal)
    {
        if(CurrentHealth < MaxHealth)
        {
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;

            UpdateLifeBar();
        }
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            UpdateLifeBar();
            OnTakeDamage?.Invoke();
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }

        
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
    }

    public void SetLifeBar(LifeBarController controller)
    {
        lifeBar = controller;
        lifeBar.UpdateLifeBar(CurrentHealth);
    }

    public void ResetValues()
    {
        CurrentHealth = MaxHealth;
        UpdateLifeBar();
        //OnUpdateLife?.Invoke(CurrentHealth, MaxHealth);
    }

    public void SetStats(ActorStats actor)
    {
        _actorStats = actor;
        CurrentHealth = MaxHealth;

        lifeBar = GetComponent<LifeBarController>();
        UpdateLifeBar();
    }
    #endregion

    private void UpdateLifeBar()
    {
        OnUpdateLife?.Invoke(CurrentHealth);

        if (lifeBar != null)
        {
            if (!lifeBar.IsVisible)
                lifeBar.SetBarVisible(true);

            lifeBar.UpdateLifeBar(CurrentHealth);
        }
    }
}
