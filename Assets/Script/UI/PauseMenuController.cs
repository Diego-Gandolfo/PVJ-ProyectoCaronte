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

    public bool IsActive { get; private set; }

    void Start()
    {
        resumeButton?.onClick.AddListener(OnResumeHandler);
        restartButton?.onClick.AddListener(OnRestartHandler);
        mainMenuButton?.onClick.AddListener(OnMenuHandler);
        quitButton?.onClick.AddListener(OnQuitHandler);
        ExitMenu();
    }

    public void CheckPause()
    {
        if (!IsActive)
            Pause();
        else
            ExitMenu();
    }

    private void Pause()
    {
        IsActive = true;
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);
        ChangeStatus();
        //musicLevel.volume -= lowerVolume;
    }

    private void ExitMenu()
    {
        IsActive = false;
        //AudioManager.instance.PlaySound(SoundClips.InteractableClick);
        //musicLevel.volume += lowerVolume;
        ChangeStatus();
    }

    private void ChangeStatus()
    {
        GameManager.instance.Pause(IsActive);
        GameManager.instance.SetCursorActive(IsActive);
        pauseMenu.SetActive(IsActive);
        HUDManager.instance.ShowHUD(!IsActive); //Es el contrario a lo que pasa en pause, si el resto esta activo, entonces el HUD no.
    }

    private void OnResumeHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);
        
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        ExitMenu();
    }

    private void OnRestartHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        GameManager.instance.Pause(false);
        GameManager.instance.SetCursorActive(false);
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene(GameManager.instance.CurrentLevel);
    }

    private void OnMenuHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        GameManager.instance.Pause(false);
        GameManager.instance.SetCursorActive(true);
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnQuitHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }
}
