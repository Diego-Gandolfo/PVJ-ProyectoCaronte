using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Outline))]
public class CrystalController : MonoBehaviour
{
    [SerializeField] private GameObject crystalDrop;
    [SerializeField] private int spawningMaxNumber;
    private Outline outline;
    private HealthController healthController;
    public Action<CrystalController> OnDie;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        healthController = GetComponent<HealthController>();
        healthController.OnDie += OnDieListener;
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
        OnDie?.Invoke(this);
    }
}
