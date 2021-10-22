using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class ShopMachineController : MonoBehaviour, IInteractable
{

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact()
    {
        HUDManager.instance.ShopManagerUI.SetUIVisible(true);
        AudioManager.instance.PlaySound(SoundClips.UIPopUp);
    }
}
