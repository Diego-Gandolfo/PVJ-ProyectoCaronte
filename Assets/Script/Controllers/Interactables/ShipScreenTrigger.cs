using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class ShipScreenTrigger : MonoBehaviour, IInteractable
{
    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact()
    {
        if (!LevelManager.instance.HasAllShipItems)
        {
            HUDManager.instance.SetShipScreenVisible(true);
            HUDManager.instance.ShipManagerUI.CheckShipItems();
            AudioManager.instance.PlaySound(SoundClips.UIPopUp);
        }
        else
        {
            LevelManager.instance.Victory(); // TODO: cambiar cuando definamos el final
        }
    }
}
