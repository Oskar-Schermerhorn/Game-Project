using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckVisual : MonoBehaviour
{
    CardDealer dealer;
    private void Awake()
    {
        dealer = GameObject.Find("BattleHandler").gameObject.GetComponent<CardDealer>();
    }

    private void Update()
    {
        this.gameObject.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>().text = "" + dealer.getDeck().Count;
    }

    public void click()
    {
        print("clicked");
    }
}
