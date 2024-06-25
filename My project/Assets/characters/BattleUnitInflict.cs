using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattleUnitInflict : MonoBehaviour
{
    ObjectLocator locator;
    menuMoveHolder menuMove;
    BattleUnitStatus status;

    hazardHandler hazards;
    fieldHandlerScript fields;
    move currentMove;
    [SerializeField] int counter = 0;
    [SerializeField] int baseAttack = 0;
    [SerializeField] int damageModifier = 0;
    [SerializeField] bool successful = false;
    [SerializeField] bool parry = false;
    [SerializeField] List<int> targets;
    public static event Action actionCommandsDone;
    public static event Action<int> nextActionCommand;
    public static event Action<move, GameObject, bool, bool, int, int> Inflict;
    public static event Action<move, GameObject, bool, bool, int, int> InflictSecondary;
    public static event Action<int, int> SwapPositions;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        menuMove = GameObject.Find("Menu").GetComponent<menuMoveHolder>();
        status = this.gameObject.GetComponent<BattleUnitStatus>();
        targetController.confirmedTarget += Prepare;
        EnemyUnitMoveSelect.targetSelected += Prepare;
        ActionCommandHandler.Successful += setSuccessful;
        ActionCommandHandler.Parried += setParry;
        ActionCommandHandler.Additional += addToDamageMod;

    }
    private void Prepare(List<int> selectedTargets)
    {
        parry = false;
        currentMove = menuMove.currentMove;
        if(currentMove.action.type == actionCommandType.DEFENSE || currentMove.action.type == actionCommandType.NONE)
        {
            setSuccessful(true);
        }
        targets = selectedTargets;
        counter = 0;
        baseAttack = this.gameObject.GetComponent<BattleUnitStat>().Attack;
        damageModifier = status.calcDamageMod();
        if (currentMove.HasProperty(moveProperties.USEBASEATTACK))
        {
            damageModifier += baseAttack;
        }
    }
    private void setSuccessful(bool result)
    {
        successful = result;
    }
    private void setParry(bool result)
    {
        parry = result;
    }
    void addToDamageMod(int amount)
    {
        damageModifier += amount;
    }
    public void inflict(int targetIndex)
    {
        if(targets.Count > targetIndex)
        {
            if (!currentMove.HasProperty(moveProperties.NULL))
            {
                inflictDamage(currentMove, locator.locateObject(targets[targetIndex]), damageModifier, counter, targetIndex, successful);
            }
            for (int i = 0; i < currentMove.MoveEffects.Count; i++)
            {
                if ((successful && currentMove.MoveEffects[i].Condition == effectCondition.SUCCESSFUL)
                    || currentMove.MoveEffects[i].Condition == effectCondition.ALWAYS)
                {
                    if (currentMove.MoveEffects[i].Target == statusTarget.INFLICT)
                    {
                        inflictStatus(currentMove.MoveEffects[i], locator.locateObject(targets[targetIndex]));
                    }
                    else if (currentMove.MoveEffects[i].Target == statusTarget.SELF)
                    {
                        inflictStatus(currentMove.MoveEffects[i], this.gameObject);
                    }
                }
                else if (currentMove.MoveEffects[i].Condition == effectCondition.RANDOM)
                {

                }
            }

            counter++;
        }
        
    }

    void inflictDamage(move currentMove, GameObject target, int damageModifier, int HitNumber, int targetIndex, bool successful)
    {
        if (!currentMove.HasProperty(moveProperties.NULL))
        {
            
            if (currentMove.Damage >= 0)
            {
                if (target.GetComponent<BattleUnitStatus>() != null)
                    damageModifier -= target.GetComponent<BattleUnitStatus>().calcDefenseMod();
                int damage = currentMove.Damage + damageModifier;
                if (!successful)
                {
                    damage /= 2;
                }
                if (parry)
                {
                    damage = 0;
                }
                //inflict damage
                target.GetComponent<BattleUnitHealth>().takeDamage(damage, successful, parry);

            }
            else
            {
                //heal
                int heal = currentMove.Damage * -1;
                print("base heal: " + heal);
                print("damage modifier: " + damageModifier);
                if (damageModifier < 0)
                    heal += damageModifier;
                if (!successful)
                    heal /= 2;
                print("result: " + heal);
                target.GetComponent<BattleUnitHealth>().Heal(heal);
            }
            Inflict(currentMove, target, successful, parry, damageModifier, HitNumber);
        }

        

        if(target.GetComponent<BattleUnitStatus>() != null)
            target.GetComponent<BattleUnitStatus>().checkOnhit();
        if(currentMove.action.type != actionCommandType.TIMED || currentMove.action.buttons.Length - 1 <= HitNumber)
            actionCommandsDone();
        else if(currentMove.action.type == actionCommandType.TIMED)
        {
            nextActionCommand(HitNumber);
            setSuccessful(false);
        }
    }
    protected void inflictStatus(effect MoveEffect, GameObject target)
    {
        statusEffect status = MoveEffect.MoveStatus;
        if(MoveEffect.duration != 0)
        {
            status.duration = MoveEffect.duration;
        }
        if (MoveEffect.additional != 0)
        {
            status.additional = MoveEffect.additional;
        }
        if (MoveEffect.amount != 0)
        {
            status.amount = MoveEffect.amount;
        }

        target.GetComponent<BattleUnitStatus>().AddStatus(status);
    }
    public void inflictHazard(hazard hazard, GameObject target)
    {
        hazards.createHazard(hazard, locator.locateObject(target));
    }
    public void inflictField(fieldEffects field)
    {
        fields.setEnemyField(field);
    }
    public void inflictSelfField(fieldEffects field)
    {
        fields.setPlayerField(field);
    }
    public void inflictSpin()
    {
        /*inflictStatus(currentMove.moveEffect.inflictStatus, locator.locateObject(targets[0]));*/
    }
    public void inflictSwap()
    {
        SwapPositions(targets[0], targets[1]);
    }
    void InflictItem()
    {
        //inflictType(currentMove, locator.locateObject(targets[0]), 0, 0, 0, true);
    }

    private void OnDisable()
    {
        targetController.confirmedTarget -= Prepare;
        EnemyUnitMoveSelect.targetSelected -= Prepare;
        ActionCommandHandler.Parried -= setParry;
        ActionCommandHandler.Additional -= addToDamageMod;
    }
}
