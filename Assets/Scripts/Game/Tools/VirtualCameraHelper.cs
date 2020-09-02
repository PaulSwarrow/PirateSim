using System;
using Cinemachine;
using Lib;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace App.Tools
{
    public class VirtualCameraHelper : BaseComponent
    {
        private CinemachineFreeLook cameraLook;
        private float vx;
        private float vy;

        private void Awake()
        {
            cameraLook = GetComponent<CinemachineFreeLook>();
            vx = cameraLook.m_XAxis.m_MaxSpeed;
            vy = cameraLook.m_YAxis.m_MaxSpeed;

        }

        private void Update()
        {
            
            if ( Cursor.visible)
            {
                cameraLook.m_YAxis.m_MaxSpeed = 0;
                cameraLook.m_XAxis.m_MaxSpeed = 0;
            }
            else
            {
                cameraLook.m_YAxis.m_MaxSpeed = vy;
                cameraLook.m_XAxis.m_MaxSpeed = vx;
            }
        }
    }
}