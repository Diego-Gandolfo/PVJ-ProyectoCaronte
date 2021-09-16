using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRechargeAreaController : MonoBehaviour
{
    [SerializeField] private float oxygenRegenerationPerSecond = 0.5f;
    [SerializeField] private float timerOfRegeneration = 1f;
    private OxygenSystemController currentOxygenUser;
    private float currentTime;
    //private bool isDeployed;

    void Start()
    {
        currentTime = timerOfRegeneration;
        //isDeployed = true; //TODO: si la maquina tiene que deployarse, esto se borra y se arma la interaccion. 
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && currentOxygenUser != null)
        {
            currentOxygenUser.RegenerateOxygen(oxygenRegenerationPerSecond);
            currentTime = timerOfRegeneration;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OxygenSystemController oxygen = other.gameObject.GetComponent<OxygenSystemController>();
        if(oxygen != null)
        {
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
            currentOxygenUser = null;
        }
    }
}
