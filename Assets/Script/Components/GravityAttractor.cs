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
        for (int i = _gravityBodysList.Count - 1; i >= 0; i--)
        {
            var body = _gravityBodysList[i]; // agarramos el body a usar

            if (body == null) // si es nulo es que fue destruido
            {
                _gravityBodysList.RemoveAt(i); // entonces lo quitamos de la lista
                return; // nos salteamos el resto del for
            }

            var gravityUp = (body.transform.position - transform.position).normalized; // determinamos cual es el vector up

            body.Rigidbody.AddForce(gravityUp * _gravity); // le aplicamos la fuerza

            body.transform.rotation = Quaternion.FromToRotation(body.transform.up, gravityUp) * body.transform.rotation; // lo rotamos para que quede "vertical" con el vector up
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
