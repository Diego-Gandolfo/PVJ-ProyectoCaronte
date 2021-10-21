using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject shopManager;
    [SerializeField] private Text currentCrystals;
    [SerializeField] private Text textQuest;
    [SerializeField] private Button finishButton;
    [SerializeField] private Button closeButton;

    private Animator _animator;

    public bool IsActive { get; private set; }

    private void Start()
    {
        _animator = shopManager.GetComponent<Animator>();
        shopManager.SetActive(IsActive);
        textQuest.text = $"Get the {LevelManager.instance.CrystalsNeeded} crystals";
        finishButton?.onClick.AddListener(OnFinishButton);
        closeButton?.onClick.AddListener(OnCloseScreen);
    }

    private void UpdateCounter()
    {
        currentCrystals.text = $"You have {LevelManager.instance.CrystalCounter} crystals";
    }

    public void SetUIVisible(bool value)
    {
        IsActive = value;
        UpdateCounter();
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
        {
            AudioManager.instance.PlaySound(SoundClips.Negative);
            _animator.SetTrigger("MessageError"); 
        }
    }
}
