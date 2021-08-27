using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickUpTrigger : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private Outline outline;
    private bool canPickUp;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canPickUp)
        {
            Interact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(true);
            outline.enabled = true;
            canPickUp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(false);
            outline.enabled = false;
            canPickUp = false;
        }
    }

    private void Interact()
    {
        CrystalManager.instance.AddCrystal(value); //Agarramos el numero actual de contador y le sumamos uno.
        HUDManager.instance.ShowPrompt(false);
        outline.enabled = false;
        Destroy(gameObject); //Nos destruimos        
    }
}
