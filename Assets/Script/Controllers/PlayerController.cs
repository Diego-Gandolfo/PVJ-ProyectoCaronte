using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialize Fields

    [Header("Movement")]
    [SerializeField] private float speed; // Esta es la que se llamaba maxSpeed, pero el nombre no era descriptivo
    [SerializeField] private float sprintMultiplyer;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSensibility;

    [Header("Jump")]
    [SerializeField] private float jumpImpulseForce; // El impulso que se le dara al saltar

    [Header("Attack")]
    [SerializeField] private MachineGun weapon;

    #endregion

    #region Private Fields

    // Components
    private Animator animator;
    private Rigidbody rigidBody;

    // Movement
    private bool canMove;
    private float currentSpeed; // Esta es la que antes se llamaba speed, pero no tiene sentido que sea serializada si en el Start se le pisa el valor

    // Rotation
    private bool canRotate;
    private float rotX;
    private float rotY;

    // Jump
    private bool canJump; // Si tiene permitido saltar

    #endregion

    #region Unity Methods

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        Initialize();
    }

    private void Update()
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

    #endregion

    #region Private Methods

    private void Initialize()
    {
        currentSpeed = speed;
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
            currentSpeed = currentSpeed * sprintMultiplyer;
            animator.speed *= 2;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
            animator.speed /= 2;
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
