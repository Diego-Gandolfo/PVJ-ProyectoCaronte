using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWapon : EnemyMeleeManagement
{
    [SerializeField] private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals("Player"))
        {
            HealthController playerHealth = other.GetComponent<HealthController>();
            playerHealth.TakeDamage(damage);
        }
    }

    public override void AttackPlayer()
    {
        base.AttackPlayer();

    }
}
