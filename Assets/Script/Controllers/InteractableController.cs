using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public IInteractable interactable;
    private Outline outline;
    private bool canInteract;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) //Si es un player
        {
            HUDManager.instance.ShowPrompt(true);
            outline.enabled = true;
            canInteract= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(false);
            outline.enabled = false;
            canInteract = false;
        }
    }

    private void Interact()
    {
        HUDManager.instance.ShowPrompt(false); //Le sacamos el prompt 
        outline.enabled = false; //apagamos el outline

        interactable.Interact(); //Acá es donde cada interactable hace los suyo  
    }
}
