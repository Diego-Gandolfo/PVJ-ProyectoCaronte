using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(OxygenSystemController))]
public class PlayerController : ActorController
{
    #region Serialize Fields
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Vector3 offset;

    [Header("Jump")]
    [SerializeField] private Transform[] jumpPoints;
    [SerializeField] private LayerMask surfaceList;

    [Header("Attack")]
    [SerializeField] private MachineGun weapon;

    [Header("Prefabs")]
    [SerializeField] private GameObject deathBag;
    [Header("Cristal Bag")]
    [SerializeField] private int cristalDroppedAmmount;
    #endregion

    #region Private Fields
    // Components
    private Rigidbody rigidBody;
    private OxygenSystemController oxygenSystem;
    [SerializeField]private DialogueTrigger introductionDialogue;
    // Movement
    private bool isUsingWeapon;
    private bool isPlayAimSound;
    private bool aiming;
    private bool shooting;
    private bool canPlaySound;
    private bool canDoDoubleJump;
    private bool isDoubleJumping;
    private float currentSpeed;
    private float distanceGround = 0.4f;
    private bool canMove = true;
    private float timeToPlaySound = 0.5f;
    private float currentTimeToPlaySound;
    #endregion

    #region Propertys
    public bool IsSprinting { get; private set; }

    public Action<bool, RaycastHit> IsShooting;

