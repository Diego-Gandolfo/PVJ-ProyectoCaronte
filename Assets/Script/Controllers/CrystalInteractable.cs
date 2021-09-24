using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class CrystalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 1;

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact()
    {
        UICrystalCounter.instance.AddCrystal(value); //Agarramos el numero actual de contador y le sumamos uno.
        Destroy(gameObject); //Nos destruimos        
    }
}
