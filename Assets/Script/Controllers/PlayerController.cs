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

    // Movement
    private bool canMove;
    private float currentSpeed;

    // Rotation
    private bool canRotate;
    private float rotX;
    private float rotY;

    // Jump
    private bool canJump;

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
        GetComponent<HealthController>().SetLifeBar(HUDManager.instance.GetLifeBar()); //Seteamos una ves el LifeBar;
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

            if (canJump)
            {
                Jump();
            }
            else
            {
                CheckGround();
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
        canJump = true;
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
        rotX += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility.x;
        rotY += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensibility.y;
        rotY = Mathf.Clamp(rotY, -20, 20);

        if (rotX >= 360)
        {
            rotX = 0;
        }

        transform.rotation = Quaternion.Euler(new Vector3(-rotY, rotX, 0));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;
            var jumpForce = transform.up * jumpImpulseForce;
            rigidBody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, .2f))
        {
            if (hit.collider != null)
            {
                canJump = true;
            }
        }
    }

    #endregion

    #region Public Methods

    public void SetCanMove(bool enableMovement)
    {
        canMove = enableMovement;
    }

    public void SetCanRotate(bool value)
    {
        canRotate = value;
    }

    public void SetCanJump(bool value)
    {
        canJump = value;
    }

    public void WeaponShoot()
    {
        weapon.Shoot();
    }

    #endregion
}
