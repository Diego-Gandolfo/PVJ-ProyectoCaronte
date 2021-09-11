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
        //Vector3 pos;
        //pos.x = Input.mousePosition.x;
        //pos.y = Input.mousePosition.y;
        //pos.z = Vector3.Distance(firePoint.position, crossHair.transform.position);
        //pos = Camera.main.ScreenToWorldPoint(pos);

        //var bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bulletClone.transform.forward = firePoint.position - pos;

        //RaycastHit hit;

        //if(Physics.Raycast(crossHair.position, crossHair.forward, out hit, Mathf.Infinity))
        //{
        //    var bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //    print(hit.point);
        //    bulletClone.transform.forward = Vector3.Distance(hit.point, firePoint.position) > 0.5f ? (hit.point - firePoint.position) : bulletClone.transform.forward;
        //}

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            var bulletClone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            print(hit.point);
            bulletClone.transform.forward = firePoint.position - hit.point;
        }
    }
}
