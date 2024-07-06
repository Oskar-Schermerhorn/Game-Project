using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private void Awake()
    {
        BattleUnitFinish.EndTurn += removeCard;
    }

    public void removeAllCards()
    {
        while(this.transform.childCount > 0)
        {
            this.transform.GetChild(0).gameObject.GetComponent<CardDisplay>().removeCard();
        }
    }

    void removeCard()
    {
        if (this.transform.childCount > 0)
        {
            this.transform.GetChild(0).gameObject.GetComponent<CardDisplay>().removeCard();
        }
        
    }

    private void OnDisable()
    {
        BattleUnitFinish.EndTurn -= removeCard;
    }
}
