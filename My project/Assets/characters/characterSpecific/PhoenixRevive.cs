using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhoenixRevive : BattleUnitHealth
{
    ObjectLocator locator;
    turnManagement turn;
    [SerializeField] bool offensiveRevive = false;
    [SerializeField] statusEffect egg;
    public static event Action<move> reviveMove;
    int targeting = 4;

    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        targetController.confirmedTarget += targets => updateTargeting(targets[0]);
    }
    void updateTargeting(int target)
    {
        targeting = target;
    }
    override protected void Die()
    {
        this.GetComponent<BattleUnitStatus>().AddStatus(egg);
        if(turn.turnState == battleState.PLAYERTURN)
        {
            offensiveRevive = true;
            //this.gameObject.GetComponent<BattleUnitAnimate>().changeAnimation("Dead");
            //transform.position = GameObject.Find("BattleHandler/Positions/EnemyPositions/position" + targeting).transform.position;
            //this.gameObject.GetComponent<BattleUnitCamera>().zoomOut();
            //this.gameObject.GetComponent<BattleUnitCamera>().stopScreenShake();
            //this.gameObject.GetComponent<BattleUnitFinish>().forceFinishAttack();
        }
        else
        {
            this.gameObject.GetComponent<BattleUnitAnimate>().changeAnimation("Die");
        }
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }
    public move revive()
    {
        int[] targets;
        if (offensiveRevive)
        {
            targets = new int[] { 7,6,5,4 };
        }
        else
        {
            int me = locator.locateObject(this.gameObject);
            targets = new int[] { 0,1,2,3 };
            List<int> newTargets = new List<int>();
            for(int i =0; i< 4; i++)
            {
                if(targets[i] != me)
                {
                    newTargets.Add(targets[i]);
                }
            }
            targets = new int[3];
            for (int i = 0; i<3; i++)
            {
                targets[i] = newTargets[i];
            }
        }
        move Revive = new move(new string[] { "Revive" }, 0, new int[] { 2 }, moveTargets.BOTH, new effect("flare", "none"), targets, targetType.UNMOVABLE, new actionCommand());
        reviveMove(Revive);

        offensiveRevive = false;
        return Revive;
    }
    public void FinishReviving()
    {
        this.gameObject.GetComponent<Animator>().Play("neutral");
        this.gameObject.GetComponent<Transform>().position = GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + locator.locateObject(this.gameObject)).transform.position;
        this.gameObject.GetComponent<BattleUnitHealth>().health = 4;
        CallHit();
    }
    private void OnDisable()
    {
        targetController.confirmedTarget -= targets => updateTargeting(targets[0]);
    }
}
