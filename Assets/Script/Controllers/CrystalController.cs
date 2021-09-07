using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] private GameObject crystalPickUpPrefab;
    private Outline outline;
    private HealthController healthController;


    void Start()
    {
        outline = GetComponent<Outline>();
        healthController = GetComponent<HealthController>();
        healthController.OnDie.AddListener(OnDieListener);
    }

    private void OnCollisionEnter(Collision collision) //No se si es mega necesario
    {
        if (collision.gameObject.layer == 7)
        {
            outline.enabled = true;
        }
    }

    private void OnDieListener()
    {
        Destroy(gameObject);
        //Spawneas los cristales para pickear y te destruis
        //var gravityBody = gameObject.GetComponent<GravityBody>();
        //if (gravityBody != null)
        //    gravityBody.Attractor.RemoveGravityBody(gravityBody);
    }
}
