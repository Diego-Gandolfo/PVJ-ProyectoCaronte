using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InteractableController))]

public class OxigenCapsuleController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject forceField;
    private InteractableController interactableController;
    private Animator animator;

    public event Action OnOxygenCapsuleActivation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
        Desactivate();
        DialogueManager.Instance.SuscribeOnOxygenCapsuleActivation(this);
    }
    public void Interact()
    {
        Activated();
    }

    private void Activated()
    {
        animator.SetBool("IsActive", true);
        forceField.SetActive(true);
        interactableController.ActivateOnce();
        OnOxygenCapsuleActivation?.Invoke();
        //HUDManager.instance.QuestManager.CrystalQuest();
        //QuestsManager.Instance?.StartCrystalQuest();
        AudioManager.instance.PlaySound(SoundClips.CapsuleActivated);
    }
    private void Desactivate()
    {
        animator.SetBool("IsActive", false);
        forceField.SetActive(false);
    }
}
