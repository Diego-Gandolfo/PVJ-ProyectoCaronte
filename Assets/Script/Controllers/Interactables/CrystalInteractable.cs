using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class CrystalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int value = 1;
    private InteractableController interactableController;

    void Start()
    {
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
    }

    public void Interact()
    {
        PlaySound();
        LevelManager.instance.AddCrystalInPlayer(value); //Agarramos el numero actual de contador y le sumamos uno.
        interactableController.ActivateOnce();
        Destroy(gameObject); //Nos destruimos //TODO: Maybe a pool?       
    }

    private void PlaySound()
    {
        AudioManager.instance.PlaySound(SoundClips.CrystalPickUp);
    }
}
