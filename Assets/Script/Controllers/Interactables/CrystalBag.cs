using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class CrystalBag : MonoBehaviour, IInteractable
{
    private int crystals = 1;
    [SerializeField] private int maxCrystalsToCarry;
    private InteractableController interactableController;

    #region Events

    public event Action OnBackpackPickedUp;

    #endregion

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
        DialogueManager.Instance.SuscribeOnBackpackPickedUp(this);
    }

    public void SetCrystalQuantity(int number)
    {
        crystals = number;
        if(crystals >= maxCrystalsToCarry)
        {
            crystals = maxCrystalsToCarry;
        }
    }

    public void Interact()
    {
        LevelManager.instance.AddCrystalInPlayer(crystals);
        OnBackpackPickedUp?.Invoke();
        Destroy(gameObject); //TODO: Maybe a pool? 
    }
}
