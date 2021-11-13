using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShipItem : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button giveButton;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Text buttonText;
    [SerializeField] private TooltipTrigger trigger;
    [SerializeField] private string giveItem = "on machine";
    [SerializeField] private Animator _animator;
    private ShipItemSO item;

    private void Start()
    {
        giveButton.onClick.AddListener(GiveEvent);
        _animator = GetComponent<Animator>();
    }

    private void GiveEvent()
    {
        if(LevelManager.instance.ShipManager.CheckIfPlayerHasItem(item))
        {
            giveButton.interactable = false;
            buttonText.text = giveItem;
            LevelManager.instance.ShipManager.RemoveFromPlayer(item);
            LevelManager.instance.ShipManager.AddItemToShip(item);
            HUDManager.instance.ShipManagerUI.CheckInventory();
            //TODO: play some sound as feedback;
        }
        else
        {
            HUDManager.instance.ShipManagerUI.Animator.Play("ShipSystem_MissingItem");
            _animator?.Play("ShipButtonError");
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

    public void ChangeButtonColor(List<ShipItemSO> shipItemSOs)
    {
        if (giveButton.enabled)
        {
            if (shipItemSOs.Contains(item))
            {
                print(item.name + " lo tengo");
                _animator.SetBool("Status", false);
                buttonText.text = "give";
            }
            else
            {
                print(item.name + " no tengo");
                _animator.SetBool("Status", true);
                buttonText.text = "still missing";
            }
        }
    }
}
