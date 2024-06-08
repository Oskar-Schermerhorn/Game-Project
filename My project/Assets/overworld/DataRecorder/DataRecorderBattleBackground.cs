using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderBattleBackground : MonoBehaviour
{
    DataRecorderObjectPermanence dataObject;
    public Sprite Background { get; private set; }
    private void Awake()
    {
        dataObject = this.gameObject.GetComponent<DataRecorderObjectPermanence>();
    }
    public void setBackground()
    {
        Background = GameObject.Find(dataObject.currentRoomName).GetComponentInChildren<Rooms>().BattleBackground;
    }
}
