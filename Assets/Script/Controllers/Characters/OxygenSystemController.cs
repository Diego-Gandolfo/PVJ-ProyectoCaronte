using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class OxygenSystemController : MonoBehaviour
{
    [SerializeField] private float maxOxygen;
    [SerializeField] private float currentOxygen;
    [SerializeField] private float oxigenConsumeMultiplier;
    [SerializeField] private int asphyxiationDamage;
    [SerializeField] private float heartBeatTick;
    private bool isInSafeZone;
    private HealthController healtController;
    private float currentTime;

    //EVENTS
    public Action OnAsphyxiation;
    public Action<float, float> OnChangeInOxygen; //currentOxygen, maxOxygen

    private float exygenRecoverSoundDuration = 1.0f;
    private bool canRecoverOxygen;

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
            if (CheckOxygenLevel())
                    ConsumeOxygen();
            else
                Asphyxiation();

            OnChangeInOxygen?.Invoke(currentOxygen, maxOxygen);
        }
        else if (isInSafeZone)
        {
            exygenRecoverSoundDuration -= Time.deltaTime;
        }
    }

    #region Private Methods
    private bool CheckOxygenLevel()
    {
        return currentOxygen > 0;
    }

    private void ConsumeOxygen()
    {
        currentOxygen -= oxigenConsumeMultiplier;
        if (currentOxygen <= 20)
            PlayHeartbeatSound();
    }

    private void PlayHeartbeatSound()
    {
        if (currentTime <= 0)
        {
            AudioManager.instance.PlaySound(SoundClips.Heartbeat);
            currentTime = heartBeatTick;
        }
    }

    private void PlayOxygenRecoverSound()
    {
        if (exygenRecoverSoundDuration <= 0)
        {
            AudioManager.instance.PlaySound(SoundClips.OxygenRecover);
            exygenRecoverSoundDuration = 1.0f;
        }
    }

    private void Asphyxiation()
    {
        if (currentTime <= 0)
        {
            healtController.TakeDamage(asphyxiationDamage);
            PlayHeartbeatSound();
        }
        //OnAsphyxiation?.Invoke(); //(No borrar) TODO: UI/Sound effect for lack of oxygen, 
    }
    #endregion

    #region Public Methods
    public void RegenerateOxygen(float oxygenRegen)
    {
        if(currentOxygen < maxOxygen)
        {
            if (currentOxygen <= (maxOxygen - oxygenRegen))
            {
                currentOxygen += oxygenRegen;
                PlayOxygenRecoverSound();
            }
            else
                currentOxygen = maxOxygen;

            OnChangeInOxygen?.Invoke(currentOxygen, maxOxygen);
        }
    }

    public void IsInSafeZone(bool value)
    {
        isInSafeZone = value;
    }

    public void ResetValues()
    {
        currentOxygen = maxOxygen;
    }
    #endregion
}
