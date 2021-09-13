using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionDetection : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    private RaycastHit hit;
    [SerializeField] private LayerMask hitLayers;
    void Update()
    {
        if (!Physics.Linecast(transform.position, transform.forward,out hit,hitLayers))
        {
            transform.position += Vector3.forward;

        }
    }
}
