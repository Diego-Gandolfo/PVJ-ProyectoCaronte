using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialize Fields

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSensibility;

    [Header("Jump")]
    [SerializeField] private float jumpImpulseForce;

    [Header("Attack")]
    [SerializeField] private MachineGun weapon;

    #endregion

    #region Private Fields

    // Components
    private Animator animator;
    private Rigidbody rigidBody;
    protected HealthController healthController;

    // Movement
    private bool canMove;
    private float currentSpeed;

    // Rotation
    private bool canRotate;
    private float rotX;

    #endregion

    #region Propertys

    public float MoveSpeed => moveSpeed;
    public float SprintSpeed => sprintSpeed;
    public float CurrentSpeed => currentSpeed;

    #endregion

    #region Unity Methods

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        healthController = GetComponent<HealthController>();
        healthController.OnDie.AddListener(OnDieListener);
        healthController.OnTakeDamage.AddListener(OnTakeDamage);
        healthController.SetLifeBar(HUDManager.instance.GetLifeBar()); //Seteamos una ves el LifeBar;
        Initialize();
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (canMove)
            {
                Move();
                Sprint();
            }

            if (canRotate)
            {
                Rotate();
            }

            if (CheckIsGrounded())
            {
                Jump();
            }
        }
    }

    #endregion

    #region Private Methods

    private void Initialize()
    {
        currentSpeed = moveSpeed;
        canMove = true;
        canRotate = true;
    }

    private void Move()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;

        if (canMove)
        {
            transform.position += movement * currentSpeed * Time.deltaTime;
        }

        animator.SetFloat("Speed", moveZ);
        animator.SetFloat("HorizontalSpeed", moveX);
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            animator.speed = 2f;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed;
            animator.speed = 1f;
        }
    }

    private void Rotate()
    {
        rotX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility.x;

        if (rotX >= 360) rotX = 0;

        transform.Rotate(transform.up, rotX, Space.World);

        // La otra parte, "Mouse Y", se hace en el Script LookUpDown
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var jumpForce = transform.up * jumpImpulseForce;
            rigidBody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
    
    private void OnTakeDamage()
    {
        //TODO: hacer la animacion del Player para TakeDamage
    }

    private void OnDieListener()
    {
        RespawnManager.instance.Respawn();
        healthController.ResetValues();
    }
    #endregion

    #region Public Methods
    public bool CheckIsGrounded()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, 0.25f))
        {
            if (hit.collider != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void SetCanMove(bool enableMovement)
    {
        canMove = enableMovement;
    }

    public void SetCanRotate(bool value)
    {
        canRotate = value;
    }

    public void WeaponShoot()
    {
        weapon.Shoot();
    }

    #endregion
}
