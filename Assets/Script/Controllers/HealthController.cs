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
    public Action<int, int> OnUpdateLife; //currentLife, MaxLife
    #endregion

    #region Propertys
    public int MaxHealth => _actorStats.MaxLife;
    public int CurrentHealth { get; private set; }
    #endregion

    #region Public Methods

    public void TakeDamage(int damage)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damage;
            OnUpdateLife?.Invoke(CurrentHealth, MaxHealth);
            OnTakeDamage?.Invoke();
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }

        if (lifeBar != null)
        {
            if (!lifeBar.IsVisible)
                lifeBar.SetBarVisible(true);

            lifeBar.UpdateLifeBar(CurrentHealth, MaxHealth);
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
        lifeBar.UpdateLifeBar(CurrentHealth, MaxHealth);
    }

    public void ResetValues()
    {
        CurrentHealth = MaxHealth;
        OnUpdateLife?.Invoke(CurrentHealth, MaxHealth);
    }

    public void SetStats(ActorStats actor)
    {
        _actorStats = actor;
        CurrentHealth = MaxHealth;
        lifeBar = GetComponent<LifeBarController>();
        if (lifeBar != null)
            lifeBar.UpdateLifeBar(CurrentHealth, MaxHealth);
    }
    #endregion
}