    #endregion
    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        oxygenSystem = GetComponent<OxygenSystemController>();
        currentSpeed = _actorStats.OriginalSpeed;
        weapon.SetPlayer(this);
    }

    private void Start()
    {
        canMove = true;
        GameManager.instance.SetCursorActive(false);
        LevelManager.instance.SetPlayer(this);
        SubscribeEvents();
        currentTimeToPlaySound = timeToPlaySound;
        InputController.instance.CanInteract(true);
        introductionDialogue.ReproduceDialogue();
    }

    private void Update()
    {
        CanMove();
        PlayStepSound();

        if (!weapon.IsOverheat)
        {
            DoShoot();
        }
        else
            IsInOverheat();


        if (Input.GetKeyDown(KeyCode.P)) 
            HealthController.TakeDamage(10); //TODO: BORRAR 
    }
    #endregion

    #region Private Methods
    private void SubscribeEvents()
    {
        InputController.instance.OnMove += Move;
        InputController.instance.OnRotate += Rotate;
        InputController.instance.OnShoot += OnShoot;
        InputController.instance.OnJump += Jump;
        InputController.instance.OnSprint += Sprint;
        InputController.instance.OnAim += IsAiming;
    }

    private void Move(float horizontal, float vertical)
    {
        if (!isUsingWeapon)
        {
            if (canMove)
            {

            Vector3 movement = (transform.right * horizontal + transform.forward * vertical).normalized;
            transform.position += movement * currentSpeed * Time.deltaTime;
                if (canPlaySound && CheckIfGrounded() /*&& !IsSprinting*/)
                {
                    if (horizontal != 0 || vertical != 0)
                    {
                        canPlaySound = false;
                        AudioManager.instance.PlaySound(SoundClips.Steps);
                        currentTimeToPlaySound = 0.0f;
                    }
                }
            }
            

        }

        animator.SetFloat("Speed", vertical);
        animator.SetFloat("HorizontalSpeed", horizontal);
        
    }

    private void PlayStepSound()
    {
        currentTimeToPlaySound += Time.deltaTime;
        if (currentTimeToPlaySound >= timeToPlaySound)
            canPlaySound = true;
        else canPlaySound = false;
    }

    private void Sprint(bool value)
    {
        if (!isUsingWeapon)
        {
            if (canMove)
            {
                IsSprinting = value;
                if (IsSprinting)
                {

                    currentSpeed = _actorStats.BuffedSpeed;
                    animator.speed = _actorStats.BuffedAnimatorSpeed;
                    //canPlaySound = false;
                    //AudioManager.instance.PlaySound(SoundClips.Steps);
                    //currentTimeToPlaySound = 0.0f;

                }
                else
                {
                    currentSpeed = _actorStats.OriginalSpeed;
                    animator.speed = _actorStats.OriginalAnimatorSpeed;
                }
            }

        } else
        {
            IsSprinting = false;
            currentSpeed = _actorStats.OriginalSpeed;
            animator.speed = _actorStats.OriginalAnimatorSpeed;
        }
    }

    private void Rotate(Vector2 rotation)
    {
        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }

    private void Jump()
    {
        if (!isUsingWeapon)
        {
            if (CheckIfGrounded())
            {
                DoJump();
            }
            else
            {
                if (!isDoubleJumping && canDoDoubleJump)
                {
                    isDoubleJumping = true;
                    DoJump();
                }
            }
        }
    }

    private void DoJump()
    {
        AudioManager.instance.PlaySound(SoundClips.Jump);

        var jumpForce = Vector3.up * _actorStats.JumpForce;
        rigidBody.AddForce(jumpForce, ForceMode.Impulse);
    }

    private bool CheckIfGrounded()
    {
        foreach (var jump in jumpPoints)
        {
            RaycastHit hit;
            Ray ray = new Ray(jump.position, -transform.up);

            if (Physics.Raycast(ray, out hit, distanceGround, surfaceList))
            {
                if (hit.collider != null)
                {
                    isDoubleJumping = false;
                    return true;
                }

            }
        }
        return false;
    }

    private void IsInOverheat()
    {
        IsAiming(false);
        shooting = false;
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlaySound(SoundClips.PlayerTakesDamage);
    }

    protected override void OnDie()
    {
        animator.Play("Die");
        canMove = false;
        InputController.instance.CanInteract(false);
        
    }
    private void Respawn()
    {
        base.OnDie();
        canMove = true;
        LevelManager.instance.Respawn();
        HUDManager.instance.UICrystal.ErrorAnimation();
        HealthController.ResetValues();
        oxygenSystem.ResetValues();
        InputController.instance.CanInteract(true);

    }
    private void DropableCrystales()
    {
        Vector3 position = transform.position;
        if(LevelManager.instance.CrystalsInPlayer > 0)
        {
            var current = LevelManager.instance.CrystalsInPlayer / 2; //Esto es  lo que le saca los cristales al jugador
            var bag = Instantiate(deathBag, position, Quaternion.identity);
            bag.GetComponent<CrystalBag>().SetCrystalQuantity(current);
            LevelManager.instance.RemoveCrystalsInPlayer(current); 
        }
    }

    private void IsAiming(bool value)
    {
        aiming = value;
        HUDManager.instance.ShowCrosshair(aiming);
        animator.SetBool("IsAiming", aiming);
        aimVirtualCamera.gameObject.SetActive(aiming);

        if (aiming && !weapon.IsOverheat)
        {
            if (!isPlayAimSound)
            {
                isPlayAimSound = true;
                AudioManager.instance.PlaySound(SoundClips.Aim);
            }
        }
        else
            isPlayAimSound = false;
    }

    private void OnShoot(bool value)
    {
        shooting = value;
    }

    private void DoShoot()
    {

        bool canShoot = ((shooting && aiming) || shooting);
        RaycastHit hit;
        Physics.Raycast(cam.transform.position + offset, cam.transform.forward, out hit, 999f, _attackStats.TargetList);
        IsShooting?.Invoke(canShoot, hit);
        animator.speed = 1f;
    }

    private void Shoot() //Mega necesario por un tema de como funciona la animacion del player, no se puede transferir al weapon, sigue chillando. 
    {
        AudioManager.instance.PlaySound(SoundClips.Shoot);
        weapon.Shoot();
    }
    private void CanMove()
    {
        if((aiming || shooting) && !weapon.IsOverheat)
        {
            isUsingWeapon = true;
            canMove = false;
        }
        else
        {
            isUsingWeapon = false;
            canMove = true;
        }
    }

    private void OnDestroy()
    {
        InputController.instance.OnMove -= Move;
        InputController.instance.OnRotate -= Rotate;
        InputController.instance.OnShoot -= OnShoot;
        InputController.instance.OnJump -= Jump;
        InputController.instance.OnSprint -= Sprint;
        InputController.instance.OnAim -= IsAiming;
    }

    #endregion

    public void SetCanDoDoubleJump()
    {
        canDoDoubleJump = true;
    }

    public void UpgradeMaxOygen(float oxygen)
    {
        oxygenSystem.SetMaxOxygen(oxygen);
    }
}