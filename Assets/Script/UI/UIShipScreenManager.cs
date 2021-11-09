using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShipScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplatePrefab = null;
    [SerializeField] private GameObject itemsContainer = null;
    [SerializeField] private ShipItemSO[] shipItems = null;
    [SerializeField] private Button closeButton = null;

    private List<GameObject> itemsList = new List<GameObject>();

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
}
