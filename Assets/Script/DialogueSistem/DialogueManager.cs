using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Static

    public static DialogueManager Instance { get; private set; }

    #endregion

    #region Serialized Fields

    [Header("Scriptable Objects")]
    [SerializeField] private DialogueSO _firstBackpackSO;
    [SerializeField] private DialogueSO _firstCrystalSO;
    [SerializeField] private DialogueSO _firstDieByAbyssSO;
    [SerializeField] private DialogueSO _firstDieNoAbyssSO;
    [SerializeField] private DialogueSO _firstPartOfShipSO;
    [SerializeField] private DialogueSO _introDialogueSO;
    [SerializeField] private DialogueSO _lastPartOfShipRepairedSO;
    [SerializeField] private DialogueSO _oxygenAdviceSO;

    #endregion

    #region Private Fields

    // Components
    private DialogueSystem _dialogueSystem;

    // Flags
    private bool _firstBackpackFlag;
    private bool _firstCrystalFlag;
    private bool _firstDieByAbyssFlag;
    private bool _firstDieNoAbyssFlag;
    private bool _firstPartOfShipFlag;
    //private bool _introDialogueFlag;
    private bool _lastPartOfShipRepairedFlag;
    private bool _oxygenAdviceFlag;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeFlags();
        LevelManager.instance.OnCrystalsInPlayerUpdate += OnCrystalsInPlayerUpdateHandler;
    }

    #endregion

    #region Private Methods

    private void InitializeFlags()
    {
        _firstBackpackFlag = false;
        _firstCrystalFlag = false;
        _firstDieByAbyssFlag = false;
        _firstDieNoAbyssFlag = false;
        _firstPartOfShipFlag = false;
        //_introDialogueFlag = false;
        _lastPartOfShipRepairedFlag = false;
        _oxygenAdviceFlag = false;
    }

    private void OnAsphyxiationHandler()
    {
        if (_oxygenAdviceFlag) return;

        _dialogueSystem.AddToDialogueQueue(_oxygenAdviceSO);
        _oxygenAdviceFlag = true;
    }

    private void OnBackpackPickedUpHandler()
    {
        if (_firstBackpackFlag) return;

        _dialogueSystem.AddToDialogueQueue(_firstBackpackSO);
        _firstBackpackFlag = true;
    }

    private void OnCrystalsInPlayerUpdateHandler(int value)
    {
        if (_firstCrystalFlag) return;

        _dialogueSystem.AddToDialogueQueue(_firstCrystalSO);
        _firstCrystalFlag = true;
    }

    private void OnDieByAbysshandler()
    {
        if (_firstDieByAbyssFlag) return;

        StartCoroutine(DieByAbyssCoroutine(5f));
        _firstDieByAbyssFlag = true;
    }

    private void OnShipItemPickedUpHandler()
    {
        if (_firstPartOfShipFlag) return;

        _dialogueSystem.AddToDialogueQueue(_firstPartOfShipSO);
        _firstPartOfShipFlag = true;
    }

    private void OnCompletedHandler()
    {
        if (_lastPartOfShipRepairedFlag) return;

        _dialogueSystem.AddToDialogueQueue(_lastPartOfShipRepairedSO);
        _lastPartOfShipRepairedFlag = true;
    }

    #endregion

    #region Public Methods

    public void SetDialogueSystem(DialogueSystem dialogueSystem)
    {
        _dialogueSystem = dialogueSystem;
    }

    public void StartIntroDialogue()
    {
        _dialogueSystem.AddToDialogueQueue(_introDialogueSO);
    }

    public void SuscribeOnAsphyxiation(OxygenSystemController oxygenSystemController)
    {
        oxygenSystemController.OnAsphyxiation += OnAsphyxiationHandler;
    }

    public void SuscribeOnBackpackPickedUp(CrystalBag crystalBag)
    {
        crystalBag.OnBackpackPickedUp += OnBackpackPickedUpHandler;
    }

    public void SuscribeOnDieByAbyss(DeathZone deathZone)
    {
        deathZone.OnDieByAbyss += OnDieByAbysshandler;
    }

    public void SuscribeOnShipItemPickedUp(ShipItemInteractable shipItemInteractable)
    {
        shipItemInteractable.OnShipItemPickedUp += OnShipItemPickedUpHandler;
    }

    public void SuscribeOnCompleted(ShipItemsManager shipItemsManager)
    {
        shipItemsManager.OnCompleted += OnCompletedHandler;
    }

    #endregion

    #region Coroutines

    IEnumerator DieByAbyssCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueSystem.AddToDialogueQueue(_firstDieByAbyssSO);
    }

    #endregion
}
