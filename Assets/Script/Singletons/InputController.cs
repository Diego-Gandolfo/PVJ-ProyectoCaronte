using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSensibility = new Vector3(300f, 100f, 0);

    public static InputController instance;

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
    public Action OnDash;
    public Action OnJump;
    public Action<bool> OnShoot;
    public Action<bool> OnAim;
    public Action<bool> OnSprint;
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
            PlayAimSound();
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
            OnShoot?.Invoke(true);
        else if (Input.GetKeyUp(shoot))
            OnShoot?.Invoke(false);
    }

    private void CheckAiming() // No se usa el GetKeyDown porque hay que updatear todo el tiempo el dato de si esta aimeando.
    {
        if (Input.GetKey(aiming))
            OnAim?.Invoke(true);
            
        else
            OnAim?.Invoke(false);
    }

    private void PlayAimSound()
    {
        if (Input.GetMouseButtonDown(1))
            AudioManager.instance.PlaySound(SoundClips.Aim);
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
            OnSprint?.Invoke(true);
        else if (Input.GetKeyUp(sprint))
            OnSprint?.Invoke(false);
    }
    #endregion
}
