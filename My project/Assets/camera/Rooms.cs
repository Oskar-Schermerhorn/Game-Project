using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rooms : MonoBehaviour
{
    public GameObject virtualCam;
    public static event Action<bool, GameObject> ActiveRoom;
    [SerializeField] public Sprite BattleBackground;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            ActiveRoom(true, this.gameObject.transform.parent.gameObject);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
            ActiveRoom(false, this.gameObject.transform.parent.gameObject);
        }

    }
}
