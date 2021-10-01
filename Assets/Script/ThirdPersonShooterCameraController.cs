using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//using StarterAssets;

public class ThirdPersonShooterCameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask target;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firepoint;

    private Vector3 mouseWorldPosition;
    private Vector3 worldAimTarget;

    void Start()
    {
        InputController.instance.OnAim += OnAim;
        InputController.instance.OnShoot += Shoot;
    }

    void Update()
    {
        mouseWorldPosition = Vector3.zero;
    }

    public void OnAim(bool value)
    {
        aimVirtualCamera.gameObject.SetActive(value);

        worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
    }

    public void Shoot(bool value)
    {
        if (value)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, target))
            {
                mouseWorldPosition = raycastHit.point;

                Vector3 aimDir = (mouseWorldPosition - firepoint.position).normalized;
                Instantiate(bulletPrefab, firepoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
        }
    }

}
