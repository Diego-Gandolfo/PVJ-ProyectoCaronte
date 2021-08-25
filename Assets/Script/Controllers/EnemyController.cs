using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected PlayerController player;

    protected void RecognizePlayer()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)Debug.Log("no hay PLAYER en la escena ! ! !");
        else if (player != null) Debug.LogWarning("encontramos un Player ! ! !");
    }
}
