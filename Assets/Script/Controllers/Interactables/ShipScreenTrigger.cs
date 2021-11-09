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
        HUDManager.instance.SetShipScreenVisible(true);
        AudioManager.instance.PlaySound(SoundClips.UIPopUp);
    }
}
