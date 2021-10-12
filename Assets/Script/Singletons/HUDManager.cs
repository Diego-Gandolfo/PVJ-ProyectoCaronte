using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private SonarManager sonarManager;
    [SerializeField] private UIQuestManager questUIManager;

    private LifeBarController lifeBar;
    private PromptTrigger promptTrigger;

    public static HUDManager instance;
    public SonarManager SonarManager => sonarManager;
    public ShopManagerUI ShopManagerUI { get; private set; }
    public OverHeatManager OverHeatManager { get; private set; }
    public PauseMenuController PauseMenu { get; private set; }
    public UIQuestManager QuestManager => questUIManager;

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
        ShopManagerUI = GetComponent<ShopManagerUI>();
        PauseMenu = GetComponent<PauseMenuController>();
    }

    private void SubscribeEvents()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        InputController.instance.OnAim += ShowCrosshair;
        InputController.instance.OnPause += OnPause;
    }

    private void OnPause()
    {
        if (ShopManagerUI.IsActive)
            ShopManagerUI.SetUIVisible(false);
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
        QuestManager.ShowBox(QuestManager.IsMissionActive);
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
        InputController.instance.OnAim -= ShowCrosshair;
        InputController.instance.OnPause -= OnPause;
    }
}
