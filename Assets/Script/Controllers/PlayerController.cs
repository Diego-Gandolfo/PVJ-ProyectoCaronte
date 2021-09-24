using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(OxygenSystemController))]
public class PlayerController : ActorController
{
    #region Serialize Fields
    [Header("Jump")]
    [SerializeField] private Transform[] jumpPoints;
    [SerializeField] private LayerMask surfaceList;


    [Header("Attack")]
    [SerializeField] private MachineGun weapon;

    #endregion

    #region Private Fields
    // Components
    private Rigidbody rigidBody;
    private OxygenSystemController oxygenSystem;

    // Movement
    private bool isUsingWeapon;
    private float currentSpeed;
    private float distanceGround = 1.1f;
    #endregion

    #region Propertys
    public bool IsSprinting { get; private set; }

    public Action<bool> IsShooting;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        GameManager.instance.SetPlayer(this);
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        oxygenSystem = GetComponent<OxygenSystemController>();
        currentSpeed = _actorStats.OriginalSpeed;
        weapon.SetPlayer(this);
    }

    private void Start()
    {
        SubscribeEvents();
    }

    #endregion

    #region Private Methods
    private void SubscribeEvents()
    {
        InputController.instance.OnMove += Move;
        InputController.instance.OnRotate += Rotate;
        InputController.instance.OnShoot += CanShoot;
        InputController.instance.OnJump += Jump;
        InputController.instance.OnSprint += Sprint;
        InputController.instance.OnAim += IsAiming;
    }

    private void Move(float horizontal, float vertical)
    {
        if (!isUsingWeapon)
        {
            Vector3 movement = transform.right * horizontal + transform.forward * vertical;
            transform.position += movement * currentSpeed * Time.deltaTime;
        }
        animator.SetFloat("Speed", vertical);
        animator.SetFloat("HorizontalSpeed", horizontal);
    }

    private void Sprint(bool value)
    {
        if (!isUsingWeapon)
        {
            IsSprinting = value;
            if (IsSprinting)
            {
                currentSpeed = _actorStats.BuffedSpeed;
                animator.speed = 2f;
            }
            else
            {
                currentSpeed = _actorStats.OriginalSpeed;
                animator.speed = 1f;
            }
        }
    }

    private void Rotate(float rotX)
    {
        transform.Rotate(transform.up, rotX, Space.World); // La otra parte, "Mouse Y", se hace en el Script LookUpDown
    }

    private void Jump()
    {
        if (CheckIfGrounded())
        {
            var jumpForce = transform.up * _actorStats.JumpForce;
            rigidBody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private bool CheckIfGrounded()
    {
        bool answer = false;
        foreach (var jump in jumpPoints)
        {
            RaycastHit hit;
            Ray ray = new Ray(jump.position, -transform.up);

            if (Physics.Raycast(ray, out hit, distanceGround, surfaceList))
            {
                if (hit.collider != null)
                    answer = true;
            }
        }
        return answer;
    }

    protected override void OnDie()
    {
        base.OnDie();
        RespawnManager.instance.Respawn();
        HealthController.ResetValues();
        oxygenSystem.ResetValues();
    }

    private void IsAiming(bool value)
    {
        isUsingWeapon = value;
        weapon.IsAiming(value);
        animator.SetBool("IsAiming", value);
    }

    private void CanShoot(bool value) //Acá recibe el input de si esta disparando o no a traves de un GetKeyDown or GetKeyUp
    {
        IsShooting?.Invoke(value);
        isUsingWeapon = value;
        animator.speed = 1f;
    }

    private void Shoot() //Mega necesario por un tema de como funciona la animacion del player, no se puede transferir al weapon, sigue chillando. 
    {
        weapon.Shoot();
    }
    #endregion
}