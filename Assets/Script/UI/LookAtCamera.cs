using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cam, cam.up);
    }
}
