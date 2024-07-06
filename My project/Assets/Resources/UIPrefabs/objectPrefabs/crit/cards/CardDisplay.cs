using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] card thisCard;
    [SerializeField] Color Red;
    [SerializeField] Color Blue;
    [SerializeField] Color Purple;
    [SerializeField] Color Gray;


    public void setCard(card nextCard)
    {
        thisCard = nextCard;
    }

    public void removeCard()
    {
        Destroy(this.gameObject);
    }

    void setVisual()
    {
        if (thisCard != null)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = checkColor();
            this.transform.Find("Symbol").gameObject.GetComponent<TextMeshProUGUI>().text = checkSymbol();
            if(thisCard.property != cardProperty.NORMAL && thisCard.property != cardProperty.STATUS)
            {
                this.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>().text = thisCard.damageModifier.ToString();
            }
            else
            {
                this.transform.Find("Number").gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
            
        }
        
    }

    Color checkColor()
    {
        switch (thisCard.color)
        {
            case cardColor.RED:
                return Red;
            case cardColor.BLUE:
                return Blue;
            case cardColor.PURPLE:
                return Purple;
            case cardColor.GRAY:
                return Gray;

        }
        return Color.white;
    }

    string checkSymbol()
    {
        switch (thisCard.property)
        {
            case cardProperty.NORMAL:
                return "";
            case cardProperty.ADD:
                return "+";
            case cardProperty.SUB:
                return "-";
            case cardProperty.MULTIPLY:
                return "X";
            case cardProperty.DIVIDE:
                return "/";
            case cardProperty.STATUS:
                break;
        }
        return "";
    }
    

}
