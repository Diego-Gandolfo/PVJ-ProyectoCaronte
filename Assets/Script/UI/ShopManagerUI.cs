using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject shopManager;
    [SerializeField] private Text text;
    [SerializeField] private Button finishButton;
    [SerializeField] private Button closeButton;
    public bool IsActive { get; private set; }

    private void Start()
    {
        SetUIVisible(false);
        SetText();
        finishButton?.onClick.AddListener(OnFinishButton);
        closeButton?.onClick.AddListener(OnCloseScreen);
    }

    private void SetText()
    {
        text.text = $"Get the {LevelManager.instance.CrystalsNeeded} crystals";
    }


    public void SetUIVisible(bool value)
    {
        IsActive = value;
        HUDManager.instance.ShowHUD(!IsActive);
        shopManager.SetActive(IsActive);
        GameManager.instance.Pause(IsActive);
        GameManager.instance.SetCursorActive(IsActive);
    }

    public void OnCloseScreen()
    {
        SetUIVisible(false);
    }

    public void OnFinishButton()
    {
        if (LevelManager.instance.CrystalCounter >= LevelManager.instance.CrystalsNeeded)
            LevelManager.instance.Victory();
        else
            print($"te faltan {LevelManager.instance.CrystalsNeeded - LevelManager.instance.CrystalCounter} cristales");
        //TODO: else give a negativew sound and feedback. 
    }
}
