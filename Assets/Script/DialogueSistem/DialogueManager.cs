using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    #region Static

    public static DialogueManager Instance { get; private set; }

    #endregion

    #region Serialized Fields

    [Header("Scriptable Objects")]
    [SerializeField] private DialogueSO _firstBackpackSO;
    [SerializeField] private DialogueSO _firstCrystalSO;
    [SerializeField] private DialogueSO _lastCrystalSO;
    [SerializeField] private DialogueSO _firstDieByAbyssSO;
    [SerializeField] private DialogueSO _firstDieSO;
    [SerializeField] private DialogueSO _firstPartOfShipSO;
    [SerializeField] private DialogueSO _introDialogueSO;
    [SerializeField] private DialogueSO _lastPartOfShipRepairedSO;
    [SerializeField] private DialogueSO _oxygenAdviceSO;
    [SerializeField] private DialogueSO _firstOverheatSO;
    [SerializeField] private DialogueSO _firstOxygenCapsuleActivationSO;

    #endregion

    #region Private Fields

    // Components
    private DialogueSystem _dialogueSystem;

    // Flags
    private bool _firstBackpackFlag;
    private bool _firstCrystalFlag;
    private bool _lastCrystalFlag;
    private bool _firstDieByAbyssFlag;
    private bool _firstDieFlag;
    private bool _firstPartOfShipFlag;
    private bool _lastPartOfShipRepairedFlag;
    private bool _oxygenAdviceFlag;
    private bool _firstOverheat;
    private bool _firstOxygenCapsuleActivation;

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
        LevelManager.instance.OnLastCrystalPickedUp += OnLastCrystalPickedUpHandler;
    }

    #endregion

    #region Private Methods

    private void InitializeFlags()
    {
        _firstBackpackFlag = false;
        _firstCrystalFlag = false;
        _lastCrystalFlag = false;
        _firstDieByAbyssFlag = false;
        _firstDieFlag = false;
        _firstPartOfShipFlag = false;
        //_introDialogueFlag = false;
        _lastPartOfShipRepairedFlag = false;
        _oxygenAdviceFlag = false;
        _firstOxygenCapsuleActivation = false;
        _firstOverheat = false;
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

    private void OnLastCrystalPickedUpHandler()
    {
        if (_lastCrystalFlag) return;

        _dialogueSystem.AddToDialogueQueue(_lastCrystalSO);
        _lastCrystalFlag = true;
    }

    private void OnDieHandler()
    {
        if (_firstDieFlag) return;

        StartCoroutine(DieNoAbyssCoroutine(5f));
        _firstDieFlag = true;
    }

    private void OnDieByAbyssHandler()
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

    private void OnOxygenCapsuleActivationHandler()
    {
        if (_firstOxygenCapsuleActivation) return;

        _dialogueSystem.AddToDialogueQueue(_firstOxygenCapsuleActivationSO);
        _firstOxygenCapsuleActivation = true;
    }

    private void OnOverheatHandler()
    {
        if (_firstOverheat) return;

        _dialogueSystem.AddToDialogueQueue(_firstOverheatSO);
        _firstOverheat = true;
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

    public void SuscribeOnDie()
    {
        var healtController = LevelManager.instance.Player.HealthController;
        healtController.OnDie += OnDieHandler;
        healtController.OnDieByAbyss += OnDieByAbyssHandler;
    }

    public void SuscribeOnShipItemPickedUp(ShipItemInteractable shipItemInteractable)
    {
        shipItemInteractable.OnShipItemPickedUp += OnShipItemPickedUpHandler;
    }

    public void SuscribeOnCompleted(ShipItemsManager shipItemsManager)
    {
        shipItemsManager.OnCompleted += OnCompletedHandler;
    }

    public void SuscribeOnOverheat(MachineGun machineGun)
    {
        machineGun.OnOverheat += OnOverheatHandler;
    }

    public void SuscribeOnOxygenCapsuleActivation(OxigenCapsuleController oxigenCapsuleController)
    {
        oxigenCapsuleController.OnOxygenCapsuleActivation += OnOxygenCapsuleActivationHandler;
    }

    #endregion

    #region Coroutines

    IEnumerator DieByAbyssCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueSystem.AddToDialogueQueue(_firstDieByAbyssSO);
    }
    
    IEnumerator DieNoAbyssCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _dialogueSystem.AddToDialogueQueue(_firstDieSO);
    }

    #endregion
}
