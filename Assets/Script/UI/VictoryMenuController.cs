using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenuController : MonoBehaviour
{
    #region Serialized Fields

    [Header("Settings")]
    [SerializeField] private int _mainMenuScene;
    [SerializeField] private bool _endAnimationPlay;

    [Header("UI")]
    [SerializeField] private Button _continueButton = null;
    [SerializeField] private Text _messageText;
    [SerializeField] private Text _crystalsPickedUpText;
    [SerializeField] private Text _crystalsSpentText;
    [SerializeField] private Text _crystalsDeliveredText;

    #endregion

    #region Private Fields

    // Components
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

        SetTextsUI();
    }

    #endregion

    #region Private Methods

    private void SetTextsUI()
    {
        var gameManager = GameManager.instance;
        var crystalsPickedUp = gameManager.ReportCrystalsInPlayer + gameManager.ReportCrystalsInBank + gameManager.ReportCrystalsSpent;
        var crystalsDelivered = crystalsPickedUp - gameManager.ReportCrystalsSpent;

        _crystalsPickedUpText.text = crystalsPickedUp.ToString();
        _crystalsSpentText.text = gameManager.ReportCrystalsSpent.ToString();
        _crystalsDeliveredText.text = crystalsDelivered.ToString();

        if (crystalsPickedUp == gameManager.ReportTotalCrystalsInLevel) // No se me ocurrió una forma más prolija de hacer esto, después se puede refactorizar
        {
            _messageText.text = "You have collected all the crystals of Caronte!";

            if (crystalsDelivered == crystalsPickedUp)
            {
                _messageText.text += "\nAnd you haven't spent a single one! You are a philanthropist!";
            }
            else if (crystalsDelivered >= (crystalsPickedUp / 2))
            {
                _messageText.text += "\nAnd you have spent very little! Great job!";
            }
            else if (crystalsDelivered < (crystalsPickedUp / 2))
            {
                _messageText.text += "\nBut you've spent a lot ... Still, good job!";
            }
        }
        else if (crystalsPickedUp == 0)
        {
            _messageText.text = "You haven't got a single crystal, we expected more from you.";
        }
        else if (crystalsPickedUp >= (gameManager.ReportTotalCrystalsInLevel / 2))
        {
            _messageText.text = "You have collected more than half of the crystals of Caronte!";

            if (crystalsDelivered == crystalsPickedUp)
            {
                _messageText.text += "\nAnd you have not spent anything! Great job!";
            }
            else if (crystalsDelivered >= (crystalsPickedUp / 2))
            {
                _messageText.text += "\nAnd you've tried not to spend too much. Good job!";
            }
            else if (crystalsDelivered < (crystalsPickedUp / 2))
            {
                _messageText.text += "\nBut you've spent a lot ... Still, nice job!";
            }
        }
        else if (crystalsPickedUp < (gameManager.ReportTotalCrystalsInLevel / 2))
        {
            _messageText.text = "You collected less than half of the available crystals...";

            if (crystalsDelivered == crystalsPickedUp)
            {
                _messageText.text += "\nBut you haven't spent a single one, so good job!";
            }
            else if (crystalsDelivered > (crystalsPickedUp / 2))
            {
                _messageText.text += "\nAnd you've tried not to spend too much. Nice job!";
            }
            else if (crystalsDelivered <= (crystalsPickedUp / 2))
            {
                _messageText.text += "\nAnd you have spent quite a bit. We hope you do better next time.";
            }
        }
    }

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
