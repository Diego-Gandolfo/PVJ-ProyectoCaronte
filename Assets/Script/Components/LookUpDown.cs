using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpDown : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private float rotationSensibility = 300f;
    [SerializeField] private float clampMin = -20;
    [SerializeField] private float clampMax = 20;

    #endregion

    #region Private Fields

    private float rotY;

    #endregion

    #region Unity Methods

    private void Update()
    {
        rotY += Input.GetAxis("Mouse Y") * Time.deltaTime* rotationSensibility;
        rotY = Mathf.Clamp(rotY, clampMin, clampMax);
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(-rotY, angles.y, angles.z);
    }

    #endregion
}
