using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCamera : MonoBehaviour
{
    GameObject DefaultCam;
    GameObject ZoomCam;
    private void Start()
    {
        DefaultCam = GameObject.Find("CameraDefault");
        ZoomCam = GameObject.Find("CameraZoomFollow");
    }

    public void zoomFollow(Transform f)
    {
        GetComponent<CinemachineVirtualCamera>().Priority = 12;
        GetComponent<CinemachineVirtualCamera>().Follow = f;

    }
    public void zoomOutCamera()
    {
        GetComponent<Transform>().position = new Vector3(0,0, -10f);
        GetComponent<CinemachineVirtualCamera>().Priority = 8;
    }
    public void addScreenShake()
    {
        if(GetComponent<CinemachineVirtualCamera>().Priority == 12)
        {
            GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        }
        else
        {
            DefaultCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        }
    }
    public void stopScreenShake()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        //EnemyCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        DefaultCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        ZoomCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

    }
}
