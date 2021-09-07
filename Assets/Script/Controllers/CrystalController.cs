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
        if (collision.gameObject.layer == 7)
            outline.enabled = true;
    }

    private void OnDieListener()
    {
        var spawn = Instantiate(crystalDrop);
        spawn.transform.position = transform.position;
        Destroy(gameObject);
    }
}
