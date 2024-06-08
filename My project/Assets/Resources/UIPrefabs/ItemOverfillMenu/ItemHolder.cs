using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ItemHolder : MonoBehaviour
{
    DataRecorderItems dataItems;
    BattleInputs controls;
    [SerializeField] GameObject option;
    [SerializeField] GameObject confirmOptions;
    List<GameObject> OpenOptions = new List<GameObject>();
    int newItem;
    private void Awake()
    {
        dataItems = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
        PlayerCollision.ShowItems += ShowItems;
        controls = new BattleInputs();
        //
    }
    void ShowItems(int newIndex)
    {
        newItem = newIndex;
        if(dataItems.getOpenItemSlots().Count >0)
        {
            dataItems.addItem(newIndex);
        }
        else
        {
            OpenItemMenu(newIndex);
        }
    }
    void OpenItemMenu(int newItem)
    {
        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<MoveScript>().DisableControl();

        this.transform.Find("OverfillItemsPanel").GetComponent<Image>().enabled = true;
        this.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enabled = true;
        GameObject ItemOption;
        for (int i = 0; i<5; i++)
        {
            ItemOption = Instantiate<GameObject>(option, this.transform.Find("OverfillItemsPanel"));
            ItemOption.transform.Find("ItemIcon").GetComponent<Image>().sprite = dataItems.getItem(dataItems.items[i]).art;
            ItemOption.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = dataItems.getItem(dataItems.items[i]).itemName;
            ItemOption.gameObject.name = dataItems.getItem(dataItems.items[i]).itemName;
            int x = i;
            ItemOption.GetComponent<Button>().onClick.AddListener(() => Confirm(x));
            OpenOptions.Add(ItemOption);
        }
        ItemOption = Instantiate<GameObject>(option, this.transform.Find("OverfillItemsPanel"));
        ItemOption.transform.Find("ItemIcon").GetComponent<Image>().sprite = dataItems.getItem(newItem).art;
        ItemOption.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = dataItems.getItem(newItem).itemName;
        ItemOption.GetComponent<Button>().onClick.AddListener(() => Confirm(5));
        ItemOption.gameObject.name = dataItems.getItem(newItem).itemName;
        EventSystem.current.SetSelectedGameObject(ItemOption);
        OpenOptions.Add(ItemOption);
    }
    void Confirm(int selection)
    {
        GameObject ConfirmationBubble = Instantiate<GameObject>(confirmOptions, this.transform);
        for(int i =0; i< OpenOptions.Count; i++)
        {
            OpenOptions[i].GetComponent<Button>().interactable = false;
        }
        ConfirmationBubble.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Discard \n" + OpenOptions[selection].name + "?";
        EventSystem.current.SetSelectedGameObject(ConfirmationBubble.transform.Find("No").gameObject);
        ConfirmationBubble.transform.Find("Yes").gameObject.GetComponent<Button>().onClick.AddListener(() => ReplaceItem(selection));
        ConfirmationBubble.transform.Find("No").gameObject.GetComponent<Button>().onClick.AddListener(() => ResetItem(ConfirmationBubble));
        OpenOptions.Add(ConfirmationBubble);
    }
    void ReplaceItem(int selection)
    {
        if (selection < 5)
            dataItems.replaceItem(newItem, selection);
        for(int i =0; i< OpenOptions.Count; i++)
        {
            Destroy(OpenOptions[i]);
        }
        OpenOptions.Clear();
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<MoveScript>().EnableControl();

        this.transform.Find("OverfillItemsPanel").GetComponent<Image>().enabled = false;
        this.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enabled = false;
    }
    void ResetItem(GameObject ConfirmationBubble)
    {
        OpenOptions.Remove(ConfirmationBubble);
        Destroy(ConfirmationBubble);
        for (int i = 0; i < OpenOptions.Count; i++)
        {
            OpenOptions[i].GetComponent<Button>().interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(OpenOptions[OpenOptions.Count-1]);
    }
    private void OnDisable()
    {
        PlayerCollision.ShowItems -= ShowItems;
    }
}
