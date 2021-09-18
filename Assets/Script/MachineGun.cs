using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerController owner;
    public Animator animator;
    [SerializeField]private Transform crossHair;
    [SerializeField] private float minDistance; // distancia minima para que calcule el forward del disparo con el crosshair
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float maxShootingTime;
    [SerializeField] private float currentShootingTime;
    [SerializeField] private ParticleSystem shootingParticles;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        animator = owner.GetComponent<Animator>();
        var particles = shootingParticles.main;
        particles.duration = maxShootingTime;
        crossHair.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (Input.GetKey(KeyCode.Mouse0) && canShoot)
            {
                animator.SetBool("IsShooting", true);
                owner.SetCanMove(false);
                currentShootingTime += Time.deltaTime;
                
                if(currentShootingTime >= maxShootingTime)
                {
                    OnOverHeat();
                }

            }
            else
            {
                StopShooting();
            }
        Debug.DrawRay(crossHair.position, crossHair.forward,Color.red);
        }
        if(currentShootingTime < 0)
        {
            currentShootingTime = 0;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            crossHair.gameObject.SetActive(false);
        }
    }
    public void Shoot()
    {
        RaycastHit hit;

        Physics.Raycast(crossHair.position, crossHair.forward, out hit, Mathf.Infinity, layerMask);
        var bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (Vector3.Distance(hit.point, crossHair.position) > minDistance)
        {
            bulletClone.transform.forward = hit.point - firePoint.position;
        }
        else if (hit.collider)
        {
            bulletClone.transform.forward = firePoint.transform.forward;
        }
    }
    private void StopShooting()
    {
        if(currentShootingTime > 0)
        {
        currentShootingTime -= Time.deltaTime;

        }
        else
        {
            canShoot = true;
        }
        animator.SetBool("IsShooting", false);
        owner.SetCanMove(true);
    }
    private void OnOverHeat()
    {
        shootingParticles.Play();
        StopShooting();
        canShoot = false;
    }
}
