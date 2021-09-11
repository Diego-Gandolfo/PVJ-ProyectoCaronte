using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class OxygenSystemController : MonoBehaviour
{
    [SerializeField] private float maxOxygen;
    [SerializeField] private float currentOxygen;
    [SerializeField] private float oxygenConsumptionPerSecond;
    [SerializeField] private float timerOfConsumption = 1f;
    [SerializeField] private int asphyxiationDamage;

    private bool isInSafeZone;
    private HealthController healtController;
    private float currentTime;

    //EVENTS
    public Action OnAsphyxiation;
    public Action<float, float> OnChangeInOxygen; //currentOxygen, maxOxygen

    void Start()
    {
        healtController = GetComponent<HealthController>();
        currentOxygen = maxOxygen;
        currentTime = timerOfConsumption;
    }

    void Update() 
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && !isInSafeZone)
        {
            if (CheckOxygenLevel())
                ConsumeOxygen();
            else
                Asphyxiation();
            
            currentTime = timerOfConsumption;
            OnChangeInOxygen?.Invoke(currentOxygen, maxOxygen);
        }
    }

    #region Private Methods
    private bool CheckOxygenLevel()
    {
        return currentOxygen > 0;
    }

    private void ConsumeOxygen()
    {
        currentOxygen -= oxygenConsumptionPerSecond;
    }

    private void Asphyxiation()
    {
        healtController.TakeDamage(asphyxiationDamage);
        //OnAsphyxiation?.Invoke(); //(No borrar) TODO: UI/Sound effect for lack of oxygen, 
    }
    #endregion

    #region Public Methods
    public void RegenerateOxygen(float oxygenRegen)
    {
        if(currentOxygen < maxOxygen)
        {
            if (currentOxygen <= (maxOxygen - oxygenRegen))
                currentOxygen += oxygenRegen;
            else
                currentOxygen = maxOxygen;

            OnChangeInOxygen?.Invoke(currentOxygen, maxOxygen);
        }
    }

    public void IsInSafeZone(bool value)
    {
        isInSafeZone = value;
    }
    #endregion
}
