using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform crossHair;
    [SerializeField] private float minDistance; // distancia minima para que calcule el forward del disparo con el crosshair
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxShootingTime;
    [SerializeField] private ParticleSystem shootingParticles;
    private bool canShoot;
    private bool isShooting;
    private bool isAiming;
    private float currentShootingTime;
    private Vector3 mouseWorldPosition;
    private Vector3 worldAimTarget;
    private Animator animator;

    void Start()
    {
        var particles = shootingParticles.main;
        particles.duration = maxShootingTime;
        crossHair.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (isShooting && canShoot)
            {
                animator.SetBool("IsShooting", true);

                currentShootingTime += Time.deltaTime; // sacar el time delta time por un tema de como funciona con el calculo por frame.
                
                if (currentShootingTime >= maxShootingTime)
                {
                    OnOverHeat();
                }
            }
            else
            {
                StopShooting();
            }

            if (currentShootingTime < 0)
                currentShootingTime = 0;
            HUDManager.instance.UpdateOverHeat(currentShootingTime, maxShootingTime);
            //Debug.DrawRay(crossHair.position, crossHair.forward,Color.red);
        }
    }

    public void Shoot()
    {
        Vector3 aimDir = (mouseWorldPosition - firePoint.position).normalized;
        var rotation = Quaternion.LookRotation(aimDir, Vector3.up);
        Instantiate(bulletPrefab, firePoint.position, rotation);
    }

    private void StopShooting()
    {
        if(currentShootingTime > 0)
        {
            currentShootingTime -= Time.deltaTime;
            canShoot = false;
        }
        else
            canShoot = true;

        animator.SetBool("IsShooting", false);
    }

    private void OnOverHeat()
    {
        shootingParticles.Play();
        StopShooting();
    }

    public void IsAiming(bool value)
    {
        crossHair.gameObject.SetActive(value);
        isAiming = value;
    }

    public void SetPlayer(PlayerController player)
    {
        player.IsShooting += CanShoot;
        animator = player.GetComponent<Animator>();
    }

    public void CanShoot(bool value, Vector3 position)
    {
        isShooting = value;
        mouseWorldPosition = position;
    }
}
