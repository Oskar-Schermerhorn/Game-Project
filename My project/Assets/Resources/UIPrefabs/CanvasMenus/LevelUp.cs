using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System;

public class LevelUp : MonoBehaviour
{
    DataRecorderParty party;
    DataRecorderBattery battery;
    DataRecorderMods mods;
    EndBattleHandlerScript end;
    public static event Action RefillBP;
    bool chosen = false;
    private void Awake()
    {
        party = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        battery = GameObject.Find("DataRecorder").GetComponent<DataRecorderBattery>();
        mods = GameObject.Find("DataRecorder").GetComponent<DataRecorderMods>();
        end = GameObject.Find("BattleHandler").GetComponent<EndBattleHandlerScript>();
    }
    private void Start()
    {
        print("selecting hp");
        print(this.transform.Find("HP") != null);
        EventSystem.current.firstSelectedGameObject = this.transform.Find("HP").gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.transform.Find("HP").gameObject);
        print(EventSystem.current.currentSelectedGameObject.name);
        //this.transform.Find("HP").gameObject.GetComponent<Button>().
    }
    public void Level(int option)
    {
        if (!chosen)
        {
            print("level up option " + option);
            chosen = true;
            switch (option)
            {
                case 0:
                    party.levelUp();
                    break;
                case 1:
                    battery.levelUp();
                    break;
                case 2:
                    mods.levelUp();
                    break;
            }
            for (int i = 0; i < 8; i++)
            {
                party.getPartyMember(i).hp = party.getPartyMember(i).maxHp;
            }
            RefillBP();
        }
        end.returnToOverworld();

        
    }
}
