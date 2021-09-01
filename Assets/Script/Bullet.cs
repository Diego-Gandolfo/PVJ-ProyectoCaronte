using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private LayerMask hittableMask;
    [SerializeField] private ParticleSystem shootParticles;
    private void Start()
    {
        shootParticles = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<HealthController>() != null)
        {
            HealthController enemyHealth = other.gameObject.GetComponent<HealthController>();
            enemyHealth.TakeDamage(damage);     
        }
        OnDestroy();
    }
    private void OnDestroy()
    {
        Destroy(gameObject.GetComponent<Collider>());
        Destroy(gameObject.GetComponent<MeshRenderer>());
        Destroy(gameObject.GetComponent<TrailRenderer>());
        if (!shootParticles.isPlaying)
        {
            shootParticles.Play();
        }
    }
}
