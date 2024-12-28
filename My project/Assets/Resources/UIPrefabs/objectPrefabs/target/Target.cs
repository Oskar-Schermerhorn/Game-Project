using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected ObjectLocator locator;
    protected CardDealer dealer;
    protected GameObject cards;
    [SerializeField] public int position = 4;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        dealer = GameObject.Find("BattleHandler").GetComponent<CardDealer>();
        cards = GameObject.Find("CardHolder");
    }
    public virtual void moveToNext()
    {
        this.gameObject.transform.position = locator.locateObject(position).transform.position;
    }
    public virtual void moveToPrev()
    {

    }
}
