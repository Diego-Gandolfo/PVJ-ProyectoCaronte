using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class ShopMachineController : MonoBehaviour, IInteractable
{
    private bool isVisible;

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact()
    {
        if (!isVisible)
            isVisible = true;
        else
            isVisible = false;

        HUDManager.instance.ShopManagerUI.SetUIVisible(isVisible);
    }
}
