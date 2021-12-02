using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Static

    public static GameManager instance;

    #endregion

    #region Propertys

    public string CurrentLevel { get; set; }
    public bool IsGameFreeze { get; set; }

    // Estas son propiedades que se usan para el EndGame después se puede ver de hacer algo más bonito
    public int ReportTotalCrystalsInLevel { get; private set; }
    public int ReportCrystalsInPlayer { get; private set; }
    public int ReportCrystalsInBank { get; private set; }
    public int ReportCrystalsSpent { get; private set; }

    #endregion

    #region Unity Methods

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

        Pause(false);
    }

    #endregion

    #region Public Methods

    public void SetCursorActive(bool value)
    {
        if (value)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        if (value)
        {
            Time.timeScale = 0;
            //TODO: lower music
        }
        else
        {
            Time.timeScale = 1;
            //TODO: subir musica
        }
    }

    public void SetReportCrystalAmounts(int crystalInPlayer, int crystalInBank, int crystalSpent)
    {
        ReportCrystalsInPlayer = crystalInPlayer;
        ReportCrystalsInBank = crystalInBank;
        ReportCrystalsSpent = crystalSpent;
    }

    public void SetReportTotalCrystalsInLevel(int value)
    {
        ReportTotalCrystalsInLevel = value;
    }

    #endregion
}
