using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class CrystalBag : MonoBehaviour, IInteractable
{
    private int crystals = 1;
    [SerializeField] private int maxCrystalsToCarry;
    private InteractableController interactableController;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
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
        LevelManager.instance.AddCrystal(crystals);
        Destroy(gameObject); //TODO: Maybe a pool? 
    }
}
