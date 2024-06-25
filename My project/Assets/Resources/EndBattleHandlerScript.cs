using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System;

public class EndBattleHandlerScript : MonoBehaviour
{
    private DataRecorderCombat dataCombat;
    private DataRecorderParty dataParty;
    private DataRecorderEXP dataEXP;
    private DataRecorderProgress dataProgress;
    private ObjectLocator locator;
    public static event Action EndBattle;
    public static event Action<int> RecordBP;
    public bool leveling = false;
    [SerializeField] GameObject levelUpMenup;
    private void Start()
    {
        dataCombat = GameObject.Find("DataRecorder").GetComponent<DataRecorderCombat>();
        dataParty = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        dataEXP = GameObject.Find("DataRecorder").GetComponent<DataRecorderEXP>();
        dataProgress = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        locator = this.gameObject.GetComponent<ObjectLocator>();
    }
    public void checkBattleOver()
    {
        if (checkLostBattle())
        {
            print("lost");
            loseBattle();
        }
        else if (checkWonBattle()){
            print("Won");
            endBattle();
        }
    }
    public bool checkLostBattle()
    {
        bool L = true;
        List<GameObject> players = locator.getAll(true);
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<BattleUnitHealth>() != null && players[i].GetComponent<BattleUnitHealth>().health > 0)
            {
                L = false;
            }
        }

        return L;
    }
    public bool checkWonBattle()
    {
        bool W = true;
        List<GameObject> enemies = locator.getAll(false);
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<BattleUnitHealth>() != null && enemies[i].GetComponent<BattleUnitHealth>().health > 0)
            {
                W = false;
            }
        }
        return W;
    }
    private void loseBattle()
    {
        returnToOverworld();
        dataProgress.Reset();
    }

    // execute end battle
    private void endBattle()
    {
        //EndBattle();
        print("+" + dataCombat.getEnemies().Count + " coins");
        dataProgress.AquireCoins(dataCombat.getEnemies().Count);
        dataCombat.endCombat();
        Destroy(GameObject.Find("Menu"));
        updateHealth();
        updateBP();
        awardEXP();
        if (!leveling)
        {
            Invoke("returnToOverworld", 1.5f);
        }
        else
        {
            Instantiate<GameObject>(levelUpMenup, GameObject.Find("Canvas").transform);
        }
        
    }
    void updateHealth()
    {
        for (int i = 0; i < 4; i++)
        {
            if (locator.locateObject(i).GetComponent<BattleUnitID>() != null)
            {
                if (locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                {
                    print("saved " + locator.locateObject(i).name + "'s hp at " + locator.locateObject(i).GetComponent<BattleUnitHealth>().health + " hp");
                    print("saving " + locator.locateObject(i).name + " to " + dataParty.getPartyMember(locator.locateObject(i).GetComponent<BattleUnitID>().ID).characterName);
                    dataParty.getPartyMember(locator.locateObject(i).GetComponent<BattleUnitID>().ID).hp = locator.locateObject(i).GetComponent<BattleUnitHealth>().health;
                }
                else
                {
                    print("saved " + locator.locateObject(i).name + "'s hp at " + 1 + " hp");
                    print("saving " + locator.locateObject(i).name + " to " + dataParty.getPartyMember(locator.locateObject(i).GetComponent<BattleUnitID>().ID).characterName);
                    dataParty.getPartyMember(locator.locateObject(i).GetComponent<BattleUnitID>().ID).hp = 1;
                }
            }
        }
    }
    void updateBP()
    {
        RecordBP(GameObject.Find("Canvas/Battery").GetComponent<batteryScript>().getBP());
    }
    private void awardEXP()
    {
        int total = 0;
        for (int i = 4; i < 8; i++)
        {
            if (locator.locateObject(i).GetComponent<EnemyUnitExp>() != null)
            {
                print("gained " + locator.locateObject(i).GetComponent<EnemyUnitExp>().exp + " exp");
                total += locator.locateObject(i).GetComponent<EnemyUnitExp>().exp;
            }
        }
        if (total > 100)
            total = 100;
        leveling = dataEXP.getEXP(total);
    }

    public void returnToOverworld()
    {
        SceneManager.LoadScene(sceneName: "Overworld");
    }

}
