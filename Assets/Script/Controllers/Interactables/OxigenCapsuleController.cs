using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InteractableController))]

public class OxigenCapsuleController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject forceField;
    private InteractableController interactableController;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
        Desactivate();
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
        HUDManager.instance.QuestManager.CrystalQuest();
        //QuestsManager.Instance?.StartCrystalQuest();
    }
    private void Desactivate()
    {
        animator.SetBool("IsActive", false);
        forceField.SetActive(false);
    }
}
