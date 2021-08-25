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
       float cmFieldOfView = Mathf.Clamp(cinemachineVirtualCamera.m_Lens.FieldOfView, 25f, 60f);
        if (mouseWheelValue != 0)
        {
            cmFieldOfView += -mouseWheelValue;
        }
        cinemachineVirtualCamera.m_Lens.FieldOfView = cmFieldOfView;
    }
}
