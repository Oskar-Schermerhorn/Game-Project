using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum interactableType { ITEM, MOD, HEAL, TALK, CHECK, USE, SHOP, EVENT}
public class Interactable : MonoBehaviour
{
    public string id;
    public interactableType type;
    public bool reusable;
    public item itemDrop;
    public mods modDrop;
    public bool used;
    public List<string> dialogue;
    DataRecorderInteractables data;
    DataRecorderItems dataItem;
    DataRecorderMods dataMod;
    DataRecorderProgress dataProgress;
    public TextBoxController textbox;
    [SerializeField] string EventName;
    public static event Action<string> TriggerEvent;
    
    private void Start()
    {
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderInteractables>();
        dataItem = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
        dataMod = GameObject.Find("DataRecorder").GetComponent<DataRecorderMods>();
        dataProgress = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        //textbox = GameObject.Find("Canvas/TextBoxHolder").GetComponent<TextBoxController>();
        if (data.checkUsedInteractable(id))
        {
            gameObject.layer = 3;
        }
    }
    virtual public void Use()
    {
        print("using interactable");
        switch (type)
        {
            case interactableType.ITEM:
                if (dataItem.getOpenItemSlots().Count>0)
                {
                    if(dialogue[dialogue.Count-1] == "Inventory full")
                    {
                        dialogue.RemoveAt(dialogue.Count - 1);
                    }
                    reusable = false;
                    dataItem.addItem(itemDrop.index);
                    dataProgress.SetProgressTracker(this.gameObject, true, true);
                }
                else
                {
                    reusable = true;
                    if (dialogue[dialogue.Count - 1] == "Inventory full")
                    {
                        dialogue.RemoveAt(dialogue.Count - 1);
                    }
                    dialogue.Add("Inventory full");
                }
                break;
            case interactableType.HEAL:
                break;
            case interactableType.MOD:
                dataMod.foundNewMod(modDrop.id);
                break;
            case interactableType.TALK:
                break;
            case interactableType.EVENT:
                if(TriggerEvent != null)
                {
                    TriggerEvent(EventName);
                }
                break;
        }
        if (!reusable)
        {
            gameObject.layer = 3;
            data.useInteractable(id);
        }
        if (dialogue.Count > 0)
        {
            //textbox.show(dialogue);
            print("talk");
            textbox.StartText(dialogue, type);
        }
    }
}
