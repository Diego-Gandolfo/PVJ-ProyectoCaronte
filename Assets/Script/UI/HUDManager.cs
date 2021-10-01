using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private PromptTrigger promptTrigger;
    [SerializeField] private LifeBarController lifeBar;
    [SerializeField] private Image overHeatImage;
    [SerializeField] private UICrystalCounter crystalController;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private SonarManager sonarManager;
    private UIQuestManager questManager;

    public static HUDManager instance;

    public bool IsQuestVisible { get; private set; }
    public SonarManager SonarManager => sonarManager;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Esto lo comente porque como esta puesto en un hijo, no funciona
        }
    }

    private void Start()
    {
        SetCrosshair(false);
        GameManager.instance.OnPlayerAssing += OnPlayerAssing;
        InputController.instance.OnAim += SetCrosshair;
        questManager = GetComponent<UIQuestManager>();
        IsQuestVisible = true;
    }

    public void ShowPrompt(bool value)
    {
        promptTrigger.ShowPrompt(value);
    }

    public void ChangeCrystalAmount(int value)
    {
        //Do something
    }

    public LifeBarController GetLifeBar()
    {
        return lifeBar;
    }

    public void VisibleQuest(bool value)
    {
        questManager.QuestVisible(value);
        IsQuestVisible = value;
    }

    public void UpdateQuest(string message, string title = null)
    {
        questManager.UpdateMessage(message);
        if (title != null)
            questManager.UpdateTitle(title);
    }
    public void UpdateOverHeat(float currentHeat,float  maxHeat)
    {
        overHeatImage.fillAmount = (float)currentHeat / maxHeat;
    }

    public void SetCrosshair(bool value)
    {
        crosshair.SetActive(value);
    }

    protected void OnPlayerAssing(PlayerController player)
    {
        GameManager.instance.OnPlayerAssing -= OnPlayerAssing;
        lifeBar.SetHealthController(player.GetComponent<HealthController>());
    }
}
