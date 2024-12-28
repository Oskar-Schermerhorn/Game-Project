using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardDealer : MonoBehaviour
{
    ObjectLocator locator;
    GameObject cardHolder;


    List<card> fullDeck;
    [SerializeField] List<card> deck;
    [SerializeField] List<card> dealtCards;

    [SerializeField] GameObject cardPrefab;

    //temp to test
    [SerializeField] card normalCard;
    [SerializeField] card plus1Card;
    [SerializeField] card times2Card;

    public static event Action FinishDealing;

    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        cardHolder = GameObject.Find("Canvas/CardHolder");
        fullDeck = new List<card>();
        deck = new List<card>();
        //placeholder deck
        for (int i = 0; i < 10; i++)
        {
            fullDeck.Add(normalCard);
            fullDeck.Add(plus1Card);
        }
        fullDeck.Add(times2Card);
        deck.AddRange(fullDeck);
        turnManagement.CardTurn += dealCards;
    }

    void dealCards()
    {
        print("dealing cards");
        dealtCards.Clear();
        cardHolder.GetComponent<CardHolder>().removeAllCards();

        for(int i = 0; i<locator.numObjects(); i++)
        {
            if(locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
            {
                card nextCard = pullCard();
                dealtCards.Add(nextCard);
                GameObject cardVisual = Instantiate<GameObject>(cardPrefab, cardHolder.transform);
                cardVisual.GetComponent<CardDisplay>().setCard(nextCard);
            }
            
        }
        FinishDealing();
    }

    public card getCard(int index)
    {
        //inconsistency with what units are left in the unit list after death
        //for(int i = 0; i<locator.locateObject())
        return dealtCards[index];
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

    public List<card> getDeck()
    {
        return deck;
    } 

    public void editDeck(CardAction action, List<int>cards)
    {
        //cards 2 1
        switch (action)
        {
            case CardAction.SWAP:
                print("good so far :D");
                List<card> replacedCards = new List<card>();
                for (int i=0; i< cards.Count; i++)
                {
                    replacedCards.Add(dealtCards[cards[i]]);
                }

                card first = replacedCards[0];
                replacedCards.RemoveAt(0);
                replacedCards.Add(first);

                print(replacedCards.Count == cards.Count);

                for (int i = 0; i < cards.Count; i++)
                {
                    dealtCards.RemoveAt(cards[i]);
                    dealtCards.Insert(cards[i], replacedCards[i]);
                }

                for (int i = 0; i < cards.Count; i++)
                {
                    GameObject cardVisual = cardHolder.transform.GetChild(cards[i]).gameObject;
                    cardVisual.GetComponent<CardDisplay>().setCard(dealtCards[cards[i]]);
                    cardVisual.GetComponent<Animator>().Play("flipCard", 0, 0);
                }
                break;
            case CardAction.SHUFFLE:
                break;
        }
    }

    private void OnDisable()
    {
        turnManagement.CardTurn -= dealCards;
    }
}
