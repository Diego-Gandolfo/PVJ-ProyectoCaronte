using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected PlayerController player;

    protected void RecognizePlayer()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public virtual void AttackPlayer()
    {
    } 
}
