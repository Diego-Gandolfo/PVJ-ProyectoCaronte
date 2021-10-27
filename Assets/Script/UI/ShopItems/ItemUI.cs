using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemPrice;
    [SerializeField] private Button buyButton;

    private ItemUISO itemSO;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyEvent);
    }

    private void BuyEvent()
    {
        if(LevelManager.instance.CrystalCounter >= itemSO.price)
        {
            buyButton.interactable = false;
            LevelManager.instance.RemoveCrystal(itemSO.price);
            HUDManager.instance.ShopManagerUI.Interact(itemSO);
            HUDManager.instance.ShopManagerUI.UpdateCounter();
        }
        else
        {
            HUDManager.instance.ShopManagerUI.DoWarningQuantityCrystals();
        }
    }

    public void SetItemInfo(ItemUISO item)
    {
        itemSO = item;
        itemName.text = item.title;
        itemImage.sprite = item.image;
        itemPrice.text = item.price.ToString();

        GetComponentInChildren<TooltipTrigger>().SetContent(item.description);
    }
}
