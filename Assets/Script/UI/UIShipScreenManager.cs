using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShipScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplatePrefab = null;
    [SerializeField] private GameObject itemsContainer = null;
    [SerializeField] private GameObject inventory = null;
    [SerializeField] private GameObject inventoryContainer = null;
    [SerializeField] private GameObject inventoryPrefab = null;
    [SerializeField] private ShipItemSO[] shipItems = null;
    [SerializeField] private Button closeButton = null;

    private List<GameObject> itemsList = new List<GameObject>();
    private List<GameObject> inventoryList = new List<GameObject>();

    public int itemsNeeded => shipItems.Length;

    private void Start()
    {
        closeButton?.onClick.AddListener(OnCloseScreen);

        for (int i = 0; i < shipItems.Length; i++)
        {
            GameObject item = Instantiate(itemTemplatePrefab, itemsContainer.transform);
            var itemUI = item.GetComponent<UIShipItem>();
            itemUI.SetItemInfo(shipItems[i]);
            itemsList.Add(item);
        }
    }

    public void OnCloseScreen()
    {
        HUDManager.instance.SetShipScreenVisible(false);
    }

    public void CheckInventory()
    {

        for (int i = 0; i < shipItems.Length; i++)
        {
            GameObject item = Instantiate(inventoryPrefab, inventoryContainer.transform);
            item.GetComponent<Image>().sprite = shipItems[i].image;
            inventoryList.Add(item);
        }
    }
}
