using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected void RecognizePlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            Debug.Log("no hay PLAYER en la escena ! ! !");
        }
    }

    protected void FollowPlayer()
    {
        //TODO: Lógica de "Follow Player"
        //calcular distancia entre player y transform.position y de ahí un MoveTowards ?
    }
}
