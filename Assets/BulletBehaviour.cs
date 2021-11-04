using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float lifetime;
    [SerializeField] private float speed;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = LevelManager.instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) Destroy(gameObject);

        Vector3 playerPosition = player.transform.position - transform.position;
        transform.position += playerPosition * speed * Time.deltaTime;
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
}
