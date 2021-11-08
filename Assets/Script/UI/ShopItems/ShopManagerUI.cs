using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject shopManager = null;
    [SerializeField] private GameObject itemTemplatePrefab = null;
    [SerializeField] private GameObject itemsContainer = null;
    [SerializeField] private BaseItemShop[] itemScriptableObject = null; 
    [SerializeField] private Text currentCrystalsInPlayer = null;
    [SerializeField] private Text currentCrystalsInBank = null;
    [SerializeField] private Button saveInBankButton = null;
    [SerializeField] private Button closeButton = null;

    private List<GameObject> itemsList = new List<GameObject>();
    private Animator _animator;

    public bool IsActive { get; private set; }

    private void Start()
    {
        _animator = shopManager.GetComponent<Animator>();
        closeButton?.onClick.AddListener(OnCloseScreen);
        saveInBankButton?.onClick.AddListener(SaveToBank);

        for (int i = 0; i < itemScriptableObject.Length; i++)
        {
            if (!itemScriptableObject[i].SO.isDisabled)
            {
                GameObject item = Instantiate(itemTemplatePrefab, itemsContainer.transform);
                var itemUI = item.GetComponent<ItemUI>();
                itemUI.SetItemInfo(itemScriptableObject[i]);
                itemsList.Add(item);
            }
        }
    }

    public void UpdateCounter()
    {
        currentCrystalsInPlayer.text = LevelManager.instance.CrystalsInPlayer.ToString();
        currentCrystalsInBank.text = LevelManager.instance.CrystalsInBank.ToString();
    }

    public void OnCloseScreen()
    {
        HUDManager.instance.SetShopVisible(false);
    }

    public void SaveToBank()
    {
        if(LevelManager.instance.CrystalsInPlayer > 0)
        {
            LevelManager.instance.SetCrystalsInBank(LevelManager.instance.CrystalsInPlayer);
            //AudioManager.instance.PlaySound(SoundClips.Negative); //TODO: Add special sound
            UpdateCounter();
        }
        else
        {
            AudioManager.instance.PlaySound(SoundClips.Negative);
        }
    }

    public void DoWarningQuantityCrystals()
    {
        AudioManager.instance.PlaySound(SoundClips.Negative);
        _animator.SetTrigger("MessageError");
    }
}
