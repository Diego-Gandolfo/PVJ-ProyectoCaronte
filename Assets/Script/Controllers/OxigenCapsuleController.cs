using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(InteractableController))]

public class OxigenCapsuleController : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject forceField;
    [SerializeField] private InteractableController interactableController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        OnDisable();
        interactableController = GetComponent<InteractableController>();
        interactableController.interactable = this;
    }
    public void Interact()
    {
        OnEnable();
        
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
        animator.SetBool("IsActive", true);
        forceField.SetActive(true);
        interactableController.ActivateOnce();
        DemoQuest.Instance.StartCrystalQuest();
    }
    private void OnDisable()
    {
        animator.SetBool("IsActive", false);
        forceField.SetActive(false);
    }


}
