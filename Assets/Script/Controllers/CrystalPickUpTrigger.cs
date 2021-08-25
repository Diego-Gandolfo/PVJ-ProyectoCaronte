using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickUpTrigger : MonoBehaviour
{
    [SerializeField] private int value = 1;
    private bool canPickUp;
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && canPickUp)
        {
            Interact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(true);
            canPickUp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            HUDManager.instance.ShowPrompt(false);
            canPickUp = false;
        }
    }

    private void Interact()
    {
        CrystalManager.instance.AddCrystal(value); //Agarramos el numero actual de contador y le sumamos uno.
        HUDManager.instance.ShowPrompt(false);
        Destroy(gameObject); //Nos destruimos        
    }
}
