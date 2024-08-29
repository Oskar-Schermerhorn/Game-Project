using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckVisual : MonoBehaviour
{
    CardDealer dealer;
    [SerializeField] GameObject DeckDescriptionPrefab;
    [SerializeField] GameObject BlankCardPrefab;
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
        GameObject Description = Instantiate(DeckDescriptionPrefab, this.transform.parent);
        for(int i = 0; i < dealer.getDeck().Count; i++)
        {
            if (Description.transform.Find(dealer.getDeck()[i].name) != null)
            {
                GameObject oldCard = Description.transform.Find(dealer.getDeck()[i].name).gameObject;
                int quantity = int.Parse(oldCard.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text.Remove(0, 1));
                quantity++;
                oldCard.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x" + quantity;
            }
            else
            {
                GameObject newCard = Instantiate(BlankCardPrefab, Description.transform);
                for(int j = 0; j< newCard.transform.childCount; j++)
                {
                    newCard.transform.GetChild(j).transform.position 
                        = new Vector2(newCard.transform.GetChild(j).transform.position.x, newCard.transform.GetChild(j).transform.position.y - 7.34f);
                }
                newCard.GetComponent<CardDisplay>().setCard(dealer.getDeck()[i]);
                newCard.name = dealer.getDeck()[i].name;
                newCard.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x1";
            }
            
        }
    }
}
