using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private float _gravity = -9.81f;

    #endregion

    #region Private Fields

    private List<GravityBody> _gravityBodysList = new List<GravityBody>();
    private readonly float _rotationSpeed = 50f;

    #endregion

    #region Unity Methods

    private void Update()
    {
        AttractGravityBodys();
    }

    #endregion

    #region Private Methods

    private void AttractGravityBodys()
    {
        for (int i = 0; i < _gravityBodysList.Count; i++)
        {
            var body = _gravityBodysList[i];
            var gravityUp = (body.transform.position - transform.position).normalized;

            body.Rigidbody.AddForce(gravityUp * _gravity);

            body.transform.rotation = Quaternion.FromToRotation(body.transform.up, gravityUp) * body.transform.rotation;
        }
    }

    #endregion

    #region Public Methods

    public void AddGravityBody(GravityBody gravityBody)
    {
        _gravityBodysList.Add(gravityBody);
    }

    #endregion
}
