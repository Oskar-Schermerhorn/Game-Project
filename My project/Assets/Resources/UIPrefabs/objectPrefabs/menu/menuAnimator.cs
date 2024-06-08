using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuAnimator : MonoBehaviour
{
    [SerializeField] GameObject AttackPrefab;
    [SerializeField] GameObject BatteryPrefab;
    [SerializeField] GameObject ItemPrefab;
    private void Awake()
    {
        turnManagement.EnemyTurn += destroyMenu;
        turnManagement.Status += destroyMenu;
        targetController.confirmedTarget += destroyMenu;
        turnManagement.NewTurn += resetMenu;
    }
    public void UpdateImages(int currentSelection, bool selected)
    {
        for (int i = 0; i < GetComponentsInChildren<MenuButton>().Length; i++)
        {
            if (GetComponentsInChildren<MenuButton>()[i].currentSprite == 1)
            {
                GetComponentsInChildren<MenuButton>()[i].changeSprite(0);
            }
        }
        if (!selected)
        {
            GetComponentsInChildren<ISelectable>()[currentSelection].changeSprite(1);
        }
        else
        {
            select(currentSelection);
        }
    }
    private void select(int currentSelection)
    {
        switch (currentSelection)
        {
            case 0:
                GameObject.Find("Menu/Attack").GetComponent<ISelectable>().changeSprite(2);
                if (GameObject.Find("Menu/Battery") != null)
                {
                    Destroy(GameObject.Find("Menu/Battery"));
                }
                if (GameObject.Find("Menu/Item") != null)
                {
                    Destroy(GameObject.Find("Menu/Item"));
                }
                break;
            case 1:
                GameObject.Find("Menu/Battery").GetComponent<ISelectable>().changeSprite(2);
                if (GameObject.Find("Menu/Attack") != null)
                {
                    Destroy(GameObject.Find("Menu/Attack"));
                }
                if (GameObject.Find("Menu/Item") != null)
                {
                    Destroy(GameObject.Find("Menu/Item"));
                }
                break;
            case 2:
                GameObject.Find("Menu/Item").GetComponent<ISelectable>().changeSprite(2);
                if (GameObject.Find("Menu/Attack") != null)
                {
                    Destroy(GameObject.Find("Menu/Attack"));
                }
                if (GameObject.Find("Menu/Battery") != null)
                {
                    Destroy(GameObject.Find("Menu/Battery"));
                }
                break;
        }
        
        
    }
    public void resetMenu()
    {
        print("reset menu");
        destroyMenu();
        GameObject option;
        option = Instantiate<GameObject>(AttackPrefab, this.gameObject.transform);
        option.name = "Attack";
        option.GetComponent<ISelectable>().changeSprite(1);
        option.GetComponent<SpriteRenderer>().enabled = false;
        option = Instantiate<GameObject>(BatteryPrefab, this.gameObject.transform);
        option.name = "Battery";
        option.GetComponent<SpriteRenderer>().enabled = false;
        option = Instantiate<GameObject>(ItemPrefab, this.gameObject.transform);
        option.name = "Item";
        option.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(showMenu());
    }
    IEnumerator showMenu()
    {
        yield return new WaitForEndOfFrame();
        if (GameObject.Find("Menu/Attack") != null)
        {
            GameObject.Find("Menu/Attack").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (GameObject.Find("Menu/Battery") != null)
        {
            GameObject.Find("Menu/Battery").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (GameObject.Find("Menu/Item") != null)
        {
            GameObject.Find("Menu/Item").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void destroyMenu()
    {
        for (int i = 0; i < this.gameObject.GetComponentsInChildren<SpriteRenderer>().Length; i++)
        {
            Destroy(this.gameObject.GetComponentsInChildren<SpriteRenderer>()[i].gameObject);
        }
    }
    void destroyMenu(int _)
    {
        destroyMenu();
    }
    void destroyMenu(List<int> _)
    {
        destroyMenu();
    }
    private void OnDisable()
    {
        turnManagement.EnemyTurn -= destroyMenu;
        turnManagement.Status -= destroyMenu;
        targetController.confirmedTarget -= destroyMenu;
        turnManagement.NewTurn -= resetMenu;
    }
}
