using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject hud = null;
    [SerializeField] private GameObject crosshair = null;
    [SerializeField] private SonarManager sonarManager = null;
    [SerializeField] private GameObject shopManager = null;
    [SerializeField] private GameObject shipManager = null;
    [SerializeField] private UICrystalCounter crystalCounter = null;
    [SerializeField] private GameObject dialogueSystem = null;

    private LifeBarController lifeBar;
    public LifeBarController LifeBar { get => lifeBar; }

    private PromptTrigger promptTrigger;
    private bool isShopActive;
    private bool isShipActive;

    public static HUDManager instance;
    public SonarManager SonarManager => sonarManager;
    public ShopManagerUI ShopManagerUI { get; private set; }
    public UIShipScreenManager ShipManagerUI { get; private set; }
    public OverHeatManager OverHeatManager { get; private set; }
    public PauseMenuController PauseMenu { get; private set; }
    public UICrystalCounter UICrystal => crystalCounter;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SubscribeEvents();
        GetAllComponents();
        ShowCrosshair(false);
    }

    private void GetAllComponents()
    {
        lifeBar = GetComponent<LifeBarController>();
        promptTrigger = GetComponent<PromptTrigger>();
        OverHeatManager = GetComponent<OverHeatManager>();
        PauseMenu = GetComponent<PauseMenuController>();
        ShopManagerUI = shopManager.GetComponent<ShopManagerUI>();
        ShipManagerUI = shipManager.GetComponent<UIShipScreenManager>();
        dialogueSystem.SetActive(true);
        SetShopScreenVisible(false);
        SetShipScreenVisible(false);
    }

    private void SubscribeEvents()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        InputController.instance.OnPause += OnPause;
    }

    private void OnPause()
    {
        if (isShopActive || isShipActive)
        {
            SetShopScreenVisible(false);
            SetShipScreenVisible(false);
        }
        else
        {
            PauseMenu.CheckPause();
        }
    }

    public void ShowPrompt(bool value)
    {
        promptTrigger.ShowPrompt(value);
    }

    public void ShowCrosshair(bool value)
    {
        crosshair.SetActive(value);
    }

    public void ShowHUD(bool value)
    {
        hud.SetActive(value);
    }

    public void SetShopScreenVisible(bool value)
    {
        isShopActive = value;
        shopManager.SetActive(value);
        SetInteractiveScreenVisible(value);
    }

    public void SetShipScreenVisible(bool value)
    {
        isShipActive = value;
        shipManager.SetActive(value);
        SetInteractiveScreenVisible(value);
    }

    public void SetInteractiveScreenVisible(bool value)
    {
        TooltipSystem.instance.Reset();
        ShowHUD(!value);
        GameManager.instance.Pause(value);
        GameManager.instance.SetCursorActive(value);
    }

    public LifeBarController GetLifeBar()
    {
        return lifeBar;
    }

    protected void OnPlayerAssing(PlayerController player)
    {
        LevelManager.instance.OnPlayerAssing -= OnPlayerAssing;
        lifeBar.SetHealthController(player.GetComponent<HealthController>());
    }

    private void OnDestroy()
    {
        InputController.instance.OnPause -= OnPause;
    }
}
