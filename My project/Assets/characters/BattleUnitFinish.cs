using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattleUnitFinish : MonoBehaviour
{
    public static event Action EndTurn;
    public static event Action TurnAgain;
    SpinHandler spin;
    ObjectLocator locator;
    EndBattleHandlerScript endBattle;
    public bool spinDone = false;
    public bool spinInProgress = false;
    private void Awake()
    {
        spin = GameObject.Find("BattleHandler").GetComponent<SpinHandler>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        endBattle = GameObject.Find("BattleHandler").GetComponent<EndBattleHandlerScript>();
        MoveCoroutine.spinComplete += setCompleteSpin;
        MoveCoroutine.StartedSpin += setSpinInProg;
    }
    protected void finishAttack(int hold)
    {
        endOfAttack(true);
    }
    public IEnumerator waitSpin(bool end)
    {
        if (spin.checkSpin() && !spinInProgress && !endBattle.checkWonBattle() && !endBattle.checkLostBattle())
        {
            //spinDone = false;
            if (locator.locateObject(0).GetComponent<BattleUnitHealth>() == null || locator.locateObject(0).GetComponent<BattleUnitHealth>().health <= 0)
            {
                spin.spin(locator.locateObject(0));
            }
            if (locator.locateObject(4).GetComponent<BattleUnitHealth>() == null || locator.locateObject(4).GetComponent<BattleUnitHealth>().health <= 0)
                spin.spin(locator.locateObject(4));
            yield return new WaitUntil(() => spinDone);
            spinDone = false;
        }
        else if (spinInProgress)
        {
            yield return new WaitUntil(() => spinDone);
            spinDone = false;
        }

        
        if (end)
            EndTurn();
        else
            TurnAgain();
    }
    private void setCompleteSpin(bool done)
    {
        spinDone = done;
        spinInProgress = false;
    }
    private void setSpinInProg()
    {
        spinInProgress = true;
    }
    virtual protected void endOfAttack(bool end)
    {
        StartCoroutine(waitSpin(end));
        if(this.gameObject.GetComponent<BattleUnitHealth>().health > 0)
        {
            //reset animation
            this.gameObject.GetComponent<BattleUnitAnimate>().changeAnimation("neutral");
            //reset position
            this.gameObject.GetComponent<BattleUnitAttackOffset>().returnTo();
        }

        endBattle.checkBattleOver();
    }
    
    virtual protected void finishAttackWithoutEnding()
    {
        endOfAttack(false);
    }
    public void forceFinishAttack()
    {
        StartCoroutine(waitSpin(true));
        endBattle.checkBattleOver();
    }
    private void OnDisable()
    {
        MoveCoroutine.spinComplete -= setCompleteSpin;
        MoveCoroutine.StartedSpin -= setSpinInProg;
    }
}
