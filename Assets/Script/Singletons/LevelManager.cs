using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        GameManager.instance.SetPlayer(player);
    }
}
