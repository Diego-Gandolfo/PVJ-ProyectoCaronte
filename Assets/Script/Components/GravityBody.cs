using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private GravityAttractor _gravityAttractor;

    #endregion

    #region Private Methods

    private Rigidbody _rigidbody;

    #endregion

    #region Propertys

    public Rigidbody Rigidbody => _rigidbody;
    public GravityAttractor GravityAttractor => _gravityAttractor;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (_gravityAttractor != null) AssignAttractor(_gravityAttractor);
        else Debug.LogWarning($"{gameObject.name} tiene el Componente 'GravityBody' pero no tiene asignado un Gravity Attractor.");
    }

    #endregion

    #region Public Methods

    public void AssignAttractor(GravityAttractor gravityAttractor)
    {
        if (_gravityAttractor != null) // si ya tiene un GravityAttractor
            _gravityAttractor.RemoveGravityBody(this); // que lo saque de la lista

        _gravityAttractor = gravityAttractor; // le asignamos el nuevo GravityAttractor

        _gravityAttractor.AddGravityBody(this); // lo agregamos a la lista

        _rigidbody.useGravity = false; // deshabilitamos la gravedad del Rigidbody
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // freezeamos la rotacion del Rigidbody
    }

    #endregion
}
