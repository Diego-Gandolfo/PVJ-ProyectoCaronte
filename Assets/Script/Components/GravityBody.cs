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
    public GravityAttractor Attractor => _attractor;

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
}
