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

    // Start is called before the first frame update
    void Start()
    {
        animator = owner.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("IsShooting", true);
                owner.SetCanMove(false);

            }
            else
            {
                animator.SetBool("IsShooting", false);
                owner.SetCanMove(true);
            }
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
    }
}
