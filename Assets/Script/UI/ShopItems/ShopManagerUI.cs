using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Sonar,
    DoubleJump,
    OxygenBuff
}

public class ShopManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject shopManager;
    [SerializeField] private GameObject itemTemplatePrefab;
    [SerializeField] private GameObject itemsContainer;
    [SerializeField] private BaseItemShop[] itemScriptableObject; 
    [SerializeField] private Text currentCrystals;
    [SerializeField] private Button closeButton;

    private List<GameObject> itemsList = new List<GameObject>();
    private Animator _animator;

    public bool IsActive { get; private set; }

    private void Start()
    {
        _animator = shopManager.GetComponent<Animator>();
        closeButton?.onClick.AddListener(OnCloseScreen);

        for (int i = 0; i < itemScriptableObject.Length; i++)
        {
            GameObject item = Instantiate(itemTemplatePrefab, itemsContainer.transform);
            item.GetComponent<ItemUI>().SetItemInfo(itemScriptableObject[i]);
            itemsList.Add(item);
        }
    }

    public void UpdateCounter()
    {
        currentCrystals.text = LevelManager.instance.CrystalCounter.ToString(); ;
    }

    public void OnCloseScreen()
    {
        HUDManager.instance.SetShopVisible(false);
    }

    public void DoWarningQuantityCrystals()
    {
        AudioManager.instance.PlaySound(SoundClips.Negative);
        _animator.SetTrigger("MessageError");
    }
}
