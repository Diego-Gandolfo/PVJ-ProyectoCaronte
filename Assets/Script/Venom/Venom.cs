using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeToDamage;

    private float currentTimeToDamage = 0.0f;

    private bool canStartCount;
    private bool canDamage;

    private void Start()
    {
        PlayVenomSound();
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
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            HealthController playerHealth = other.gameObject.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                canStartCount = true;

                if (canDamage)
                {
                    playerHealth.TakeDamage(damage);
                    HUDManager.instance.LifeBar.SetBarColor(Color.magenta);
                    currentTimeToDamage = 0.0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController playerHealth = other.gameObject.GetComponent<HealthController>();
        if (playerHealth != null)
            canStartCount = false;
        HUDManager.instance.LifeBar.ResetBarColor();
    }

    private void PlayVenomSound()
    {
        AudioManager.instance.PlaySound(SoundClips.Venom);
    }
}
