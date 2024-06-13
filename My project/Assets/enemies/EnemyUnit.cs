using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyUnit : BattleUnit
{

    public int exp;
    public changeCondition condition;
    public targeting target;
    public List<move> validMoves = new List<move> { };
    public int range;
    public int pick;

    #region block functions
    private void openBlock()
    {
        blockable = true;
    }
    private void closeBlock()
    {
        blockable = false;
    }
    public bool checkBlockable()
    {
        return blockable;
    }
    private void openParry()
    {
        parryable = true;
    }
    private void closeParry()
    {
        parryable = false;
    }
    public bool checkParriable()
    {
        return parryable;
    }
    #endregion


    #region for enemy units
    public void useTurn()
    {
        print("using turn");
        pos = transform.position;
        updateselection();
        updatemoveset();
    }

    public void updatemoveset()
    {
        bool onlyOne = true;/*
        for (int i = 4; i < 8; i++)
        {
            if (spawn.unitList[i].getAlive() &&
                (spawn.unitList[i].getUnitGO() != this.gameObject))
            {
                onlyOne = false;
            }
        }*/
        if (condition == changeCondition.HALFHEALTH && health <= maxhealth / 2)
        {
            for (int i = 0; i < 5; i++)
            {
               /* if (!onlyOne || (moveset[moveset.Count - 1].moveTargetType != moveTargets.ALLY && !adjusted))
                {
                    print("adding a move");
                    moveset.Add(moveset[moveset.Count - 1]);
                    adjusted = true;
                }*/
            }
        }
        else if ((condition == changeCondition.HALFHEALTH && adjusted) && !onlyOne)
        {
            for (int i = 0; i < 5; i++)
            {
                print("removing a move");
                moveset.RemoveAt(moveset.Count - 1 - i);
            }
            adjusted = false;
        }

        if (onlyOne)
        {
            print("only");
            adjusted = true;
            print("number of moves: " + moveset.Count);
            for (int i = 0; i < moveset.Count; i++)
            {
                /*if (moveset[i].moveTargetType == moveTargets.ALLY)
                {
                    print("removing a type of moves");
                    moveset.RemoveAt(i);
                    i--;
                }*/
            }
            for (int j = 0; j < moveset.Count; j++)
            {
                /*print(moveset[j].animationNames[0]);
                if (moveset[j].animationNames[0] == "Absorb")
                {
                    print(moveset[j].moveTargetType);
                }*/
            }
        }
        bool possibleTarget = false;
        validMoves.Clear();
        for (int i = 0; i < moveset.Count; i++)
        {
            /*switch (moveset[i].moveTargetType)
            {
                case moveTargets.ALLY:
                    break;
            }*/
            if (possibleTarget)
            {
                validMoves.Add(moveset[i]);
            }
        }


        int random = Random.Range(0, validMoves.Count);
        Use(moveset[random], moveTargeter(moveset[random]), 0);
    }
    public void updateselection()
    {
        if (target == targeting.LOWHP)
        {

        }
    }
    GameObject moveTargeter(move m)
    {/*
        if (m.moveTargetType == moveTargets.ENEMY)
        {
            range = spawn.unitList[turnManager.GetComponent<turnManagement>().turnNum].getRangeList().Count;
            return pickRandomTarget(0);
        }
        else if (m.moveTargetType == moveTargets.ALLY)
        {
            range = 4;
            return pickRandomTarget(4);
        }
        else if (m.moveTargetType == moveTargets.BOTH)
        {

        }
        else if (m.moveTargetType == moveTargets.SELF)
        {

        }*/
        return null;
    }

    public GameObject pickRandomTarget(int based)
    {/*

        print("picking random target" + based);
        bool invalidTarget = true;
        if (based == 0)
        {
            while (invalidTarget)
            {
                pick = Random.Range(0, range);
                if (spawn.unitList[spawn.unitList[spawn.findMe(this.gameObject)].getRangeList()[pick]].getAlive())
                {
                    //print(spawn.GetComponent<SpawnScript>().unitList[spawn.GetComponent<SpawnScript>().unitList[spawn.GetComponent<SpawnScript>().findMe(this.gameObject)].getRangeList()[pick]].getAlive());
                    //print("i think " + spawn.GetComponent<SpawnScript>().unitList[spawn.GetComponent<SpawnScript>().findMe(this.gameObject)].getRangeList()[pick] + "is alive");
                    invalidTarget = false;
                }
            }
            int index = spawn.unitList[spawn.findMe(this.gameObject)].getRangeList()[pick];
            return spawn.unitList[index].getUnitGO();
        }
        else if (based == 4)
        {

            while (invalidTarget)
            {
                pick = Random.Range(4, 8);
                if (spawn.unitList[pick].getAlive()
                    && (spawn.findMe(this.gameObject) != pick))
                {
                    invalidTarget = false;
                }

            }
            return spawn.unitList[pick].getUnitGO();
        }*/
        return null;

    }
    public void showHP()
    { /*
        GameObject HpBar = GameObject.Find("Canvas2/EnemyPositions/Position" + spawn.findMe(this.gameObject) + "/bar");
        for (int i = 0; i < 3; i++)
        {
            HpBar.GetComponentsInChildren<Image>()[i].enabled = true;
        }
        HpBar.GetComponent<Slider>().maxValue = maxhealth;
        HpBar.GetComponent<Slider>().value = health;*/
    }
    public void hideHP()
    {/*
        GameObject HpBar = GameObject.Find("Canvas2/EnemyPositions/Position" + spawn.findMe(this.gameObject) + "/bar");
        for (int i = 0; i < 3; i++)
        {
            HpBar.GetComponentsInChildren<Image>()[i].enabled = false;
        }*/
    }
    #endregion
}
