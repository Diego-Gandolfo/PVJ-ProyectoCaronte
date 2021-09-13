using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float mouseWheelValue;
    //Zoom values x = min y= default z= max
    [SerializeField] private Vector3 zoomValues;
    [SerializeField] private float minDistance;
    [SerializeField] private LayerMask layers;
    [SerializeField] private Transform target;
    private void Start()
    {


    }
    void Update()
    {

        Zoom();
    }
    private void Zoom()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.fieldOfView = zoomValues.z;
        }
        else
        {
            Camera.main.fieldOfView = zoomValues.y;
        }

    }
            //private void OnWheelScroll()
            //{
            //mouseWheelValue = Input.mouseScrollDelta.y;
            //float cmFieldOfView = Mathf.Clamp(cinemachineVirtualCamera.m_Lens.FieldOfView, zoomValues.z, zoomValues.x);
            // if (mouseWheelValue < 0)
            // {
            //     cmFieldOfView += -mouseWheelValue;
            // }
            //cinemachineVirtualCamera.m_Lens.FieldOfView = cmFieldOfView;
            //}
    //    }
    //}
}
