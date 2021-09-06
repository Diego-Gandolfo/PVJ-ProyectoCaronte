using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleMove : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private float _runSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _jumpImpulseForce = 10f;

    private bool _isRunning;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private float _rotationY;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    private void FixedUpdate()
    {
        var speed = _isRunning ? _runSpeed : _walkSpeed;
        var move = _rigidbody.position + transform.TransformDirection(_moveDirection) * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(move);
    }

    private void Move()
    {
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void Rotate()
    {
        _rotationY = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;

        transform.Rotate(transform.up, _rotationY, Space.World);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var jumpForce = transform.up * _jumpImpulseForce;
            _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
}
