using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRechargeAreaController : MonoBehaviour
{
    [SerializeField] private float oxygenStartingRegeneration = 1f;
    [SerializeField] private float currentMultiplier;
    [SerializeField] private int heal = 1;
    [SerializeField] private float secondsToHeal;
    
    //private bool isDeployed;
    private float currentOxigenRegeneration;
    private OxygenSystemController currentOxygenUser;
    private HealthController healtController;
    private float healTimer;
    private float currentSecondsToHealTimer;

    void Start()
    {
        //isDeployed = true; //TODO: si la maquina tiene que deployarse, esto se borra y se arma la interaccion. 
    }

    void Update()
    {
        if(currentOxygenUser != null)
        {

            if(currentOxygenUser.MaxOxygen > currentOxygenUser.CurrentOxygen)
            {
                currentOxigenRegeneration += Time.deltaTime;
                currentOxygenUser.RegenerateOxygen(currentOxigenRegeneration * currentMultiplier);
            }
            else if(healtController.CurrentHealth < healtController.MaxHealth)
            {
                healTimer += Time.deltaTime;
                if(healTimer >= currentSecondsToHealTimer)
                {
                    healtController.Heal(heal);
                    healTimer = 0;
                    currentSecondsToHealTimer -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentOxigenRegeneration = oxygenStartingRegeneration;
        OxygenSystemController oxygen = other.gameObject.GetComponent<OxygenSystemController>();
        if(oxygen != null)
        {
            healtController = oxygen.GetComponent<HealthController>();
            currentSecondsToHealTimer = secondsToHeal;
            currentOxygenUser = oxygen;
            currentOxygenUser.IsInSafeZone(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        OxygenSystemController oxygen = other.gameObject.GetComponent<OxygenSystemController>();
        if (oxygen == currentOxygenUser && currentOxygenUser != null) //si el que se fue del area de trigger, coincide con el guardado
        {
            currentOxygenUser.IsInSafeZone(false);
            healtController = null;
            currentOxygenUser = null;
        }
    }

}
