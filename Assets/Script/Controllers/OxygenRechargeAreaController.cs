using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenRechargeAreaController : MonoBehaviour
{
    private float currentOxigenRegeneration;
    private OxygenSystemController currentOxygenUser;
    [SerializeField] private float currentMultiplier;
    //private bool isDeployed;

    void Start()
    {
        currentOxigenRegeneration = 1f;
        //isDeployed = true; //TODO: si la maquina tiene que deployarse, esto se borra y se arma la interaccion. 
    }

    void Update()
    {
        if(currentOxygenUser != null)
        {
         currentOxigenRegeneration += Time.deltaTime;
        currentOxygenUser.RegenerateOxygen(currentOxigenRegeneration * currentMultiplier);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentOxigenRegeneration = 1f;
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
