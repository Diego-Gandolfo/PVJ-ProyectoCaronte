using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float delayToDestroy = 0.1f;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private LayerMask hittableMask;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((hittableMask & 1 << other.gameObject.layer) != 0)
        {
            HealthController enemyHealth = other.GetComponent<HealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"después de este disparo, la vida del enemigo es { enemyHealth.CurrentHealth }");
            }

            Destroy(gameObject, delayToDestroy);
        }
    }
}
