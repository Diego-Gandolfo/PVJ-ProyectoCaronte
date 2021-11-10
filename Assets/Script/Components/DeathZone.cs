using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    #region Events

    public event Action OnDieByAbyss;

    #endregion

    #region Unity Methods

    private void Start()
    {
        DialogueManager.Instance.SuscribeOnDieByAbyss(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var healthController = other.gameObject.GetComponent<HealthController>();

        if (healthController == null)
        {
            healthController = other.gameObject.GetComponentInChildren<HealthController>();
        }

        if (healthController != null)
        {
            if (healthController.gameObject.CompareTag("Player")) OnDieByAbyss?.Invoke();
            healthController.Die();
        }
    }

    #endregion
}
