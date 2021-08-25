using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float mouseWheelValue;
    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        
        
    }
    void Update()
    {
        mouseWheelValue = Input.mouseScrollDelta.y;
        Mathf.Clamp(cinemachineVirtualCamera.m_Lens.FieldOfView, 25f, 50);
        if (Input.mouseScrollDelta.y != 0)
        {
            if (cinemachineVirtualCamera.m_Lens.FieldOfView <= 50)
                cinemachineVirtualCamera.m_Lens.FieldOfView += Input.mouseScrollDelta.y;
            else if (cinemachineVirtualCamera.m_Lens.FieldOfView >= 25)
            {
                cinemachineVirtualCamera.m_Lens.FieldOfView -= Input.mouseScrollDelta.y;
            }
        }
    }
}
