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
    move RevivePlayer;
    move ReviveEnemy;
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
        move Revive = RevivePlayer;
        if (offensiveRevive)
        {
            Revive = ReviveEnemy;
        }

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
