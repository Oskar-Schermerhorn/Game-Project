using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardIndicatorScript : MonoBehaviour
{
    public hazard haz;
    public int turns;
    public int position;
    public void incrementTurns()
    {
        turns--;
        updateColor();
    }
    public void updateColor()
    {
        switch (turns)
        {
            case 1:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            default:
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
        }
    }
    public void inflictDamage()
    {
        this.gameObject.GetComponentInParent<hazardHandler>().inflictDamage();
    }
    public void finishAnimation()
    {
        this.gameObject.GetComponentInParent<hazardHandler>().hazardHandled();
    }
}
