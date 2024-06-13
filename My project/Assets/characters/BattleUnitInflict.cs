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
        damageModifier = status.calcDamageMod();
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
            if (locator.locateObject(targets[targetIndex]).GetComponent<BattleUnitHealth>() != null && locator.locateObject(targets[targetIndex]).GetComponent<BattleUnitHealth>().health >0)
            {
                inflictType(currentMove, locator.locateObject(targets[targetIndex]), damageModifier, counter, targetIndex, successful);
                counter++;
            }
        }
        
    }
    void inflictType(move currentMove, GameObject target, int damageModifier, int HitNumber, int targetIndex, bool successful)
    {
        if (target.GetComponent<BattleUnitStatus>() != null && !currentMove.HasProperty(moveProperties.NULL)  && currentMove.Damage >=0)
            damageModifier -= target.GetComponent<BattleUnitStatus>().calcDefenseMod();
        if (targetIndex == 0)
        {
            inflictPrimary(currentMove, target, damageModifier, HitNumber, successful);
            if (!currentMove.HasProperty(moveProperties.NULL))
            {
                if(HitNumber ==0 || currentMove.action.type == actionCommandType.TIMED)
                    Inflict(currentMove, target, successful, parry, damageModifier, HitNumber);
                else
                    InflictSecondary(currentMove, target, successful, parry, damageModifier, HitNumber);

            }
                
        }
        else if (targetIndex < targets.Count)
        {
            inflictSecondary(currentMove, locator.locateObject(targets[targetIndex]), damageModifier, HitNumber, targetIndex, successful);
            if (!currentMove.HasProperty(moveProperties.NULL))
            {
                InflictSecondary(currentMove, target, successful, parry, damageModifier, HitNumber);
            }
        }
    }
    void inflictPrimary(move currentMove, GameObject target, int damageModifier, int HitNumber, bool successful)
    {
        if (!currentMove.HasProperty(moveProperties.NULL))
        {
            if (currentMove.Damage >= 0)
            {
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
                int heal = currentMove.Damage *-1;
                print("base heal: " + heal);
                print("damage modifier: " + damageModifier);
                if (damageModifier < 0)
                    heal += damageModifier;
                if (!successful)
                    heal /= 2;
                print("result: " + heal);
                target.GetComponent<BattleUnitHealth>().Heal(heal);
            }
        }
        if (successful)
        {
            /*if (currentMove.moveEffect.inflictStatus.effectName != "none")
            {
                if(currentMove.moveEffect.inflictStatus.effectName != "spin" && currentMove.moveEffect.inflictStatus.effectName != "reverseSpin")
                {
                    //inflict status
                    inflictStatus(currentMove.moveEffect.inflictStatus, target);
                }
                
            }
            if (currentMove.moveEffect.selfStatus.effectName != "none")
            {
                if (currentMove.moveEffect.selfStatus.effectName != "spin" && currentMove.moveEffect.selfStatus.effectName != "reverseSpin")
                {
                    if(currentMove.moveEffect.selfStatus.effectName != "recoil")
                    {
                        //inflict status
                        inflictStatus(currentMove.moveEffect.selfStatus, this.gameObject);
                    }
                    else
                    {
                            this.gameObject.GetComponent<BattleUnitHealth>().takeDamage(currentMove.damageValues[HitNumber] + damageModifier, true, false);
                        
                        
                    }
                }
            }
            if (currentMove.moveEffect.hazard != null)
            {
                // inflict hazard
                inflictHazard(currentMove.moveEffect.hazard, target);
            }
            if (currentMove.moveEffect.inflictField != null && currentMove.moveEffect.inflictField.effectName != "noneField")
            {
                //inflict field
                inflictField(currentMove.moveEffect.inflictField);
            }
            if (currentMove.moveEffect.selfField != null && currentMove.moveEffect.selfField.effectName != "noneField")
            {
                //inflict self field
                inflictSelfField(currentMove.moveEffect.selfField);
            }*/
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
    void inflictSecondary(move currentMove, GameObject target, int damageModifier, int HitNumber, int targetIndex, bool successful)
    {
        /*if (currentMove.damageValues.Length > 0)
        {
            if (currentMove.damageValues[HitNumber] >= 0)
            {
                //inflict damage
                int damage = currentMove.damageValues[HitNumber] + damageModifier;
                if (!successful)
                    damage /= 2;
                if (parry)
                {
                    damage = 0;
                }
                //GameObject newTarget = locator.locateObject(currentMove.targetPos[targetIndex]);
                if(target.GetComponent<BattleUnitHealth>() != null)
                    target.GetComponent<BattleUnitHealth>().takeDamage(damage, successful, parry);
            }
            else
            {
                //heal
                int heal = currentMove.damageValues[HitNumber] * -1;
                if (damageModifier < 0)
                    heal += damageModifier;
                if (!successful)
                    heal /= 2;
                //GameObject newTarget = locator.locateObject(currentMove.targetPos[targetIndex]);
                target.GetComponent<BattleUnitHealth>().Heal(heal);
            }
        }
        if (successful)
        {
            if (currentMove.moveEffect.inflictStatus.effectName != "none")
            {
                if(currentMove.moveEffect.inflictStatus.effectName != "spin" && currentMove.moveEffect.inflictStatus.effectName != "reverseSpin")
                {
                    //inflict status
                    inflictStatus(currentMove.moveEffect.inflictStatus, target);
                }
                
            }
            if (currentMove.moveEffect.hazard != null)
            {
                // inflict hazard
                inflictHazard(currentMove.moveEffect.hazard, target);
            }
        }*/
    }
    protected void inflictStatus(statusEffect status, GameObject target)
    {
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
        inflictType(currentMove, locator.locateObject(targets[0]), 0, 0, 0, true);
    }

    private void OnDisable()
    {
        targetController.confirmedTarget -= Prepare;
        EnemyUnitMoveSelect.targetSelected -= Prepare;
        ActionCommandHandler.Parried -= setParry;
        ActionCommandHandler.Additional -= addToDamageMod;
    }
}
