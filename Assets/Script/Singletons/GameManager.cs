using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string CurrentLevel { get; set; }
    public bool IsGameFreeze { get; set; }

    public PlayerController Player => player;
    private PlayerController player;

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

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }
}
