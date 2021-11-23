using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject creditsMenu = null;
    [Header("MainMenu Settings")]
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button creditsButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private bool playAnimation;
    [SerializeField] private bool endAnimationPlay;

    private bool mainMenuCheck;
    [SerializeField] private string level01 = "TerraplainLevel";
    [SerializeField] private int mainMenuScene;

    [Header("Credits Settings")]
    [SerializeField] private Button goBackCreditsButton;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.instance.SetCursorActive(true);
        playButton.onClick.AddListener(OnPlayHandler);
        creditsButton.onClick.AddListener(OnCreditsHandler);
        goBackCreditsButton.onClick.AddListener(OnGoBackHandler);
        exitButton.onClick.AddListener(OnQuitHandler);
        OnGoBackHandler(); //Si o si, aseguramos que inicie en el MenuInicial
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !mainMenuCheck)//Esto es porque si no estan en uno de los sub menus, pueden volver para atras con Escape
            OnGoBackHandler();
        if(endAnimationPlay && playAnimation)
        {
            animator.Play("EndAnimation");
        }
    }

    private void OnPlayHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        GameManager.instance.SetCursorActive(false);

        if (playAnimation)
        {
            animator.Play("Enter To Game Cinematic");
            creditsButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(false);
        }
        else
        {
            OnPlayAnimationExecution();
        }

        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
    }

    private void OnPlayAnimationExecution()
    {
        SceneManager.LoadScene(level01);
    }
    private void BackToMainMenue()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    private void OnCreditsHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        mainMenu.SetActive(false);
        //helpMenu.SetActive(false);
        creditsMenu.SetActive(true);
        mainMenuCheck = false;
    }

    private void OnGoBackHandler()
    {
        //if (!firstTime) AudioManager.instance.PlaySound(SoundClips.MouseClick);
        mainMenu.SetActive(true);
        //helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenuCheck = true;
    }

    private void OnQuitHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);

        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }
}
