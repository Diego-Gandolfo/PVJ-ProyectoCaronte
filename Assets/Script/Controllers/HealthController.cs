using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    public int MaxHealth => maxHealth;
    [SerializeField] private int maxHealth;
    public int CurrentHealth { get => currentHealth; }
    [SerializeField] private int currentHealth;

    //este booleano lo uso para que solo me debuguee la vida del player y no la de los enemigos. 
    [SerializeField] private bool isPlayer;

    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(10);
        //    Debug.LogError($"soy { name } y mi vida actual es: { currentHealth } ");
        //}  
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            //Debug.Log("encontré un animator para el enemy"); // Esto lo comente para que no este tirando el Debug (recueden borrarlos cuando ya no son necesarios)
            //animator.SetTrigger("TakeDamage"); // De momento esto no hace nada, asi que lo dejo comentado
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
}
