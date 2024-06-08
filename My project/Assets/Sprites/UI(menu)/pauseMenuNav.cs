using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class pauseMenuNav : MonoBehaviour
{
    [SerializeField] private DataRecorderParty dataParty;
    [SerializeField] private DataRecorderItems dataItem;
    [SerializeField] private DataRecorderMods dataMod;
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private GameObject[] defaultButton;
    [SerializeField] private GameObject optionsBox;
    [SerializeField] private GameObject characterBox;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject infoPage;
    [SerializeField] private GameObject modPrefab;
    [SerializeField] private pauseManager PauseManager;
    //[SerializeField] private Sprite[] icons;
    //[SerializeField] private Sprite[] BattleSprites;
    //[SerializeField] private partyMember[] PartyMembers;
    public int currentTab = 0;
    public int selectedCharIndex = 0;
    public int selectedItemIndex = 0;
    //event Action<mods> modEquip;

    public void nextTab()
    {
        cancel();
        hideTab(currentTab);
        if (currentTab < tabs.Length - 1)
        {
            currentTab++;
        }
        else
        {
            currentTab = 0;
        }
        showTab(currentTab);
    }

    public void prevTab()
    {
        cancel();
        hideTab(currentTab);
        if (currentTab > 0)
        {
            currentTab--;
        }
        else
        {
            currentTab = tabs.Length -1;
        }
        showTab(currentTab);
    }

    private void hideTab(int which)
    {
        tabs[which].SetActive(false);
    }

    private void showTab(int which)
    {
        tabs[which].SetActive(true);
        if(which == 2)
            setupMod();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultButton[which]);
        print(EventSystem.current);
    }

    public void resetTabs()
    {
        hideTab(currentTab);
        currentTab = 0;
        showTab(currentTab);
        //setupMod();
        //defaultButton.SetActive(true);
    }
    public void Start()
    {
        GameObject data = GameObject.Find("DataRecorder");
        dataParty = data.GetComponent<DataRecorderParty>();
        dataItem = data.GetComponent<DataRecorderItems>();
        dataMod = data.GetComponent<DataRecorderMods>();
        PauseManager = GameObject.Find("pauseManager").GetComponent<pauseManager>();
        for (int i = 0; i < 4; i++){
            selectedCharIndex = dataParty.getPartyIndex(i);
            if(selectedCharIndex != -1)
                putInCircle(dataParty.checkCharacterValid(selectedCharIndex));
        }
    }
    public void characterButton(int index)
    {
        selectedCharIndex = index;
        optionsBox.SetActive(true);
        print("Button has been pressed");
        deactivateCharacterButtons();
        optionsBox.GetComponentInChildren<Button>().Select();
    }
    public void deactivateCharacterButtons()
    {
        foreach (Button child in characterBox.GetComponentsInChildren<Button>())
        {
            
            //child.enabled = false;
            child.interactable = false;
        }
        circle.GetComponent<Button>().interactable = false;
    }
    public void reactivateCharacterButtons()
    {
        foreach (Button child in characterBox.GetComponentsInChildren<Button>())
        {
            //child.enabled = true;
            child.interactable = true;
        }
        characterBox.GetComponentInChildren<Button>().Select();
    }
    public void gotoMove()
    {
        foreach (Button child in circle.GetComponentsInChildren<Button>()){
            child.interactable = true;
            child.Select();
        }
        circle.GetComponent<Button>().interactable = false;
        //circle.GetComponentInChildren<Button>().Select();
        optionsBox.SetActive(false);
    }
    public void gotoInfo()
    {
        deactivateCharacterButtons();
        circle.GetComponent<Button>().interactable = false;
        optionsBox.SetActive(false);
        infoPage.SetActive(true);
        //GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/BattleSpritePanel/CharacterBorder/BattleSprite").GetComponent<Image>().sprite = BattleSprites[selectedCharIndex];
        GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/BattleSpritePanel/CharacterBorder/BattleSprite").GetComponent<Image>().sprite 
            = dataParty.getPartyMember(selectedCharIndex).battleSprite;
        //GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/BattleSpritePanel/Text").GetComponent<Text>().text 
        GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/BattleSpritePanel/Text").GetComponent<TextMeshProUGUI>().text
            = dataParty.getPartyMember(selectedCharIndex).characterName;
        GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/HPPanel/Text").GetComponent<TextMeshProUGUI>().text 
            = dataParty.getPartyMember(selectedCharIndex).hp + "/" + dataParty.getPartyMember(selectedCharIndex).maxHp;
        GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/ATKPanel/Text").GetComponent<TextMeshProUGUI>().text 
            = dataParty.getPartyMember(selectedCharIndex).atk.ToString();
        GameObject.Find("Canvas/PausePanel/PartyPage/InfoPanel/ATKPanel/Text (1)").GetComponent<TextMeshProUGUI>().text 
            = dataParty.getPartyMember(selectedCharIndex).def.ToString();

    }
    public void putInCircle(int index)
    {
        Color32 color = new Color32(71, 71, 71, 255);
        Sprite icon = null;
        
        //if character already exists in party
        if (dataParty.checkCharacterValid(selectedCharIndex) != -1)
        {
            color = new Color32(71, 71, 71, 255);
            switch (dataParty.checkCharacterValid(selectedCharIndex))
            {
                case 0:
                    print("trySwitch color");
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    break;
                case 1:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    break;
                case 2:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    break;
                case 3:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    break;
            }
            dataParty.addCharacter(dataParty.checkCharacterValid(selectedCharIndex), -1);
        }
        switch (selectedCharIndex)
        {
            case 0:
                color = new Color32(78, 46, 153, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 1:
                color = new Color32(61, 27, 19, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 2:
                color = new Color32(153, 47, 72, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 3:
                color = new Color32(39, 121, 130, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 4:
                color = new Color32(95, 173, 52, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 5:
                color = new Color32(243, 200, 122, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 6:
                color = new Color32(118, 139, 145, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
            case 7:
                color = new Color32(36, 36, 36, 255);
                icon = dataParty.getPartyMember(selectedCharIndex).overSprite;
                break;
        }
        switch (index)
        {
            case 0:
                print("trySwitch color");
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront").GetComponent<Image>().color = color;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().color = Color.white;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().sprite = icon;
                break;
            case 1:
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop").GetComponent<Image>().color = color;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().color = Color.white;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().sprite = icon;
                break;
            case 2:
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot").GetComponent<Image>().color = color;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().color = Color.white;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().sprite = icon;
                break;
            case 3:
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack").GetComponent<Image>().color = color;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().color = Color.white;
                GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().sprite = icon;
                break;
        }
        dataParty.addCharacter(index, selectedCharIndex);
        if(dataParty.checkCharacterValid(0) == -1)
        {
            print("help");
            dataParty.addCharacter(index, 0);
            color = new Color32(78, 46, 153, 255);
            icon = dataParty.getPartyMember(0).overSprite;
            switch (index)
            {
                case 0:
                    print("trySwitch color");
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().color = Color.white;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 1:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().color = Color.white;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 2:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().color = Color.white;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 3:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack").GetComponent<Image>().color = color;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().color = Color.white;
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
            }
        }
        else
        {
            cancel();
        }
        
    }
    public void spinCircle()
    {
        dataParty.spinCharList();
        for(int i = 0; i<4; i++)
        {
            Color32 color = new Color32();
            Sprite icon = null;
                switch (dataParty.getPartyIndex(i))
            {
            case 0:
                    color = new Color32(78, 46, 153, 255);
                    icon = dataParty.getPartyMember(0).overSprite;
            break;
            case 1:
                    color = new Color32(61, 27, 19, 255);
                    icon = dataParty.getPartyMember(1).overSprite;
                    break;
            case 2:
                    color = new Color32(153, 47, 72, 255);
                    icon = dataParty.getPartyMember(2).overSprite;
                    break;
            case 3:
                    color = new Color32(39, 121, 130, 255);
                    icon = dataParty.getPartyMember(3).overSprite;
                    break;
            case 4:
                color = new Color32(95, 173, 52, 255);
            break;
            case 5:
                color = new Color32(243, 200, 122, 255);
            break;
            case 6:
                color = new Color32(118, 139, 145, 255);
            break;
            case 7:
                color = new Color32(36, 36, 36, 255);
            break;
            }
            switch (i)
            {
                case 0:
                    print("trySwitch color");
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront").GetComponent<Image>().color = color;
                    if(icon != null)
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().color = Color.white;
                    else
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleFront/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 1:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop").GetComponent<Image>().color = color;
                    if (icon != null)
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().color = Color.white;
                    else
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleTop/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 2:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot").GetComponent<Image>().color = color;
                    if (icon != null)
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().color = Color.white;
                    else
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBot/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
                case 3:
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack").GetComponent<Image>().color = color;
                    if (icon != null)
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().color = Color.white;
                    else
                        GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().color = new Color(255, 255, 255, 0);
                    GameObject.Find("Canvas/PausePanel/PartyPage/PartyPageFront/BaseCircle/CircleBack/CharacterSprite").GetComponent<Image>().sprite = icon;
                    break;
            }

        }
    }

    public void gotoConsume(int which)
    {
        selectedItemIndex = which;
        foreach (Button child in GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel").GetComponentsInChildren<Button>())
        {

            //child.enabled = false;
            child.interactable = false;
        }
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/KeyItemsText").GetComponent<Button>().interactable = false;
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume").SetActive(true);
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume").GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume"));
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume/Text").GetComponent<TextMeshProUGUI>().text = "Consume?";
        
        if (!dataItem.getItem(dataItem.items[which]).consume)
        {
            GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume/Text").GetComponent<TextMeshProUGUI>().text = "Drop?";
            //GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume").GetComponent<Button>().interactable = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume"));
        }
    }
    public void Update()
    {
        if(currentTab == 1)
        {
            if (EventSystem.current.currentSelectedGameObject == GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel/Panel"))
            {
                changeDisplayedItem(0);
            }
            else if (EventSystem.current.currentSelectedGameObject == GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel/Panel (1)"))
            {
                changeDisplayedItem(1);
            }
            else if (EventSystem.current.currentSelectedGameObject == GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel/Panel (2)"))
            {
                changeDisplayedItem(2);
            }
            else if (EventSystem.current.currentSelectedGameObject == GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel/Panel (3)"))
            {
                changeDisplayedItem(3);
            }
            else if (EventSystem.current.currentSelectedGameObject == GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel/Panel (4)"))
            {
                changeDisplayedItem(4);
            }
        }
        
    }
    public void changeDisplayedItem(int index)
    {
        int itemIndex = dataItem.items[index];

        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Outline/ItemPanel/ItemIcon").GetComponent<Image>().sprite = dataItem.getItem(itemIndex).art;
        if(GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Outline/ItemPanel/ItemIcon").GetComponent<Image>().sprite == null)
        {
            GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Outline/ItemPanel/ItemIcon").GetComponent<Image>().enabled = false;
        }
        else
        {
            GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Outline/ItemPanel/ItemIcon").GetComponent<Image>().enabled = true;
        }
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Flavor/Text").GetComponent<TextMeshProUGUI>().text = dataItem.getItem(itemIndex).flavor;
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Effect/Text").GetComponent<TextMeshProUGUI>().text = dataItem.getItem(itemIndex).effectDescription;
        //GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/ItemName/Text").GetComponent<Text>().text = data.GetComponent<DataRecorderScript>().getItem(itemIndex).itemName;
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/ItemName/Text").GetComponent<TextMeshProUGUI>().text = dataItem.getItem(itemIndex).itemName;
    }
    public void reactivateItems()
    {
        foreach (Button child in GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/NavPanel").GetComponentsInChildren<Button>())
        {

            //child.enabled = false;
            child.interactable = true;
        }
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/KeyItemsText").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume").SetActive(false);
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel").SetActive(false);
    }
    public void gotoWhom()
    {
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel").SetActive(true);
        GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/Consume").SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel/Character"));
        for (int i = 0; i < 8; i++)
        {
            GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel").transform.GetChild(i).GetComponent<Image>().sprite = dataParty.getPartyMember(i).overSprite;
            //GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel").transform.GetChild(i).GetComponentInChildren<Text>().text
            GameObject.Find("Canvas/PausePanel/ItemsPage/ItemsPageFront/WhomPanel").transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text
                = dataParty.getPartyMember(i).hp + "/" + dataParty.getPartyMember(i).maxHp;
        }
    }
    public void useItem(int whom)
    {
        print("Used an item");
        if (dataItem.getItem(selectedItemIndex).hpRestore > 0)
        {
            dataParty.getPartyMember(whom).hp += dataItem.getItem(selectedItemIndex).hpRestore;
            if (dataParty.getPartyMember(whom).hp > dataParty.getPartyMember(whom).maxHp)
                dataParty.getPartyMember(whom).hp = dataParty.getPartyMember(whom).maxHp;
            
        }
        dataItem.usedItem(selectedItemIndex);
        cancel();
    }

    public void setupMod()
    {
        print("setting up mods");
        List<mods> tempEquipped = new List<mods>();
        for(int i = 0; i< dataMod.getEquippedMods().Count; i++)
        {
            tempEquipped.Add(dataMod.getEquippedMods()[i]);
        }
        for(int k = 0; k<tempEquipped.Count; k++)
        {
            print(tempEquipped[k].modName);
        }
        if (/*data.GetComponent<DataRecorderScript>().getFoundMods().Count != 0 &&*/
            GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel") != null /*&&
            GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>().Length < data.GetComponent<DataRecorderScript>().getFoundMods().Count*/)
        {
            for (int i = GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>().Length; i < dataMod.getFoundMods().Count; i++)
            {
                print("new mod");
                GameObject modDisplay = Instantiate(modPrefab, GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").transform);
                modDisplay.GetComponentsInChildren<TextMeshProUGUI>()[0].text = dataMod.getFoundMods()[i].modName;
                modDisplay.GetComponentsInChildren<TextMeshProUGUI>()[1].text = dataMod.getFoundMods()[i].cost.ToString();
                modDisplay.GetComponent<Button>().onClick.AddListener(() => { modButton(modDisplay); });
                for (int j = 0; j < tempEquipped.Count; j++) 
                { 
                    if(tempEquipped[j].modName == GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>()[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text)
                    {
                        tempEquipped.RemoveAt(j);
                        var colors = modDisplay.GetComponent<Button>().colors;
                        colors.normalColor = new Color(0.09411766f, 0.1215686f, 0.3215686f);
                        colors.selectedColor = new Color(0.09411766f, 0.1215686f, 0.3215686f, 0.5f);
                        modDisplay.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        modDisplay.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1);
                        modDisplay.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1);
                        modDisplay.GetComponent<Button>().colors = colors;
                        j = tempEquipped.Count;
                    }
                }
                //modEquip?.Invoke(data.GetComponent<DataRecorderScript>().getFoundMods()[i]);
                if (i == 0)
                {
                    defaultButton[2] = GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>()[0].gameObject;
                }
            }
        }
        GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/CapacityBar").GetComponent<Slider>().maxValue = dataMod.getMaxMP();
        GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/CapacityBar").GetComponent<Slider>().value = dataMod.getCurrentMP();
        GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/CapacityBar/Text").GetComponent<TextMeshProUGUI>().text = "MP:\n"
            + dataMod.getCurrentMP() 
            + "\n   /\n     " 
            + dataMod.getMaxMP();
    }
    public void resetModDisplay()
    {
        print("mods before reset:" + GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>().Length);
        for (int i = 0; i < GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>().Length; i++)
        {
            Destroy(GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>()[i].gameObject);
        }
        print("mods after reset:" + GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/Panel").GetComponentsInChildren<Button>().Length);
    }
    public void modButton(GameObject b)
    {
        print("button press");
        var colors = b.GetComponent<Button>().colors;
        //b.GetComponent<Button>().colors = colors;
        mods mod = null;
        for(int i = 0; i< dataMod.getFoundMods().Count; i++)
        {
            if(b.GetComponentsInChildren<TextMeshProUGUI>()[0].text == dataMod.getFoundMods()[i].modName)
            {
                mod = dataMod.getFoundMods()[i];
            }
        }
        if (colors.normalColor == new Color(1,1,1,1) && dataMod.equipMod(mod))
        {
            colors.normalColor = new Color(0.09411766f, 0.1215686f, 0.3215686f);
            colors.selectedColor = new Color(0.09411766f, 0.1215686f, 0.3215686f, 0.5f);
            b.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            b.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(1, 1, 1);
            b.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(1, 1, 1);
            b.GetComponent<Button>().colors = colors;
        }
        else if (colors.normalColor == new Color(0.09411766f, 0.1215686f, 0.3215686f))
        {
            dataMod.unequipMod(mod);
            colors.normalColor = new Color(1, 1, 1, 1);
            colors.selectedColor = new Color(0.09411766f, 0.1215686f, 0.3215686f, 1f);
            b.GetComponent<Image>().color = new Color(1, 1, 1, 0.3921569f);
            b.GetComponentsInChildren<TextMeshProUGUI>()[0].color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            b.GetComponentsInChildren<TextMeshProUGUI>()[1].color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            b.GetComponent<Button>().colors = colors;
        }
        GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/CapacityBar").GetComponent<Slider>().value = dataMod.getCurrentMP();
        GameObject.Find("Canvas/PausePanel/ModsPage/ModsPageFront/CapacityBar/Text").GetComponent<TextMeshProUGUI>().text = "MP:\n"
            + dataMod.getCurrentMP()
            + "\n   /\n     "
            + dataMod.getMaxMP();
    }

    public void cancel()
    {
        if(currentTab == 0)
        {
            reactivateCharacterButtons();
            optionsBox.SetActive(false);
            foreach (Button child in circle.GetComponentsInChildren<Button>())
            {
                child.interactable = false;
            }
            circle.GetComponent<Button>().interactable = true;
            infoPage.SetActive(false);
        }
        else if(currentTab == 1)
        {
            reactivateItems();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultButton[1]);
        }
    }
}
