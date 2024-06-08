using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackground : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = GameObject.Find("DataRecorder").GetComponent<DataRecorderBattleBackground>().Background;
    }
}
