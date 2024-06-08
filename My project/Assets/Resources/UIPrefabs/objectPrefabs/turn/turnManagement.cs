using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public enum battleState { START, PLAYERTURN, ENEMYTURN, HAZARDS, WON, LOST }

public class turnManagement : MonoBehaviour
{
    public battleState turnState;
    public int turnNum = 0;
    [SerializeField] SpawnScript spawn;
    ObjectLocator locator;
    public static event Action NewTurn;
    public static event Action SpinTurn;
    public static event Action<int> PlayerTurn;
    public static event Action<int> EnemyTurn;
    public static event Action Status;
    public int statusEffectsPlaying = 0;

    void Start()
    {
        
        turnState = battleState.START;
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        spawn = GameObject.FindWithTag("BattleHandler").GetComponent<SpawnScript>();
        SpawnScript.battleSetup += executeTurn;
        MoveCoroutine.spinComplete += spinTurn;
        spawn.startUp();
        NewTurn();
        turnState = battleState.PLAYERTURN;
        BattleUnitFinish.EndTurn += EndTurn;
        BattleUnitFinish.TurnAgain += TurnAgain;
        
        
    }

    void TurnAgain()
    {
        if(NewTurn != null)
            NewTurn();
        StartCoroutine(TurnAgainNextFrame());
    }
    IEnumerator TurnAgainNextFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        executeTurn();
    }
    public void EndTurn()
    {
        print("ended turn");
        StartCoroutine(EndTurnNextFrame());
    }
    IEnumerator EndTurnNextFrame()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        while (statusEffectsPlaying > 0)
        {
            print("waiting for status effects");
            yield return null;
        }
        //yield return new WaitWhile(statusEffectsPlaying == 0);
        //yield return new WaitForSeconds(0.2f);
        changeTurn();
    }
    //change turn
    public void changeTurn()
    {
        turnNum++;
        
        if (turnNum > 8)
        {
            turnNum = 0;
        }
        updateTurnState();
        if (turnNum == 8)
        {
            checkStatus();
            executeStatusAnimations();
        }
        else
        {
            executeNextTurn();
        }
        


    }
    public void executeNextTurn()
    {
        if(NewTurn != null)
            NewTurn();
        if (!checkValidTurn())
        {
            changeTurn();
        }
        else
        {
            executeTurn();
        }
    }

    private bool checkValidTurn()
    {
        if(turnNum < 8)
        {
            if (locator.locateObject(turnNum).GetComponent<BattleUnitHealth>() == null)
                return false;
            if (locator.locateObject(turnNum).GetComponent<BattleUnitHealth>().health <= 0)
                return false;
            if (locator.locateObject(turnNum).GetComponent<BossUnitData>() != null)
                return true;
            for (int i = 0; i < locator.locateObject(turnNum).GetComponent<BattleUnitStatus>().myStatus.Count; i++)
            {
                if (locator.locateObject(turnNum).GetComponent<BattleUnitStatus>().myStatus[i].type == statusType.STUN)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    private void executeTurn()
    {
        if(turnState == battleState.PLAYERTURN && PlayerTurn != null)
        {
            PlayerTurn(turnNum);
        }
        else if(turnState == battleState.ENEMYTURN && EnemyTurn != null)
        {
            EnemyTurn(turnNum);
        }
    }

    private void updateTurnState()
    {
        switch (turnNum)
        {
            case 0:
                turnState = battleState.PLAYERTURN;
                break;
            case 4:
                turnState = battleState.ENEMYTURN;
                print("sending enemy turn signal");
                break;
            case 8:
                turnState = battleState.HAZARDS;
                break;
        }
    }

    //set hazards
    public void finishedHazards()
    {
        /*turnState = battleState.PLAYERTURN;
        if (!checkBattleOver())
        {
            if (!spawn.unitList[0].getAlive())
                spawn.spin();
            if (!spawn.unitList[4].getAlive())
                spawn.spinEnemy();
        }*/
      //  endTurn();
    }
    
    private void spinTurn(bool done)
    {
        if(done)
        {
            SpinTurn();
            print("spin turn");
            if (turnNum < 4)
            {
                if (GameObject.Find("Menu").GetComponent<menuExecute>().manual)
                {
                    if (locator.locateObject(turnNum).GetComponent<BattleUnitHealth>() == null || locator.locateObject(turnNum).GetComponent<BattleUnitHealth>().health <= 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                            {
                                if (GameObject.Find("Menu").GetComponent<menuExecute>().left)
                                    gameObject.GetComponent<SpinHandler>().spin(locator.locateObject(i));
                                else
                                    gameObject.GetComponent<SpinHandler>().reverseSpin(locator.locateObject(i));
                                break;
                            }
                        }
                    }
                    else
                    {
                        print("playerturn" + turnNum);
                        PlayerTurn(turnNum);
                        GameObject.Find("Menu").GetComponent<menuExecute>().manual = false;
                    }
                    
                }
                
            }
        }
        
    }
    private void checkStatus()
    {
        for(int i =0; i< 8; i++)
        {
            if(locator.locateObject(i).GetComponent<BattleUnitStatus>() != null && locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus.Count > 0)
            {
                Status();
                break;
            }
        }
    }
    private void executeStatusAnimations()
    {
        StartCoroutine(StatusAnimation());
    }
    IEnumerator StatusAnimation()
    {
        for (int i = 0; i < 8; i++)
        {
            if (locator.locateObject(i).GetComponent<BattleUnitStatus>() != null && locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus.Count > 0 
                && locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health >0
                || locator.locateObject(i).GetComponent<PhoenixRevive>() != null)
            {
                for (int j = 0; j < locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus.Count; j++)
                {
                    if (locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus[j].animation != null && locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus[j].time == statusTime.STARTTURN)
                    {
                        locator.locateObject(i).GetComponent<BattleUnitStatus>().myStatus[j].animate(locator.locateObject(i).transform, i);
                        yield return new WaitUntil(() => locator.locateObject(i).GetComponentsInChildren<statusAnimation>().Length == 0);
                    }
                    
                }
            }
        }
        print("done");
        for (int i = 0; i < 8; i++)
        {
            if (locator.locateObject(i).GetComponent<BattleUnitFinish>() != null)
            {
                StartCoroutine(locator.locateObject(i).GetComponent<BattleUnitFinish>().waitSpin(true));
                break;
            }
        }
        /*
         * 
        */

    }

    private void OnDisable()
    {
        BattleUnitFinish.EndTurn -= EndTurn;
        BattleUnitFinish.TurnAgain -= TurnAgain;
        SpawnScript.battleSetup -= executeTurn;
        MoveCoroutine.spinComplete -= spinTurn;
        
}
}
