using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string currentLevel = "TerraplainLevel";
    [SerializeField] private int currentCrystalsNeeded = 30;
    [SerializeField] private Transform respawnPoint;

    public static LevelManager instance;

    public PlayerController Player { get; private set; }
    public int CrystalCounter { get; private set; }
    public int CrystalsNeeded => currentCrystalsNeeded;

    public Action<PlayerController> OnPlayerAssing;
    public Action<int> OnCrystalUpdate;


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

    public void AddCrystal(int number)
    {
        CrystalCounter += number;
        OnCrystalUpdate?.Invoke(CrystalCounter);

        if (CrystalCounter >= CrystalsNeeded)
            HUDManager.instance.QuestManager.DeliverQuest();
    }

    public void RemoveCrystal(int number)
    {
        CrystalCounter -= number;
        OnCrystalUpdate?.Invoke(CrystalCounter);
    }

    public void Respawn()
    {
        Player.transform.position = respawnPoint.position;
    }
}
