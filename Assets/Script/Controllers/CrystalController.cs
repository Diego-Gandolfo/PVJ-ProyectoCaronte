using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private GameObject crystalDrop;
    private Outline outline;
    private HealthController healthController;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        healthController = GetComponent<HealthController>();
        healthController.OnDie.AddListener(OnDieListener);
    }

    private void OnCollisionEnter(Collision collision) //No se si es mega necesario
    {
    }

    private void OnDieListener()
    {
        var spawn = Instantiate(crystalDrop);
        spawn.transform.position = transform.position;

        var spawnGravityBody = spawn.GetComponent<GravityBody>();

        if (spawnGravityBody != null)
        {
            var myGravityBody = GetComponent<GravityBody>();
            if(myGravityBody != null && myGravityBody.GravityAttractor != null)
            spawnGravityBody.AssignAttractor(myGravityBody.GravityAttractor);
        }
        
        gameObject.SetActive(false);
    }
}
