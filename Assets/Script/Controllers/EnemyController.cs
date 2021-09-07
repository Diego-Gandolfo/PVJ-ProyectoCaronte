using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    #region Protected Fields
    protected PlayerController player;
    protected HealthController healthController;
    #endregion

    #region Protected Methods
    protected void Start()
    {
        healthController = GetComponent<HealthController>();
        healthController.OnDie.AddListener(OnDieListener);
    }

    protected void RecognizePlayer()
    {
        player = GameManager.instance.Player;
    }
    #endregion

    #region Public Methods
    public virtual void AttackPlayer()
    {
    }

    public virtual void OnDieListener()
    {
        float delay = 0.1f;
        Destroy(gameObject, delay);
    }
    #endregion
}
