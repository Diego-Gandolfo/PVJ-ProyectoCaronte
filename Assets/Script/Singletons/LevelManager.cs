using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string currentLevel = "TerraplainLevel";

    public static LevelManager instance;

    public PlayerController Player { get; private set; }

    public Action<PlayerController> OnPlayerAssing;


    public void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        GameManager.instance.CurrentLevel = currentLevel;
    }

    public void SetPlayer(PlayerController player)
    {
        Player = player;
        OnPlayerAssing?.Invoke(player);
    }

    public void GameOver()
    {
        GameManager.instance.SetCursorActive(true);
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        GameManager.instance.SetCursorActive(true);
        SceneManager.LoadScene("Victory");
    }

}
