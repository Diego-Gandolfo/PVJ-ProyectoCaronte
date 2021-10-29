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
    [SerializeField] private TooltipTrigger trigger;

    private BaseItemShop item;

    private void Start()
    {
        buyButton.onClick.AddListener(BuyEvent);
    }

    private void BuyEvent()
    {
        if(LevelManager.instance.CrystalCounter >= item.SO.price)
        {
            buyButton.interactable = false;
            LevelManager.instance.RemoveCrystal(item.SO.price);
            item.Interact();
            HUDManager.instance.ShopManagerUI.UpdateCounter();
        }
        else
        {
            HUDManager.instance.ShopManagerUI.DoWarningQuantityCrystals();
        }
    }

    public void SetItemInfo(BaseItemShop itemObject)
    {
        item = itemObject;
        itemName.text = item.SO.title;
        itemImage.sprite = item.SO.image;
        itemPrice.text = item.SO.price.ToString();

        trigger.SetContent(item.SO.description);
    }
}
