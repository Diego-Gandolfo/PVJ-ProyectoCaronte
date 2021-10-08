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
        ResetValues();
    }

    void Update() 
    {
        if (!isInSafeZone)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                if (CheckOxygenLevel())
                    ConsumeOxygen();
                else
                    Asphyxiation();

                currentTime = timerOfConsumption;
                OnChangeInOxygen?.Invoke(currentOxygen, maxOxygen);
            }
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
        if (currentOxygen <= 20)
            PlayHeartbeatSound();
    }

    private void PlayHeartbeatSound()
    {
        AudioManager.instance.PlaySound(SoundClips.Heartbeat);
    }

    private void Asphyxiation()
    {
        healtController.TakeDamage(asphyxiationDamage);
        PlayHeartbeatSound();
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
        if(isInSafeZone)
            currentTime = timerOfConsumption;
    }

    public void ResetValues()
    {
        currentOxygen = maxOxygen;
        currentTime = timerOfConsumption;
    }
    #endregion
}
