using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenuController : MonoBehaviour
{
    #region Serialized Fields

    [Header("Canvas")]
    [SerializeField] private GameObject _reportGO;

    [Header("Settings")]
    [SerializeField] private int _mainMenuScene;
    [SerializeField] private Button _continueButton = null;
    [SerializeField] private bool _endAnimationPlay;

    #endregion

    #region Private Fields

    private Animator _animator;

    #endregion

    #region Unity Methods

    void Start()
    {
        GameManager.instance.SetCursorActive(true);

        _animator = GetComponent<Animator>();

        _continueButton.onClick.AddListener(OnContinueHandler);

        if (_endAnimationPlay)
        {
            _animator.Play("EndAnimation");
        }
    }

    #endregion

    #region Private Methods

    private void OnContinueHandler()
    {
        AudioManager.instance.PlaySound(SoundClips.InteractableClick);
        GameManager.instance.SetCursorActive(false);
        _animator.Play("ContinueToMainMenu");
    }

    private void OnContinueToMainMenuHandler() // Se llama en un AnimationEvent del clip 'ContinueToMainMenu'
    {
        SceneManager.LoadScene(_mainMenuScene);
    }

    #endregion
}
