using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class ShipItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ShipItemSO shipItemSO;

    public event Action OnShipItemPickedUp;

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
        DialogueManager.Instance.SuscribeOnShipItemPickedUp(this);
    }

    public void Interact()
    {
        LevelManager.instance.ShipManager.AddItemToPlayer(shipItemSO);
        //TODO: Play some sound as feedback! Maybe add a symbol on screen???
        OnShipItemPickedUp?.Invoke();
        Destroy(gameObject);
    }
}
