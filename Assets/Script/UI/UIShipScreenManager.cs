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
    private List<UIShipItem> uiShipItemList = new List<UIShipItem>();
    public Animator Animator { get; private set; }

    public int itemsNeeded => shipItems.Length;

    private void Awake()
    {
        Animator = GetComponent<Animator>();

        for (int i = 0; i < shipItems.Length; i++)
        {
            GameObject item = Instantiate(itemTemplatePrefab, itemsContainer.transform);
            var itemUI = item.GetComponent<UIShipItem>();
            uiShipItemList.Add(itemUI);
            itemUI.SetItemInfo(shipItems[i]);
            itemsList.Add(item);
        }
    }

    private void Start()
    {
        closeButton?.onClick.AddListener(OnCloseScreen);
    }

    public void CheckShipItems()
    {
        CheckInventory();
        SetButtonsColors();
    }

    public void OnCloseScreen()
    {
        HUDManager.instance.SetShipScreenVisible(false);
    }

    public void CheckInventory()
    {
        if(LevelManager.instance != null)
        {
            if (LevelManager.instance.ShipManager.PlayerInventory.Count > 0)
            {
                inventory.SetActive(true);
                for (int i = inventoryList.Count - 1; i >= 0; i--)
                {
                    Destroy(inventoryList[i]);
                }
                inventoryList.Clear();

                for (int i = 0; i < LevelManager.instance.ShipManager.PlayerInventory.Count; i++)
                {
                    GameObject item = Instantiate(inventoryPrefab, inventoryContainer.transform);
                    item.GetComponent<Image>().sprite = LevelManager.instance.ShipManager.PlayerInventory[i].image;
                    inventoryList.Add(item);
                }
            }
            else
            {
                inventory.SetActive(false);
            }
        }
    }

    public void SetButtonsColors()
    {
        for (int i = 0; i < uiShipItemList.Count; i++)
        {
            uiShipItemList[i].ChangeButtonColor(LevelManager.instance?.ShipManager.PlayerInventory);
        }
    }
}
