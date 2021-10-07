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
        OnDisable();
    }
    public void Interact()
    {
        OnEnable();
        
    }

    private void OnEnable()
    {
        animator.SetBool("IsActive", true);
        forceField.SetActive(true);
        interactableController.ActivateOnce();
        QuestsManager.Instance?.StartCrystalQuest();
    }
    private void OnDisable()
    {
        animator.SetBool("IsActive", false);
        forceField.SetActive(false);
    }
}
