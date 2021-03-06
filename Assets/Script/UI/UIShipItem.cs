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
    private Animator _animator;
    private ShipItemSO item;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        giveButton.onClick.AddListener(GiveEvent);
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
        print(giveButton.interactable);
        if (giveButton.interactable)
        {
            print(item.title);
            if (shipItemSOs.Contains(item))
            {
                buttonText.text = "give";
                _animator.SetBool("Found", true);
            }
            else
            {
                _animator.SetBool("Found", false);
            }
        }
    }
}
