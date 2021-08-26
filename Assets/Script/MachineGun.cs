using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerController owner;
    public Animator animator;
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
