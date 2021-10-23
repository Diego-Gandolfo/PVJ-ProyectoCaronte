using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class OxygenSystemController : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float maxOxygen;
    [SerializeField] private float currentOxygen;
    [SerializeField] private float oxigenConsumeMultiplier;
    [SerializeField] private float sprintMultiplier = 0.25f;
    [SerializeField] private int asphyxiationDamage;
    [SerializeField] private float heartBeatTick;

    #endregion

    #region Private Fields

    // Components
    private HealthController healtController;
    private PlayerController player;

    // Parameters
    private bool isInSafeZone;
    private float currentTime;
    private float timeToPlayOxygenRecoverSoundAgain = 1.0f;
    private float oxygenRecoverSoundDuration = 3.0f;

    #endregion

    #region Events

    public Action OnAsphyxiation;
    public Action<float, float> OnChangeInOxygen; //currentOxygen, maxOxygen

    #endregion

    #region Propertys

    public float MaxOxygen => maxOxygen;
    public float CurrentOxygen => currentOxygen;

    #endregion

    #region Unity Methods

    void Start()
    {
        healtController = GetComponent<HealthController>();
        player = GetComponent<PlayerController>();
        ResetValues();
    }

    void Update() 
    {
        if (!GameManager.instance.IsGameFreeze)
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
                timeToPlayOxygenRecoverSoundAgain -= Time.deltaTime;
            }
        }
    }

    #endregion

    #region Private Methods

    private bool CheckOxygenLevel()
    {
        return currentOxygen > 0;
    }

    private void ConsumeOxygen()
    {
        var oxygenToConsume = oxigenConsumeMultiplier * Time.deltaTime;

        if (currentOxygen <= (maxOxygen / 3))
        {
            oxygenToConsume /= 2; // para que consuma la mitad
        }

        if (currentOxygen <= (maxOxygen / 5))
        {
            PlayHeartbeatSound();
        }

        if (currentOxygen <= (maxOxygen / 10))
        {
            oxygenToConsume /= 2; // es deliverado que se vuelva a dividir, para que en esta etapa este consumiendo 1/4
        }

        if (player.IsSprinting)
        {
            currentOxygen -= oxygenToConsume * sprintMultiplier;
        }
        else
        {
            currentOxygen -= oxygenToConsume;
        }
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
        if (timeToPlayOxygenRecoverSoundAgain <= 0 && currentOxygen <= 50)
        {
            AudioManager.instance.PlaySound(SoundClips.OxygenRecover);
            timeToPlayOxygenRecoverSoundAgain = oxygenRecoverSoundDuration;
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
        var oxygenToRegen = oxygenRegen * Time.deltaTime;

        if (currentOxygen < maxOxygen)
        {
            if (currentOxygen <= (maxOxygen - oxygenToRegen))
            {
                currentOxygen += oxygenToRegen;
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
