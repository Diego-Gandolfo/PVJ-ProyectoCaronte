using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private GravityAttractor _attractor;

    #endregion

    #region Private Methods

    private Rigidbody _rigidbody;

    #endregion

    #region Propertys

    public Rigidbody Rigidbody => _rigidbody;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        if (_attractor != null) _attractor.AddGravityBody(this);
    }

    #endregion

    #region Public Methods

    public void AssignAttractor(GravityAttractor attractor)
    {
        _attractor = attractor;
        _attractor.AddGravityBody(this);
    }

    #endregion
}
