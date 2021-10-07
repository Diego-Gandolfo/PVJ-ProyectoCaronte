using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;

    //[SerializeField] private float lowerVolume = 1f;

    [Header("PauseMenu Settings")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    //Extras
    private bool isActive;

    void Start()
    {
        resumeButton?.onClick.AddListener(OnResumeHandler);
        restartButton?.onClick.AddListener(OnRestartHandler);
        mainMenuButton?.onClick.AddListener(OnMenuHandler);
        quitButton?.onClick.AddListener(OnQuitHandler);
        InputController.instance.OnPause += CheckPause;
        ExitMenu();
    }

    private void CheckPause()
    {
        if (!HUDManager.instance.ShopManagerUI.IsActive)
        {
            if (!isActive)
            {
                Pause();
            }
            else
            {
                ExitMenu();
            }
        }
    }

    private void Pause()
    {
        isActive = true;
        ChangeStatus();
        //musicLevel.volume -= lowerVolume;
    }

    private void ExitMenu()
    {
        isActive = false;
        //musicLevel.volume += lowerVolume;
        ChangeStatus();
    }

    private void ChangeStatus()
    {
        GameManager.instance.Pause(isActive);
        GameManager.instance.SetCursorActive(isActive);
        pauseMenu.SetActive(isActive);
        HUDManager.instance.ShowHUD(!isActive); //Es el contrario a lo que pasa en pause, si el resto esta activo, entonces el HUD no.
    }

    private void OnResumeHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        ExitMenu();
    }

    private void OnRestartHandler()
    {
        GameManager.instance.Pause(false);
        GameManager.instance.SetCursorActive(false);
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene(GameManager.instance.CurrentLevel);
    }

    private void OnMenuHandler()
    {
        GameManager.instance.Pause(false);
        GameManager.instance.SetCursorActive(true);
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }

    private void OnDestroy()
    {
        InputController.instance.OnPause -= CheckPause;
    }
}
