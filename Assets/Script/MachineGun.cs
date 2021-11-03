using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem effectShoot;
    [SerializeField] private float maxShootingTime;
    [SerializeField] private ParticleSystem shootingParticles;
    [SerializeField] private ParticleSystem flashParticles;

    private bool canPlaySound;
    private bool isShooting;
    private float currentShootingTime;
    private Animator animator;
    private RaycastHit target;
    private float overheatSoundDuration = 2.0f;

    public bool IsOverheat { get; private set; }

    void Start()
    {
        var particles = shootingParticles.main;
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
                if (currentShootingTime >= 0)
                {
                    if (!IsOverheat)
                        currentShootingTime -= (Time.deltaTime / 10f);
                    else
                        currentShootingTime -= (Time.deltaTime / 4f);
                }
            }

            OnOverHeat();

            if(isShooting && !IsOverheat)
                animator.SetBool("IsShooting", true);
            else
                animator.SetBool("IsShooting", false);

            HUDManager.instance.OverHeatManager.UpdateStatBar(currentShootingTime, maxShootingTime);
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
            shootingParticles.Play();

            if (currentShootingTime <= 0)
                IsOverheat = false;
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
}
