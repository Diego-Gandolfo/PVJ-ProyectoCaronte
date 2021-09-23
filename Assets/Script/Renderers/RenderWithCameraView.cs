using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderWithCameraView : MonoBehaviour
{
    private MeshRenderer mesh;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    void OnBecameInvisible()
    {
        mesh.enabled = false;
    }
    void OnBecameVisible()
    {
        mesh.enabled = true;
    }
}
