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
            moveset.AddRange(moveData.getMoveset());
            validMoves.Clear();
            for (int i = 0; i < moveset.Count; i++)
            {
                checkValid(moveset[i]);
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
        if (selectedMove.HasProperty(targetProperties.SINGLETARGET))
        {
            if (selectedMove.HasProperty(targetProperties.FRONT))
            {
                if (selectedMove.HasProperty(targetProperties.PLAYERS))
                {
                    List<GameObject> players = locator.getAll(true);
                    validTargets.Add(locator.locateObject(players[0]));

                }
                if (selectedMove.HasProperty(targetProperties.ENEMIES))
                {
                    List<GameObject> enemies = locator.getAll(false);
                    if (!selectedMove.HasProperty(targetProperties.SELF))
                    {
                        enemies.Remove(this.gameObject);
                    }
                    if (enemies.Count > 0)
                    {
                        validTargets.Add(locator.locateObject(enemies[0]));
                    }
                }
            }
            if (selectedMove.HasProperty(targetProperties.FREETARGET))
            {
                if (selectedMove.HasProperty(targetProperties.PLAYERS))
                {
                    List<GameObject> players = locator.getAll(true);
                    for (int i = 0; i < players.Count; i++)
                    {
                        if (players[i].GetComponent<BattleUnitHealth>().health > 0)
                        {
                            validTargets.Add(locator.locateObject(players[i]));
                        }
                        
                    }

                }
                if (selectedMove.HasProperty(targetProperties.ENEMIES))
                {
                    List<GameObject> enemies = locator.getAll(false);
                    if (!selectedMove.HasProperty(targetProperties.SELF))
                    {
                        enemies.Remove(this.gameObject);
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        validTargets.Add(locator.locateObject(enemies[i]));
                    }

                }
            }
            if (selectedMove.HasProperty(targetProperties.SELF))
            {
                validTargets.Add(locator.locateObject(this.gameObject));
            }

            System.Random random = new System.Random();
            int pickedTarget = -1;
            List<int> SelectedTargets = new List<int>();
            for (int i = 0; i < checkNumTargets(selectedMove); i++)
            {
                pickedTarget = random.Next(0, validTargets.Count);
                SelectedTargets.Add(validTargets[pickedTarget]);
            }
            print("enemy target");
            for (int i = 0; i < SelectedTargets.Count; i++)
            {
                print("picked target: " + SelectedTargets[i]);
            }

            return SelectedTargets;
        }
        if (selectedMove.HasProperty(targetProperties.MULTITARGET))
        {
            if (selectedMove.HasProperty(targetProperties.PLAYERS))
            {
                List<GameObject> players = locator.getAll(true);
                for (int i = 0; i < players.Count; i++)
                {
                    validTargets.Add(locator.locateObject(players[i]));
                }

            }
            if (selectedMove.HasProperty(targetProperties.ENEMIES))
            {
                List<GameObject> enemies = locator.getAll(false);
                if (!selectedMove.HasProperty(targetProperties.SELF))
                {
                    enemies.Remove(this.gameObject);
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    validTargets.Add(locator.locateObject(enemies[i]));
                }

            }
            if (selectedMove.HasProperty(targetProperties.SELF))
            {
                validTargets.Add(locator.locateObject(this.gameObject));
            }
        }

        print(selectedMove.Name);
        print(validTargets.Count);
        return validTargets;
        
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
    protected void checkValid(move checkingMove)
    {
        if (checkingMove.HasProperty(enemyAIProperties.PREFERUNEFFECTED))
        {
            preferUneffected(checkingMove);
        }
        if (checkingMove.HasProperty(targetProperties.PLAYERS) && checkingMove.EnemyMoveAI.Count==0){
            validMoves.Add(checkingMove);
        }
        if (checkingMove.HasProperty(targetProperties.ENEMIES) && !checkingMove.HasProperty(targetProperties.SELF))
        {
            List<GameObject> enemies = locator.getAll(false);
            for(int i =0; i<enemies.Count; i++)
            {
                if(enemies[i] != this.gameObject)
                {
                    validMoves.Add(checkingMove);
                    break;
                }
            }
        }
    }

    private void preferUneffected(move checkingMove)
    {
        if (checkingMove.HasProperty(targetProperties.PLAYERS))
        {
            List<GameObject> players = locator.getAll(true);
            statusEffect searchStatus = checkingMove.MoveEffects[0].MoveStatus;
            for (int i = 0; i < checkingMove.MoveEffects.Count; i++)
            {
                if (checkingMove.MoveEffects[i].Target == statusTarget.INFLICT)
                {
                    searchStatus = checkingMove.MoveEffects[i].MoveStatus;
                    break;
                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].GetComponent<BattleUnitHealth>().health > 0 && !players[i].GetComponent<BattleUnitStatus>().HasStatus(searchStatus))
                {
                    print(players[i].name + " does not have the status yet");
                    validMoves.Add(checkingMove);
                    break;
                }
            }
        }
        //will not attack self
        if (checkingMove.HasProperty(targetProperties.ENEMIES))
        {
            List<GameObject> enemies = locator.getAll(false);
            enemies.Remove(this.gameObject);
            statusEffect searchStatus = checkingMove.MoveEffects[0].MoveStatus;
            for (int i = 0; i < checkingMove.MoveEffects.Count; i++)
            {
                if (checkingMove.MoveEffects[i].Target == statusTarget.INFLICT)
                {
                    searchStatus = checkingMove.MoveEffects[i].MoveStatus;
                    break;
                }
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].GetComponent<BattleUnitStatus>().HasStatus(searchStatus))
                {
                    validMoves.Add(checkingMove);
                    break;
                }
            }
        }
        bool selfstatus = false;
        statusEffect searchSelfStatus = checkingMove.MoveEffects[0].MoveStatus;
        for (int i = 0; i < checkingMove.MoveEffects.Count; i++)
        {
            if (checkingMove.MoveEffects[i].Target == statusTarget.SELF)
            {
                searchSelfStatus = checkingMove.MoveEffects[i].MoveStatus;
                selfstatus = true;
                break;
            }
        }
        if (checkingMove.HasProperty(targetProperties.SELF) && selfstatus)
        {
            if (!this.gameObject.GetComponent<BattleUnitStatus>().HasStatus(searchSelfStatus))
                validMoves.Add(checkingMove);
        }
    }

    protected int checkNumTargets(move selectedMove)
    {
        if (selectedMove.HasProperty(targetProperties.SINGLETARGET))
        {
            return 1;
        }
        if (selectedMove.HasProperty(targetProperties.MULTITARGET))
        {
            int count = 0;
            if (selectedMove.HasProperty(targetProperties.PLAYERS))
            {
                count += locator.getAll(true).Count;
            }
            if (selectedMove.HasProperty(targetProperties.ENEMIES))
            {
                count += locator.getAll(false).Count-1;
            }
            if (selectedMove.HasProperty(targetProperties.SELF))
            {
                count++;
            }
            return count;
        }
        return 1;
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
