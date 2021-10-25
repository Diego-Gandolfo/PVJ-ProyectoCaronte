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

    private void Start()
    {
        buyButton.onClick.AddListener(BuyEvent);
    }

    private void BuyEvent()
    {

    }

    public void SetItemInfo()
    {

    }
}
