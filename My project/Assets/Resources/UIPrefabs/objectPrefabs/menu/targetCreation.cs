using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class targetCreation : MonoBehaviour
{
    [SerializeField] GameObject movableTargetPrefab;
    [SerializeField] GameObject unmovableTargetPrefab;
    [SerializeField] ObjectLocator locator;
    [SerializeField] turnManagement turn;
    [SerializeField] DataRecorderItems dataItem;
    public static event Action Targeting;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        dataItem = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
        menuState.SelectMove += placeTargets;
        menuState.SelectItem += placeItemTargets;
    }
    public void placeTargets(int moveIndex)
    {
        move currentMove = locator.locateObject(turn.turnNum).GetComponent<BattleUnitData>().moveset[moveIndex];
        if(currentMove.moveTargetType == moveTargets.SELF)
        {
            
            switch (currentMove.targetType)
            {
                case targetType.UNMOVABLE:
                    print("creating self target");
                    createTargets(0, turn.turnNum);
                    break;
                case targetType.PAIRS:
                    createTargets(0, turn.turnNum);
                    List<int> teamSlots = new List<int>() { 0, 1, 2, 3 };
                    teamSlots.Remove(turn.turnNum);
                    createTargets(1, teamSlots[0], teamSlots);
                    break;
            }
        }
        else if(currentMove.moveTargetType == moveTargets.ENEMY || currentMove.moveTargetType == moveTargets.BOTH)
        {
            switch (currentMove.targetType)
            {
                case targetType.SINGLE:
                    createTargets(0, currentMove.targetPos[0], getRange());
                    break;
                case targetType.UNMOVABLE:
                    for (int i = 0; i < currentMove.targetPos.Length; i++)
                    {
                        if(locator.locateObject(currentMove.targetPos[i]).GetComponent<BattleUnitHealth>() != null && locator.locateObject(currentMove.targetPos[i]).GetComponent<BattleUnitHealth>().health > 0)
                            createTargets(i, currentMove.targetPos[i]);
                    }
                    break;
                case targetType.PAIRS:
                    createTargets(0, currentMove.targetPos[0]);
                    List<int> secondaryPos = new List<int>();
                    secondaryPos.AddRange(currentMove.targetPos);
                    secondaryPos.Remove(4);
                    for(int i =0; i<secondaryPos.Count; i++)
                    {
                        if(locator.locateObject(secondaryPos[i]).GetComponent<BattleUnitHealth>() == null || locator.locateObject(secondaryPos[i]).GetComponent<BattleUnitHealth>().health <= 0)
                        {
                            print("removing " + secondaryPos[i]);
                            secondaryPos.Remove(secondaryPos[i]);
                            i--;
                        }
                    }
                    if(secondaryPos.Count>0)
                        createTargets(1, currentMove.targetPos[1], secondaryPos);
                    break;
                case targetType.THREES:
                    createTargets(0, currentMove.targetPos[0], new List<int>() { 4, 5, 6, 7 });
                    createTargets(1, currentMove.targetPos[1], new List<int>() { 4, 5, 6, 7 });
                    createTargets(2, currentMove.targetPos[2], new List<int>() { 4, 5, 6, 7 });
                    break;
                case targetType.FOURS:
                    createTargets(0, currentMove.targetPos[0], new List<int>() { 4, 5, 6, 7 });
                    createTargets(1, currentMove.targetPos[1], new List<int>() { 4, 5, 6, 7 });
                    createTargets(2, currentMove.targetPos[2], new List<int>() { 4, 5, 6, 7 });
                    createTargets(3, currentMove.targetPos[3], new List<int>() { 4, 5, 6, 7 });
                    break;
            }
            for (int i = 0; i < currentMove.targetPos.Length; i++)
            {
                GameObject target = GameObject.Find("Targets/Target" + i);
                if (locator.locateObject(currentMove.targetPos[i]).GetComponent<BattleUnitHealth>() != null && locator.locateObject(currentMove.targetPos[i]).GetComponent<BattleUnitHealth>().health >0)
                {
                    if(target != null)
                        target.transform.position = locator.locateObject(currentMove.targetPos[i]).transform.position;

                }

            }
        }
        else if(currentMove.moveTargetType == moveTargets.ALLY)
        {
            switch (currentMove.targetType)
            {
                case targetType.SINGLE:
                    createTargets(0, currentMove.targetPos[0], getAllies());
                    break;
                case targetType.UNMOVABLE:
                    for (int i = 0; i < currentMove.targetPos.Length; i++)
                    {
                        createTargets(i, currentMove.targetPos[i]);
                    }
                    break;
                case targetType.FOURS:
                    createTargets(0, currentMove.targetPos[0], getAllies());
                    createTargets(1, currentMove.targetPos[1], getAllies());
                    createTargets(2, currentMove.targetPos[2], getAllies());
                    createTargets(3, currentMove.targetPos[3], getAllies());
                    break;
            }
            for (int i = 0; i < currentMove.targetPos.Length; i++)
            {
                GameObject target = GameObject.Find("Targets/Target" + i);
                if (locator.locateObject(currentMove.targetPos[i]) != null)
                {
                    target.transform.position = locator.locateObject(currentMove.targetPos[i]).transform.position;

                }

            }
        }
        else if(currentMove.moveTargetType == moveTargets.ALLYINCLUDINGDEAD)
        {
            switch (currentMove.targetType)
            {
                case targetType.SINGLE:
                    createTargets(0, currentMove.targetPos[0], new List<int>() { 0,1,2,3});
                    break;
                
                case targetType.UNMOVABLE:
                    for (int i = 0; i < currentMove.targetPos.Length; i++)
                    {
                        createTargets(i, currentMove.targetPos[i]);
                    }
                    break;
                case targetType.FOURS:
                    /*createTargets(0, currentMove.targetPos[0], new List<int>() { 0, 1, 2, 3 });
                    createTargets(1, currentMove.targetPos[1], new List<int>() { 0, 1, 2, 3 });
                    createTargets(2, currentMove.targetPos[2], new List<int>() { 0, 1, 2, 3 });
                    createTargets(3, currentMove.targetPos[3], new List<int>() { 0, 1, 2, 3 });*/
                    break;
            }
            for (int i = 0; i < currentMove.targetPos.Length; i++)
            {
                GameObject target = GameObject.Find("Targets/Target" + i);
                if (locator.locateObject(currentMove.targetPos[i]) != null)
                {
                    target.transform.position = locator.locateObject(currentMove.targetPos[i]).transform.position;

                }

            }
        }
        
        Targeting();
    }
    public void placeItemTargets(int itemIndex)
    {
        item currentItem = dataItem.getItem(dataItem.items[itemIndex]);
        List<int> targetPositions = new List<int>();
        if (currentItem.useOnPlayer)
        {
            for(int i=0; i<4; i++)
            {
                if(locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                {
                    targetPositions.Add(i);
                }
            }
        }
        if (currentItem.useOnEnemy)
        {
            for (int i = 4; i < 8; i++)
            {
                if (locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                {
                    targetPositions.Add(i);
                }
            }
        }
        createTargets(0, targetPositions[0], targetPositions);
        GameObject target = GameObject.Find("Targets/Target0");
        target.transform.position = locator.locateObject(targetPositions[0]).transform.position;
        Targeting();
    }
    private List<int> getRange()
    {
        List<int> range = new List<int> ();
        range.Add(4);
        switch (turn.turnNum)
        {
            case 0:
                if (locator.locateObject(5).GetComponent<BattleUnitHealth>() != null && locator.locateObject(5).GetComponent<BattleUnitHealth>().health >0)
                    range.Add(5);
                if (locator.locateObject(6).GetComponent<BattleUnitHealth>() != null && locator.locateObject(6).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(6);
                break;
            case 1:
                if (locator.locateObject(5).GetComponent<BattleUnitHealth>() != null && locator.locateObject(5).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(5);
                break;
            case 2:
                if (locator.locateObject(6).GetComponent<BattleUnitHealth>() != null && locator.locateObject(6).GetComponent<BattleUnitHealth>().health > 0)
                    range.Add(6);
                break;

                
        }
        return range;
    }
    private List<int> getAllies()
    {
        List<int> range = new List<int>();
        for(int i = 0; i< 4; i++)
        {
            if (locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                range.Add(i);
        }
        return range;
    }

    void createTargets(int id, int pos, List<int>possible)
    {
        print("creating targets");
        GameObject targetRef;
        targetRef = Instantiate<GameObject>(movableTargetPrefab, GameObject.Find("Targets").transform);
        targetRef.name = "Target" + id;
        targetRef.GetComponent<movableTarget>().position = pos;
        targetRef.GetComponent<movableTarget>().possiblePositions = possible;
        targetRef.transform.position= locator.locateObject(pos).transform.position;
    }
    void createTargets(int id, int pos)
    {
        print("creating targets");
        GameObject targetRef;
        targetRef = Instantiate<GameObject>(unmovableTargetPrefab, GameObject.Find("Targets").transform);
        targetRef.name = "Target" + id;
        targetRef.GetComponent<Target>().position = pos;
        print(targetRef.GetComponent<Target>().position);
        targetRef.transform.position = locator.locateObject(targetRef.GetComponent<Target>().position).transform.position;
    }
    private void OnDisable()
    {
        menuState.SelectMove -= placeTargets;
        menuState.SelectItem -= placeItemTargets;
    }
}
