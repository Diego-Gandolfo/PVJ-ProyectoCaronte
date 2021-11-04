using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Static

    public static LevelManager instance;

    #endregion

    #region Serialized Fields

    [SerializeField] private string currentLevel = "TerraplainLevel";
    [SerializeField] private int currentCrystalsNeeded = 30;
    [SerializeField] private Transform respawnPoint;

    #endregion

    #region Private Fields

    //private UIQuestManager _questManager;

    #endregion

    #region Events

    public Action<PlayerController> OnPlayerAssing;
    public Action<int> OnCrystalUpdate;

    #endregion

    #region Propertys

    public PlayerController Player { get; private set; }
    public int CrystalCounter { get; private set; }
    public int CrystalsNeeded => currentCrystalsNeeded;

    #endregion

    #region Unity Methods

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
        //_questManager = HUDManager.instance.QuestManager;
    }

    #endregion

    #region Public Methods

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

        //if (CrystalCounter >= CrystalsNeeded && _questManager.IsMissionActive)
        //{
        //    _questManager.QuestVisible(false);
        //}
    }

    public void RemoveCrystal(int number)
    {
        CrystalCounter -= number;
        OnCrystalUpdate?.Invoke(CrystalCounter);
    }

    public void Respawn()
    {
        Player.transform.position = respawnPoint.position;
        print("respawn: " + respawnPoint.position);
    }

    #endregion
}
