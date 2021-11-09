using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem effectShoot;
    [SerializeField] private float maxShootingTime;
    [SerializeField] private ParticleSystem overheatParticles;
    [SerializeField] private ParticleSystem flashParticles;
    [SerializeField] private float coolingModificator = 10f;
    [SerializeField] private float coolingModificatorOnOverheat = 4f;
    [SerializeField] private float currentMultiplier = 4f;
    private int multiplier = 2;

    private bool canPlaySound;
    private bool isShooting;
    private float currentShootingTime;
    private Animator animator;
    private RaycastHit target;
    private float overheatSoundDuration = 2.0f;
    private bool isRefrigeratorActive;
    
    public bool IsOverheat { get; private set; }

    void Start()
    {
        var particles = overheatParticles.main;
        particles.duration = maxShootingTime;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (isShooting && !IsOverheat)
            {
                currentShootingTime += Time.deltaTime; // sacar el time delta time por un tema de como funciona con el calculo por frame.

                if (currentShootingTime >= maxShootingTime)
                {
                    IsOverheat = true;
                    PlayOverheatSound();
                }
            }
            else //si no esta disparando, resta. 
            {
                if (!isRefrigeratorActive)
                {
                    if (currentShootingTime >= 0)
                    {
                        if (!IsOverheat)
                            currentShootingTime -= (Time.deltaTime / coolingModificator);
                        else
                            currentShootingTime -= (Time.deltaTime / coolingModificatorOnOverheat);
                    }
                }
                else
                {
                    if (currentShootingTime >= 0)
                    {
                        if (!IsOverheat)
                            currentShootingTime -= (Time.deltaTime / (coolingModificator / multiplier));
                        else
                            currentShootingTime -= (Time.deltaTime / (coolingModificatorOnOverheat/multiplier));
                    }

                    //if (currentShootingTime >= 0)
                    //{
                    //    print("entre");
                    //    currentMultiplier += currentMultiplier / Time.deltaTime;
                    //    currentShootingTime -= (Time.deltaTime / currentMultiplier);
                    //} else
                    //{
                    //    currentMultiplier = 4f;
                    //}

                }
            }

            OnOverHeat();

            if(isShooting && !IsOverheat)
                animator.SetBool("IsShooting", true);
            else
                animator.SetBool("IsShooting", false);

            HUDManager.instance.OverHeatManager.UpdateStatBar(currentShootingTime, maxShootingTime);
        } else
        {
            isShooting = false;
        }
    }

    public void Shoot()
    {
        //TODO: Particle system play del firepoint (effecto como si estuviera disparando la bala que sale del arma)
        flashParticles.Play();
        Instantiate(bulletPrefab, target.point, Quaternion.LookRotation(target.normal)); //Instancia en el lugar donde pego la bala. No la vemos recorrer el camino. 
    }

    private void OnOverHeat()
    {
        if (IsOverheat)
        {
            isShooting = false;
            overheatParticles.Play();

            if (currentShootingTime <= 0)
                IsOverheat = false;
        } else
        {
            overheatParticles.Stop();
        }
    }

    private void PlayOverheatSound()
    {
        overheatSoundDuration -= Time.deltaTime;
        if (overheatSoundDuration <= 0) canPlaySound = false;
        else canPlaySound = true;

        if (canPlaySound) AudioManager.instance.PlaySound(SoundClips.Overheat);
    }

    public void SetPlayer(PlayerController player)
    {
        player.IsShooting += CanShoot;
        animator = player.GetComponent<Animator>();
    }

    public void CanShoot(bool value, RaycastHit hit)
    {
        isShooting = value;
        target = hit;
    }

    public void UpgradeBuff()
    {
        isRefrigeratorActive = true;
    }
}
