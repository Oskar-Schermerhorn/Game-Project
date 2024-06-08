using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyUnitMoveSelect : MonoBehaviour
{
    protected List<move> moveset = new List<move>();
    protected List<move> validMoves = new List<move>();
    BattleUnitData moveData;
    protected ObjectLocator locator;
    public static event Action<int> moveSelected;
    public static event Action<List<int>> targetSelected;
    private void Awake()
    {
        turnManagement.EnemyTurn += PickMove;
        moveData = this.gameObject.GetComponent<BattleUnitData>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
    }
    virtual protected void PickMove(int position)
    {
        if(locator.locateObject(this.gameObject) == position)
        {
            moveset.Clear();
            moveset.AddRange(moveData.moveset);
            validMoves.Clear();
            for (int i = 0; i < moveset.Count; i++)
            {
                checkValid(moveset[i], position);
            }
            if(validMoves.Count> 0)
            {

                targetSelected(RandomTargets(validMoves[RandomMove()], position));
            }
            else
            {
                print("stuck, no moves");
            }
        }
    }
    virtual protected List<int> RandomTargets(move selectedMove, int position)
    {
        List<int> validTargets = new List<int>();

        if(selectedMove.targetType == targetType.UNMOVABLE)
        {
            validTargets.AddRange(selectedMove.targetPos);
            return validTargets;
        }

        if ((selectedMove.moveTargetType == moveTargets.ENEMY || selectedMove.moveTargetType == moveTargets.BOTH) && selectedMove.targetType == targetType.SINGLE)
        {
            validTargets.AddRange(getRange(position));
        }
        else if(selectedMove.moveTargetType == moveTargets.ENEMY || selectedMove.moveTargetType == moveTargets.BOTH)
        {
            validTargets.AddRange(selectedMove.targetPos);
        }
        if (selectedMove.moveTargetType == moveTargets.ALLY || selectedMove.moveTargetType == moveTargets.BOTH)
        {
            for (int i = 4; i < 8; i++)
            {
                if (i != position && locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health >0)
                {
                    validTargets.Add(i);
                }
            }
        }
        System.Random random = new System.Random();
        int pickedTarget = -1;
        List<int> SelectedTargets = new List<int>();
        for(int i =0; i<checkNumTargets(selectedMove); i++)
        {
            pickedTarget = random.Next(0, validTargets.Count);
            SelectedTargets.Add(validTargets[pickedTarget]);
        }
        print("enemy target");
        for( int i = 0; i< SelectedTargets.Count; i++)
        {
            print("picked target: " + SelectedTargets[0]);
        }
        
        return SelectedTargets;
    }
    protected int RandomMove()
    {
        System.Random random = new System.Random();
        int pickedMove = random.Next(0, validMoves.Count);
        //checking for the actual index, not the index of valid moves
        moveSelected(findMoveIndex(validMoves[pickedMove]));
        return pickedMove;
    }
    protected int findMoveIndex(move findThis)
    {
        for(int i = 0; i<moveset.Count; i++)
        {
            if(moveset[i] == findThis)
            {
                return i;
            }
        }
        return -1;
    }
    protected void checkValid(move checkingMove, int position)
    {
        if(checkingMove.moveTargetType == moveTargets.ENEMY)
        {
            if (checkEnemy(checkingMove, position))
                validMoves.Add(checkingMove);
        }
        //will not attack self
        else if(checkingMove.moveTargetType == moveTargets.ALLY)
        {
            if (checkAlly(position))
                validMoves.Add(checkingMove);
        }
        else if(checkingMove.moveTargetType == moveTargets.BOTH)
        {
            if (checkEnemy(checkingMove, position) || checkAlly(position))
                validMoves.Add(checkingMove);
        }
        else if(checkingMove.moveTargetType == moveTargets.SELF)
        {
            validMoves.Add(checkingMove);
        }
    }
    private bool checkAlly(int position)
    {
        for (int i = 4; i < 8; i++)
        {
            if (i != position && locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health >0)
            {
                return true;
            }
        }
        return false;
    }
    private bool checkEnemy(move checkingMove, int position)
    {
        return getRange(position).Count > 0;
    }
    protected List<int> getRange(int position)
    {
        List<int> range = new List<int>();
        range.Add(0);
        switch (position)
        {
            case 4:
                if (locator.locateObject(1).GetComponent<BattleUnitHealth>() != null && locator.locateObject(1).GetComponent<BattleUnitHealth>().health >0)
                    range.Add(1);
                if (locator.locateObject(2).GetComponent<BattleUnitHealth>() != null && locator.locateObject(2).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(2);
                break;
            case 5:
                if (locator.locateObject(1).GetComponent<BattleUnitHealth>() != null && locator.locateObject(1).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(1);
                break;
            case 6:
                if (locator.locateObject(2).GetComponent<BattleUnitHealth>() != null && locator.locateObject(2).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(2);
                break;


        }
        return range;
    }
    protected int checkNumTargets(move selectedMove)
    {
        if (selectedMove.targetType == targetType.SINGLE)
        {
            return 1;
        }
        else if (selectedMove.targetType == targetType.PAIRS)
        {
            return 2;
        }
        else if (selectedMove.targetType == targetType.THREES)
        {
            return 3;
        }
        else if (selectedMove.targetType == targetType.FOURS)
        {
            return 4;
        }
        return 8;
    }
    protected void useTargetSelected(int pickedMove, List<int> targets)
    {
        moveSelected(pickedMove);
        targetSelected(targets);
    }
    private void OnDisable()
    {
        turnManagement.EnemyTurn -= PickMove;
    }
}
