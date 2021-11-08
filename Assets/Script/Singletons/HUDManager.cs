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
    [SerializeField] private UICrystalCounter crystalCounter = null;

    private LifeBarController lifeBar;
    private PromptTrigger promptTrigger;
    private bool isShopActive;

    public static HUDManager instance;
    public SonarManager SonarManager => sonarManager;
    public ShopManagerUI ShopManagerUI { get; private set; }
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
        SetShopVisible(false);
    }

    private void SubscribeEvents()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        InputController.instance.OnPause += OnPause;
    }

    private void OnPause()
    {
        if (isShopActive)
            SetShopVisible(false);
        else
            PauseMenu.CheckPause();
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

    public void SetShopVisible(bool value)
    {
        isShopActive = value;
        TooltipSystem.instance.Reset();
        ShowHUD(!isShopActive);
        ShopManagerUI.UpdateCounter();
        shopManager.SetActive(isShopActive);
        GameManager.instance.Pause(isShopActive);
        GameManager.instance.SetCursorActive(isShopActive);
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
