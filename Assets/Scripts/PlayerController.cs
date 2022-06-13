using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    private CinemachineOrbitalTransposer orbitComponent;

    public float cameraRotateSpeed = 2.0f;
    
    public void Update()
    {
        if (orbitComponent == null)
        {
            orbitComponent = playerCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }
        
        if (Input.GetMouseButton(1))
        {
            float delta = Input.GetAxis("Mouse X");
            orbitComponent.m_XAxis.m_InputAxisValue = delta * cameraRotateSpeed * Time.deltaTime;
            orbitComponent.m_RecenterToTargetHeading.CancelRecentering();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            orbitComponent.m_XAxis.m_InputAxisValue = 0;
            orbitComponent.m_RecenterToTargetHeading.RecenterNow();
        }
    }
}
