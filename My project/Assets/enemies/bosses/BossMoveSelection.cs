using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum desperationCondition { NONE, HEALTH, TURN, OTHER}
public class BossMoveSelection : EnemyUnitMoveSelect
{
    //List<move> moveset = new List<move>();
    //List<move> validMoves = new List<move>();
    BossUnitData bossMoveData;
    [SerializeField] int energy;
    [SerializeField] int maxEnergy;
    [SerializeField] int limitedMoves;
    [SerializeField] desperationCondition condition;
    [SerializeField] int conditionValue;
    [SerializeField] int turn=  0;

    private void Awake()
    {
        turnManagement.EnemyTurn += PickMove;
        bossMoveData = this.gameObject.GetComponent<BossUnitData>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
    }

    protected override void PickMove(int position)
    {
        print("receive signal");
        if (locator.locateObject(this.gameObject) == position)
        {
            moveset.Clear();
            if(energy < maxEnergy || (limitedMoves <= 0 && !DesperiationCondition()))
            {
                print("adding regular moves");
                moveset.AddRange(bossMoveData.RegularMoves);
            }
            if (DesperiationCondition() && !(energy >= maxEnergy && limitedMoves > 0))
            {
                print("adding desperation moves");
                moveset.AddRange(bossMoveData.DesperationMoveset);
            }
            if(limitedMoves > 0)
            {
                print("adding limited moves");
                moveset.AddRange(bossMoveData.LimitedMoveset);
            }
            validMoves.Clear();
            print("validating moves");
            for (int i = 0; i < moveset.Count; i++)
            {
                //checkValid(moveset[i], position);
            }
            moveset.Clear();
            moveset.AddRange(bossMoveData.RegularMoves);
            moveset.AddRange(bossMoveData.DesperationMoveset);
            moveset.AddRange(bossMoveData.LimitedMoveset);
            for (int i = 0; i < validMoves.Count; i++)
            {
                if (validMoves[i].cost > energy)
                {
                    validMoves.RemoveAt(i);
                    i--;
                }
            }

            print("picking a move from this selection:");
            for(int i = 0; i< validMoves.Count; i++)
            {
                print(validMoves[i].Name);
            }
            if (validMoves.Count > 0)
            {
                //roll
                System.Random random = new System.Random();
                int pickedMove = random.Next(0, validMoves.Count);
                //find index of that move in whole moveset
                int moveIndex = findMoveIndex(validMoves[pickedMove]);
                energy -= validMoves[pickedMove].cost;
                if (moveIndex > bossMoveData.RegularMoves.Count + bossMoveData.DesperationMoveset.Count-1)
                {
                    limitedMoves--;
                }
                useTargetSelected(moveIndex, RandomTargets(validMoves[pickedMove], position));
                turn++;
            }
            else
            {
                print("stuck, no moves");
            }
        }
    }

    bool DesperiationCondition()
    {
        switch (condition)
        {
            case desperationCondition.HEALTH:
                return (this.gameObject.GetComponent<BattleUnitHealth>().health <= conditionValue);
            case desperationCondition.TURN:
                return (turn >= conditionValue);
        }
        return false;
    }
    private void OnDisable()
    {
        turnManagement.EnemyTurn -= PickMove;
    }
}
