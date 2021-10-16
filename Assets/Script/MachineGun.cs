using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem effectShoot;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxShootingTime;
    [SerializeField] private ParticleSystem shootingParticles;
    [SerializeField] private ParticleSystem flashParticles;

    private bool isOverheat;
    private bool isShooting;
    private float currentShootingTime;
    private Animator animator;
    private RaycastHit target;

    private float timeToPlayOverheatSound = 1.0f;
    private bool hasPlayedSound;

    void Start()
    {
        var particles = shootingParticles.main;
        particles.duration = maxShootingTime;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (isShooting && !isOverheat)
            {
                currentShootingTime += Time.deltaTime; // sacar el time delta time por un tema de como funciona con el calculo por frame.

                if (currentShootingTime >= maxShootingTime)
                {
                    isOverheat = true;
                }
            }
            else //si no esta disparando, resta. 
            {
                if (currentShootingTime >= 0)
                    currentShootingTime -= Time.deltaTime / 3f;
            }

            OnOverHeat();

            if(isShooting && !isOverheat)
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
        if (isOverheat)
        {
            shootingParticles.Play();

            PlayOverheatSound();

            if (currentShootingTime <= 0)
                isOverheat = false;
        }
    }

    private void PlayOverheatSound()
    {
        timeToPlayOverheatSound -= Time.deltaTime;
        if (timeToPlayOverheatSound <= 0)
        {
            AudioManager.instance.PlaySound(SoundClips.Overheat);
        }
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
