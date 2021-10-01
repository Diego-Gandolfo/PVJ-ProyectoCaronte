using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string CurrentLevel { get; set; }
    public bool IsGameFreeze { get; set; }
    public PlayerController Player { get; private set; }

    public Action<PlayerController> OnPlayerAssing;

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
        CurrentLevel = "TerraplainLevel";
    }

    public void GameOver()
    {
        SetCursorActive(true);
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SetCursorActive(true);
        SceneManager.LoadScene("Victory");
    }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
        OnPlayerAssing?.Invoke(player);
    }

    public void SetCursorActive(bool value)
    {
        if (value)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
