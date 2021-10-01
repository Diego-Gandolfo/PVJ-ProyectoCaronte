using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hud;

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
        ExitMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        GameManager.instance.Pause(true);
        GameManager.instance.SetCursorActive(true);
        Time.timeScale = 0;
        GameManager.instance.IsGameFreeze = true;
        isActive = true;
        hud.SetActive(false);
        pauseMenu.SetActive(true);
        //musicLevel.volume -= lowerVolume;
    }

    private void ExitMenu()
    {
        GameManager.instance.Pause(false);
        GameManager.instance.SetCursorActive(false);
        Time.timeScale = 1;
        GameManager.instance.IsGameFreeze = false;
        isActive = false;
        pauseMenu.SetActive(false);
        hud.SetActive(true);
        //musicLevel.volume += lowerVolume;
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
}
