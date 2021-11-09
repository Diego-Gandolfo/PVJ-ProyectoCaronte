using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToDamage;
    
    private float currentTimeToDamage;

    private bool canStartCount;
    private bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        canStartCount = false;   
        canDamage = false;

        currentTimeToDamage = timeToDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStartCount)
        {
            currentTimeToDamage += Time.deltaTime;
            if (currentTimeToDamage >= timeToDamage)
                canDamage = true;

            else canDamage = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        HealthController playerHealth = other.gameObject.GetComponent<HealthController>();
        if (playerHealth != null)
        {
            canStartCount = true;

            if (canDamage)
            {
                playerHealth.TakeDamage(damage);
                currentTimeToDamage = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController playerHealth = other.gameObject.GetComponent<HealthController>();
        if (playerHealth != null)
        {
            canStartCount = false;
        }
    }
}
