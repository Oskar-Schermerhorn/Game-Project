using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldEnemyDestroy : MonoBehaviour
{
    [SerializeField] private List<item> itemDrops = new List<item> { };
    [SerializeField] private GameObject itemPrefab;
    Vector2 position;
    public string id;

    

    public void defeated(List<item> items)
    {
        itemDrops = items;
        this.gameObject.GetComponent<overworldEnemyBehavior>().enabled = false;
        if (this.gameObject.GetComponent<OneTimeUse>() != null)
        {
            this.gameObject.GetComponent<OneTimeUse>().Use();
        }
        GetComponent<Animator>().Play("defeatEnemy");
        
    }
    public void dropItems()
    {
        for (int i = 0; i < itemDrops.Count; i++)
        {
            if (itemDrops[i].index != 5)
            {
                print(itemDrops[i].itemName);
                GameObject itemDropped = Instantiate<GameObject>(itemPrefab, this.gameObject.transform.position, new Quaternion(), GameObject.Find("BackgroundHolder/DroppedItems").transform);
                itemDropped.AddComponent<itemSlide>();
                itemDropped.GetComponent<itemSlide>().calculateDropPosition();
                itemDropped.name = itemDrops[i].itemName + ":" + itemDrops[i].index;
                itemDropped.GetComponent<SpriteRenderer>().color = Color.white;
                itemDropped.GetComponent<SpriteRenderer>().sprite = itemDrops[i].art16Bit;
                itemDropped.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        resetValuesAndDisable();
    }

    void resetValuesAndDisable()
    {
        this.transform.position = position;
        this.transform.rotation = Quaternion.identity;
        this.gameObject.GetComponent<overworldEnemyBehavior>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.SetActive(false);
    }

    
}
