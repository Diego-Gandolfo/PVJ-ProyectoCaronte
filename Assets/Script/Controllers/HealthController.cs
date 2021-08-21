using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    public int MaxHealth => maxHealth;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    //este booleano lo uso para que solo me debuguee la vida del player y no la de los enemigos. 
    [SerializeField] private bool isPlayer;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
            Debug.LogError($"soy { name } y mi vida actual es: { currentHealth } ");
        }  
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0) currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log($"soy { name } y morí");
    }
}
