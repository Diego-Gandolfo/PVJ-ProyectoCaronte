using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string CurrentLevel { get; set; }
    public bool IsGameFreeze { get; set; }

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        IsGameFreeze = false;
        CurrentLevel = "Demo_Final";
    }
}
