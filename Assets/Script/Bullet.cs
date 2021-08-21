using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damage;
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
    private void OnCollisionEnter(Collision collision)
    {
        if ((hittableMask & 1 << collision.gameObject.layer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
