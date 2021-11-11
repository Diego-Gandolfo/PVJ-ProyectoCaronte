using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    #region Unity Methods

    private void OnTriggerEnter(Collider other)
    {
        var healthController = other.gameObject.GetComponent<HealthController>(); // TODO: acceder al actor y sacar de ahi el HealthController

        if (healthController == null)
        {
            healthController = other.gameObject.GetComponentInChildren<HealthController>();
        }

        if (healthController != null)
        {
            healthController.DieByAbyss();
        }
    }

    #endregion
}
