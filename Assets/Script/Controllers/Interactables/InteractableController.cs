using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableController : MonoBehaviour
{
    [SerializeField] private bool showPromptAlways;
    public IInteractable interactable;
    private Outline outline;
    private bool canInteract;
    private bool hasInteractedOnce;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        hasInteractedOnce = false;
        InputController.instance.OnAction += Interact;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteractedOnce && other.gameObject.layer == 6) //Si es un player
        {
            HUDManager.instance.ShowPrompt(true);
            outline.enabled = true;
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!hasInteractedOnce && other.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(false);
            outline.enabled = false;
            canInteract = false;
        }
    }

    private  void Interact()
    {
        if (canInteract)
        {
            if (!showPromptAlways)
            {
                HUDManager.instance.ShowPrompt(false); //Le sacamos el prompt 
                outline.enabled = false; //apagamos el outline
            }

            interactable.Interact(); //Acá es donde cada interactable hace los suyo  
        }
    }

    public void ActivateOnce()
    {
        hasInteractedOnce = true; //si solo queremos activarlo una sola vez llamamos a esta funcion
        canInteract = false;
        InputController.instance.OnAction -= Interact;
    }
}
