using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private Vector3 rotationSensibility = new Vector3(300f, 300f, 0);
    [SerializeField] private float clampMin = -20;
    [SerializeField] private float clampMax = 20;

    public static InputController instance;

    private Vector2 rotation;

    #region KeyCodes
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";
    private KeyCode jump = KeyCode.Space;
    private KeyCode shoot = KeyCode.Mouse0;
    private KeyCode aiming = KeyCode.Mouse1;
    private KeyCode pause = KeyCode.Escape;
    private KeyCode sprint = KeyCode.LeftShift;
    private KeyCode action = KeyCode.E;
    #endregion

    #region Events
    public Action OnPause;
    public Action OnDash;
    public Action OnJump;
    public Action<bool> OnShoot;
    public Action<bool> OnAim;
    public Action<bool> OnSprint;
    public Action<float, float> OnMove;
    public Action<Vector2> OnRotate;
    public Action OnAction;
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
        CheckEscape();

        if (GameManager.instance != null && LevelManager.instance != null)
        {
            if (!GameManager.instance.IsGameFreeze && LevelManager.instance.Player != null)
            {
                CheckMovement();
                CheckRotate();
                CheckJump();
                CheckSprint();
                CheckShoot();
                CheckAiming();
                CheckAction();

                if (Input.GetKeyDown(KeyCode.F1))
                    LevelManager.instance.AddCrystal(1);
            }
        }

    }
    #endregion

    #region Private
    private void CheckRotate()
    {
        rotation.x += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility.x;
        if (rotation.x >= 360)
            rotation.x = 0;


        rotation.y += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensibility.y;
        rotation.y = Mathf.Clamp(rotation.y, clampMin, clampMax);

        OnRotate?.Invoke(rotation);
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

    private void CheckJump()
    {
        if (Input.GetKeyDown(jump))
            OnJump?.Invoke();
    }

    private void CheckEscape()
    {
        if (Input.GetKeyDown(pause))
        {
            OnPause?.Invoke();
        }
    }

    private void CheckSprint()
    {
        if (Input.GetKeyDown(sprint))
            OnSprint?.Invoke(true);
        else if (Input.GetKeyUp(sprint))
            OnSprint?.Invoke(false);
    }

    private void CheckAction()
    {
        if (Input.GetKeyDown(action))
            OnAction?.Invoke();
    }
    #endregion
}
