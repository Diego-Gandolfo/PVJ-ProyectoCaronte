using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField]private float maxSpeed;
    [SerializeField] private float sprintMultiplyer;
    private Animator animator;
    private Rigidbody rigidBody;
    private bool canMove;
    [SerializeField] private Vector3 rotationSensibility;
    private float rotX;
    private float rotY;
    [SerializeField] private MachineGun weapon;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
        Move();
        }

        rotX += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility.x;
        rotY += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensibility.y;
        rotY = Mathf.Clamp(rotY, -20, 20);
        if(rotX >= 360)
        {
            rotX = 0;
        }
        transform.rotation = Quaternion.Euler(new Vector3(-rotY, rotX,0 ));
        Sprint();
    }
    
    void Move()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * moveX + transform.forward * moveZ; 
        if (canMove)
        {
        transform.position += movement * speed * Time.deltaTime;

        }
        animator.SetFloat("Speed", moveZ);
        animator.SetFloat("HorizontalSpeed", moveX);
    }
    public void SetCanMove(bool enableMovement)
    {
        canMove = enableMovement;
    }
    public void WeaponShoot()
    {
        weapon.Shoot();
    }
    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * sprintMultiplyer;
            animator.speed *= 2;
                
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = maxSpeed;
            animator.speed /= 2;
        }
    }

}
