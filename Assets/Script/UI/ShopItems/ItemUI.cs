using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemPrice;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Button buyButton;

    private ItemUIObject itemSO;

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
            //itemSO.interaction.Interact();
        }
        else
        {
            HUDManager.instance.ShopManagerUI.DoWarningQuantityCrystals();
        }
    }

    public void SetItemInfo(ItemUIObject item)
    {
        itemSO = item;
        itemName.text = item.title;
        itemImage.sprite = item.image;
        itemPrice.text = item.price.ToString();
        itemDescription.text = item.description;
    }
}
