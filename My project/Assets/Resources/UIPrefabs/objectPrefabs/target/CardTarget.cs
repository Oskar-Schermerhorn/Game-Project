using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardAction {SWAP, SHUFFLE};
public class CardTarget : Target
{
    [SerializeField] public int positionIndex;
    [SerializeField] public List<int> possiblePositions;

    [SerializeField] public int numSelections;
    [SerializeField] public List<int> selections;
    [SerializeField] public List<CardAction> cardActions;
    [SerializeField] public int user;
    void Start()
    {
        //cardActions = new List<CardAction>();
        positionIndex = 0;
        for(int i=0; i<cards.transform.childCount; i++)
        {
            possiblePositions.Add(i);
        }
        resetPosition();
    }
    public override void moveToNext()
    {
        positionIndex++;
        if (positionIndex >= possiblePositions.Count)
        {
            positionIndex = 0;
        }
        position = possiblePositions[positionIndex];
        this.gameObject.transform.position = cards.transform.GetChild(position).transform.position;
    }
    public override void moveToPrev()
    {
        positionIndex--;
        if (positionIndex < 0)
        {
            positionIndex = possiblePositions.Count - 1;
        }
        position = possiblePositions[positionIndex];
        this.gameObject.transform.position = cards.transform.GetChild(position).transform.position;
    }
    public void resetPosition()
    {
        positionIndex = 0;
        position = possiblePositions[positionIndex];
        this.gameObject.transform.position = cards.transform.GetChild(position).transform.position;
    }
    public void submit()
    {
        for(int i =0; i<cardActions.Count; i++)
        {
            dealer.editDeck(cardActions[i], selections);
        }
        
    }
}
