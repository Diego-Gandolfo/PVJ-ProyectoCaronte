using UnityEngine;

public class CameraCollisionDetection : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private Vector3 _closePositionModifier;
    [SerializeField] private float _closeFOVModifier;

    #endregion

    #region Private Fields

    private Vector3 _closePosition;
    private float _closeFOV;
    private Vector3 _originalPosition;
    private float _originalFOV;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _originalPosition = transform.localPosition;
        _closePosition = _originalPosition + _closePositionModifier;

        _originalFOV = Camera.main.fieldOfView;
        _closeFOV = _originalFOV + _closeFOVModifier;
    }

    private void Update()
    {
        RaycastHit hit;

        if (!Physics.Linecast(transform.position, transform.forward, out hit, _hitLayers))
        {
            transform.localPosition = _closePosition;
            Camera.main.fieldOfView = _closeFOV;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, transform.forward, out hit, _hitLayers))
        {
            transform.localPosition = _originalPosition;
            Camera.main.fieldOfView = _originalFOV;
        }
    }

    #endregion
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionDetection : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    private RaycastHit hit;
    [SerializeField] private LayerMask hitLayers;
    void Update()
    {
        if (!Physics.Linecast(transform.position, transform.forward,out hit,hitLayers))
        {
            transform.position += Vector3.forward;

        }
    }
}
*/