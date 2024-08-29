using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class statusAnimation : MonoBehaviour
{
    public GameObject target;
    public statusEffect status;
    public static event Action<move, GameObject, bool, bool, int> Inflict;
    EndBattleHandlerScript endBattle;
    turnManagement turn;
    move statusDamage;

    private void Awake()
    {
        endBattle = GameObject.Find("BattleHandler").GetComponent<EndBattleHandlerScript>();
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        turn.statusEffectsPlaying++;
        statusDamage = new move(new moveProperty());
    }
    void InflictStatusDamage()
    {
        print(status.effectName);
        int damage = status.amount;
        if(target.transform.Find("Canvas/StatusIcon(" + status.effectName + ")")!= null && target.transform.Find("Canvas/StatusIcon("+status.effectName+")").GetComponent<statusIcon>().statusDuration >= 5)
        {
            damage += status.additional;
        }
        if (target.transform.Find("Canvas/StatusIcon(poison)") != null && status.effectName == "poison")
        {
            print("adding poison turns");
            damage += target.transform.Find("Canvas/StatusIcon(poison)").GetComponent<statusIcon>().poisonTurns;
        }
        if( status.effectName == "revive")
        {
            print("revive going through");
            move reviveMove = target.GetComponent<PhoenixRevive>().revive();
            /*for(int i = 0; i<reviveMove.targetPos.Length; i++)
            {
                ObjectLocator locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
                if (locator.locateObject(reviveMove.targetPos[i]).GetComponent<BattleUnitHealth>() != null && locator.locateObject(reviveMove.targetPos[i]).GetComponent<BattleUnitHealth>().health >0)
                {
                    locator.locateObject(reviveMove.targetPos[i]).GetComponent<BattleUnitHealth>().takeDamage(reviveMove.Damage, true, false);
                    locator.locateObject(reviveMove.targetPos[i]).GetComponent<BattleUnitStatus>().AddStatus(reviveMove.moveEffect.inflictStatus);
                    Inflict(reviveMove, locator.locateObject(reviveMove.targetPos[i]), true, false, 0, 0);
                }
            }*/
        }
        else
        {
            print("normal status");
            print("damage dealt: " + damage);
            target.GetComponent<BattleUnitHealth>().takeDamage(damage, true, false, bashProperties.NORMAL);
            statusDamage.Damage = damage;
            //Inflict(new move(new string[] { }, 0, new int[] { damage }, moveTargets.BOTH, new effect("none", "none"), new int[] { }, targetType.UNMOVABLE, new actionCommand()), target, true, false, 0, 0);
        }
        
    }
    public void endAnimation()
    {
        if(target.GetComponent<BattleUnitAnimate>()!= null)
        {
            target.GetComponent<BattleUnitAnimate>().changeAnimation("neutral");
        }
        turn.statusEffectsPlaying--;
        endBattle.checkBattleOver();
        Destroy(this.gameObject);
    }
    public void finishRevive()
    {
        target.GetComponent<PhoenixRevive>().FinishReviving();
    }
}
