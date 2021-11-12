using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float lifetime;
    [SerializeField] private float speed;

    [SerializeField] private GameObject venomProjectile;

    private PlayerController player;

    private Vector3 lastPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = LevelManager.instance.Player;
        CalculatePlayerPosition();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) Destroy(gameObject);
        
        transform.position += lastPlayerPosition * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            HealthController playerHealth = player.GetComponent<HealthController>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                Instantiate(venomProjectile, transform.position, Quaternion.identity);
                
                

                Destroy(gameObject, 0.5f);
            }
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(venomProjectile, transform.position, Quaternion.identity);

        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Enviroment"))
        {
            Instantiate(venomProjectile, transform.position, Quaternion.identity);

        }
    }

    private void CalculatePlayerPosition()
    {
        lastPlayerPosition = player.transform.position - transform.position;
    }


}
