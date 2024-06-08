using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitCamera : MonoBehaviour
{
    protected BattleCamera zoomCamera;
    private void Awake()
    {
        zoomCamera = GameObject.Find("CameraZoomFollow").GetComponent<BattleCamera>();
    }
    protected void customCamera()
    {
        zoomCamera.zoomFollow(gameObject.GetComponentsInChildren<Transform>()[1]);
    }
    protected void addScreenShake()
    {
        zoomCamera.addScreenShake();
    }
    public void stopScreenShake()
    {
        zoomCamera.stopScreenShake();
    }
    public void zoomOut()
    {
        zoomCamera.zoomOutCamera();
    }
}
