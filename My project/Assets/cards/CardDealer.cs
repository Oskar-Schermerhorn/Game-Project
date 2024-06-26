using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardDealer : MonoBehaviour
{
    ObjectLocator locator;


    List<card> fullDeck;
    [SerializeField] List<card> deck;

    //temp to test
    [SerializeField] card normalCard;
    [SerializeField] card plus1Card;

    public static event Action FinishDealing;

    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();

        fullDeck = new List<card>();
        deck = new List<card>();
        for (int i = 0; i < 10; i++)
        {
            fullDeck.Add(normalCard);
            fullDeck.Add(plus1Card);
        }
        deck.AddRange(fullDeck);
        turnManagement.CardTurn += dealCards;
    }


    void dealCards()
    {
        print("dealing cards");
        for(int i = 0; i<locator.numObjects(); i++)
        {
            if(locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
            {
                print(pullCard().name);
            }
            
        }
        FinishDealing();
    }

    card pullCard()
    {
        if (deck.Count == 0)
        {
            deck.AddRange(fullDeck);
        }
        System.Random random = new System.Random();
        int index = random.Next(0, deck.Count);
        card pickedCard = deck[index];
        deck.RemoveAt(index);
        return pickedCard;
    }

    private void OnDisable()
    {
        turnManagement.CardTurn -= dealCards;
    }
}
