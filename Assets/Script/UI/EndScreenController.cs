using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    enum ScreenType
    {
        Victory,
        GameOver
    }

    [SerializeField] private ScreenType screen;

    [Header("Buttons")]
    //[SerializeField] private Button nextLevelButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainHandler);
        menuButton.onClick.AddListener(OnMenuHandler);
        exitButton.onClick.AddListener(OnQuitHandler);

        //if (screen == ScreenType.Victory)
        //{
        //    nextLevelButton.onClick.AddListener(OnNextLevelHandler);

        //    if (GameManager.instance.NextLevel == string.Empty)
        //    {
        //        nextLevelButton.interactable = false;
        //    }
        //}

    }

    private void OnPlayAgainHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene(GameManager.instance.CurrentLevel);
    }

    private void OnMenuHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        SceneManager.LoadScene("MainMenu");
    }

    //private void OnNextLevelHandler()
    //{
    //    //AudioManager.instance.PlaySound(SoundClips.MouseClick);
    //    SceneManager.LoadScene(GameManager.instance.NextLevel);
    //}

    private void OnQuitHandler()
    {
        //AudioManager.instance.PlaySound(SoundClips.MouseClick);
        print("Se cierra el juego");
        Application.Quit();
    }
}
