using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    #region Protected Fields

    protected PlayerController player;

    #endregion

    #region Protected Methods

    protected void RecognizePlayer()
    {
        player = GameManager.instance.Player;
    }

    #endregion

    private void Update()
    {
        if (player.GetComponent<HealthController>() != null)
        {
            Debug.LogWarning("HAY HEALTH CONTROLLER");
        }
    }

    #region Public Methods

    public virtual void AttackPlayer()
    {
    } 

    #endregion
}
