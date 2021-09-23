using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;


    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSensibility;

    #region KeyCodes
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";
    private KeyCode jump = KeyCode.Space;
    private KeyCode shoot = KeyCode.Mouse0;
    private KeyCode aiming = KeyCode.Mouse1;
    private KeyCode pause = KeyCode.Escape;
    private KeyCode sprint = KeyCode.LeftShift;
    #endregion

    #region Events
    public Action OnPause;
    public Action OnShoot;
    public Action OnPhysicalAttack;
    public Action OnDash;
    public Action OnJump;
    public Action OnSprint;
    public Action<float, float> OnMove;
    public Action<float> OnRotate;
    #endregion

    #region Unity
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        CheckPause();

        if (!GameManager.instance.IsGameFreeze)
        {
            CheckMovement();
            CheckRotate();
            CheckJump();
            CheckSprint();
            CheckShoot();
            CheckAiming();
        }
    }
    #endregion

    #region Private

    private void CheckRotate()
    {
        var rotX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility.x;
        if (rotX >= 360) 
            rotX = 0;
        
        OnRotate?.Invoke(rotX);
    }   

    private void CheckMovement()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);
        OnMove?.Invoke(horizontal, vertical);
    }
    private void CheckShoot()
    {
        if (Input.GetKeyDown(shoot))
            OnShoot?.Invoke();
    }

    private void CheckAiming()
    {
        if (Input.GetKeyDown(aiming))
            OnPhysicalAttack?.Invoke();
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(jump))
            OnJump?.Invoke();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(pause))
            OnPause?.Invoke();
    }

    private void CheckSprint()
    {
        if (Input.GetKeyDown(sprint))
            OnSprint?.Invoke();
    }
    #endregion
}
