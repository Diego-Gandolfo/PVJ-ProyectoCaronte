using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float lifetime;
    [SerializeField] private float speed;

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
        if (other.gameObject.GetComponent<HealthController>() != null)
        {
            HealthController health = other.gameObject.GetComponent<HealthController>();
            health.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    private void CalculatePlayerPosition()
    {
        lastPlayerPosition = player.transform.position - transform.position;
    }
}
