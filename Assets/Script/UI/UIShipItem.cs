using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShipItem : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button giveButton;
    [SerializeField] private Text buttonText;
    [SerializeField] private TooltipTrigger trigger;

    private ShipItemSO item;

    private void Start()
    {
        giveButton.onClick.AddListener(GiveEvent);
    }

    private void GiveEvent()
    {
        if(LevelManager.instance.ShipManager.CheckIfPlayerHasItem(item))
        {
            giveButton.interactable = false;
            buttonText.text = "on machine";
            LevelManager.instance.ShipManager.RemoveFromPlayer(item);
            LevelManager.instance.ShipManager.AddItemToShip(item);
            //TODO: play some sound as feedback;
        }
        else
        {
            AudioManager.instance.PlaySound(SoundClips.Negative);
        }
    }

    public void SetItemInfo(ShipItemSO itemObject)
    {
        item = itemObject;
        itemName.text = item.title;
        itemImage.sprite = item.image;

        if (item.description != "")
            trigger.SetContent(item.description);
    }
}
