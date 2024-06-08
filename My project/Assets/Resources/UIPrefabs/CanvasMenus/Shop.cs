using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

[Serializable]
public class ShopInfo
{
    [SerializeField] int itemIndex;
    [SerializeField] int cost;
    int currentlySelected = 0;
    public int GetItem()
    {
        return itemIndex;
    }
    public int GetCost()
    {
        return cost;
    }
}

public class Shop : MonoBehaviour
{
    DataRecorderItems dataItems;
    DataRecorderProgress dataProgress;
    [SerializeField] GameObject ItemPrefab;
    [SerializeField] List<ShopInfo> itemInfo = new List<ShopInfo>();
    public BattleInputs controls;
    MoveScript player;

    private void Awake()
    {
        dataItems = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
        dataProgress = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        InteractableShop.OpenShop += SetupShop;
        controls = new BattleInputs();
        controls.BattleMenu.Direction.started += _ => { Scroll (); };
        controls.Overworld.Interact.started += _ => { Buy(); };
        controls.Overworld.Cancel.started += _ => { Unpause(); };
        controls.Overworld.Pause.started += _ => { Unpause(); };
        player = GameObject.Find("Player").GetComponent<MoveScript>();
    }
    void SetupShop(List<ShopInfo> info)
    {
        Pause();
        GameObject itemOption;
        itemInfo = info;
        foreach(ShopInfo item in info){
            itemOption = Instantiate<GameObject>(ItemPrefab, this.gameObject.transform.Find("Items"));
            itemOption.name = dataItems.getItem(item.GetItem()).itemName;
            itemOption.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = dataItems.getItem(item.GetItem()).itemName;
            itemOption.transform.Find("ItemIcon").GetComponent<Image>().sprite = dataItems.getItem(item.GetItem()).art;
            itemOption.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = item.GetCost() + "";
            
        }
        StartCoroutine(FixInformation());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.transform.Find("Items/" + dataItems.getItem(info[0].GetItem()).itemName).gameObject);
    }
    void Scroll()
    {
        StartCoroutine(FixInformation());
    }
    IEnumerator FixInformation()
    {
        yield return new WaitForEndOfFrame();
        this.transform.Find("Info/Description").GetComponent<TextMeshProUGUI>().text = dataItems.getItem(itemInfo[FindCurrentSelection()].GetItem()).effectDescription;
        this.transform.Find("Info/Owned").GetComponent<TextMeshProUGUI>().text = "Owned: " + FindOwned(itemInfo[FindCurrentSelection()].GetItem());
        this.transform.Find("Info/Coins").GetComponent<TextMeshProUGUI>().text =  dataProgress.coins + " coins";
        for(int i =0; i< this.transform.Find("Items").childCount; i++)
        {
            ValidateItem(this.transform.Find("Items").GetChild(i).gameObject);
        }
    }
    void ValidateItem(GameObject item)
    {
        Color red = new Color(241/255f, 90/255f, 96/255f);
        Color white = Color.white;
        Color text = new Color(56/255f, 56/255f, 56/255f);
        Color nameColor = text;
        Color costColor = white;
        if(!(dataItems.getOpenItemSlots().Count > 0))
        {
            nameColor = red;
        }
        if(!(dataProgress.coins >= int.Parse(item.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text)))
        {
            costColor = red;
        }
        item.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().color = nameColor;
        item.transform.Find("Cost").GetComponent<TextMeshProUGUI>().color = costColor;
    }
    int FindOwned(int itemIndex)
    {
        int owned = 0;
        for(int i = 0; i < dataItems.items.Length; i++)
        {
            if(dataItems.items[i] == itemIndex)
            {
                owned++;
            }
        }
        return owned;
    }
    int FindCurrentSelection()
    {
        for (int i = 0; i < itemInfo.Count; i++)
            if (EventSystem.current.currentSelectedGameObject.name == dataItems.getItem(itemInfo[i].GetItem()).itemName)
            {
                return i;
            }
        return -1;
    }
    void Buy()
    {
        if(dataItems.getOpenItemSlots().Count > 0 && dataProgress.coins >= itemInfo[FindCurrentSelection()].GetCost())
        {
            dataItems.addItem(itemInfo[FindCurrentSelection()].GetItem());
            dataProgress.SpendCoins(itemInfo[FindCurrentSelection()].GetCost());
        }
        StartCoroutine(FixInformation());
    }
    public void Pause()
    {
        controls.Enable();
        Time.timeScale = 0f;
        player.DisableControl();
    }

    public void Unpause()
    {
        controls.Disable();
        Time.timeScale = 1f;
        player.EnableControl();
        Destroy(this.gameObject);
        
    }

    private void OnDisable()
    {
        InteractableShop.OpenShop -= SetupShop;

    }
}
