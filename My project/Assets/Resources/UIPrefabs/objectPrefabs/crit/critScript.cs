using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critScript : MonoBehaviour
{
    string[] cardList = new string[]{"1Star", "2Star", "3Star", "4Star",
        "1Hex", "2Hex", "3Hex", "4Hex",
        "1Left", "2Left", "3Left", "4Left",
        "1Tri", "2Tri", "3Tri", "4Tri" };
    bool[] usedCard = { false, false, false, false,
    false, false, false, false,
    false, false, false, false,
    false, false, false, false};
    bool critical;
    int index;
    int indexFinal;
    int cardsPulled = 0;
    string currentState;
    Animator anim;
    Animator cardAnim;
    [SerializeField] GameObject card;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cardAnim = card.GetComponent<Animator>();
    }

    int randomize()
    {
        return Random.Range(0, 16);
    }

    void pickAcard()
    {
        changeAnimation("pullCard");
        index = randomize();
       // print(index);
        while (usedCard[index])
        {
            index = randomize();
           // print(index);
        }
        indexFinal = index;
        usedCard[index] = true;
        cardsPulled++;
        if (cardsPulled > 15)
        {
            reshuffle();
        }
        //print(cardList[index]);
    }

    public bool checkCrit()
    {
        //temp removing crits
        return false;

        critical = false;
        pickAcard();
        if(cardList[indexFinal] == "1Star")
        {
            critical = true;
        }
        else if(cardList[indexFinal] == "2Star")
        {
            critical = true;
        }
        else if (cardList[indexFinal] == "3Star")
        {
            critical = true;
        }
        else if (cardList[indexFinal] == "4Star")
        {
            critical = true;
        }
        return critical;
    }

    void reshuffle()
    {
        cardsPulled = 0;
        print("Shuffle");
        for (int i = 0; i<16; i++)
        {
            usedCard[i] = false;
        }
    }

    private void changeAnimation(string newState)
    {
        anim.Play(newState);
        currentState = newState;
    }

    void displayCard()
    {
        changeAnimation("none");
        string c = cardList[indexFinal];
        if (critical)
        {
            c = c + "Shine";
        }
        cardAnim.Play(c);
        Invoke("removeCard", 1f);
    }

    void removeCard()
    {
        if(currentState == "none")
        {
            cardAnim.Play("none");
            changeAnimation("remove");
        }
    }

    void removeCardDone()
    {
        changeAnimation("none");
    }
}
