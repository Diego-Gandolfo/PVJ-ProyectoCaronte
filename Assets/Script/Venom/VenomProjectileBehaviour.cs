using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    private float currentTimeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeToDestroy += Time.deltaTime;
        if (currentTimeToDestroy >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
