using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;

    /* SE VAN A DESCOMENTAR CUANDO ESTEN ARMADOS */
    //[SerializeField] private AudioSource musicLevel = null;
    //[SerializeField] private float lowerVolume = 1f;

    [Header("PauseMenu Settings")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    //[SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    //Extras
    private bool isActive;

    void Start()
    {
        resumeButton.onClick.AddListener(OnResumeHandler);
        restartButton.onClick.AddListener(OnRestartHandler);
        //mainMenuButton.onClick.AddListener(OnMenuHandler);
        quitButton.onClick.AddListener(OnQuitHandler);
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
        Time.timeScale = 0;
        GameManager.instance.IsGameFreeze = true;
        isActive = true;
        pauseMenu.SetActive(true);
        //musicLevel.volume -= lowerVolume;
        //timerObject.SetActive(false);
    }

    private void ExitMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.IsGameFreeze = false;
        isActive = false;
        pauseMenu.SetActive(false);
        //musicLevel.volume += lowerVolume;
        //timerObject.SetActive(true);
    }

    private void OnResumeHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        ExitMenu();
    }

    private void OnRestartHandler()
    {
        Time.timeScale = 1;
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene(GameManager.instance.CurrentLevel);
    }

    private void OnMenuHandler()
    {
        Time.timeScale = 1;
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        //SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }
}
