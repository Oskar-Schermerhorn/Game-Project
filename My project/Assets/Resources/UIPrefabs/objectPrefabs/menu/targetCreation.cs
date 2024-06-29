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
        GameObject currentPlayer = locator.locateObject(turn.turnNum);
        move currentMove = currentPlayer.GetComponent<BattleUnitData>().getMoveset()[moveIndex];
        
        List<int> possibleIndex = new List<int>();
        if (currentMove.HasProperty(targetProperties.SINGLETARGET))
        {
            if (currentMove.HasProperty(targetProperties.FRONT))
            {
                if (currentMove.HasProperty(targetProperties.PLAYERS))
                {
                    GameObject front = locator.getFront(true);
                    possibleIndex.Add(locator.locateObject(front));
                }
                else if (currentMove.HasProperty(targetProperties.ENEMIES))
                {
                    GameObject front = locator.getFront(false);
                    possibleIndex.Add(locator.locateObject(front));
                }
            }
            else if (currentMove.HasProperty(targetProperties.FREETARGET))
            {
                List<GameObject> possibleTargets = new List<GameObject>();
                if (currentMove.HasProperty(targetProperties.PLAYERS))
                {
                    possibleTargets = locator.getAll(true);
                    //add all players not including self
                    if (!currentMove.HasProperty(targetProperties.SELF) && possibleTargets.Contains(currentPlayer))
                    {
                        possibleTargets.Remove(currentPlayer);
                    }
                }
                if (currentMove.HasProperty(targetProperties.ENEMIES))
                {
                    possibleTargets = locator.getAll(false);
                    //add all players not including self

                    if (!currentMove.HasProperty(targetProperties.SELF) && possibleTargets.Contains(currentPlayer))
                    {
                        possibleTargets.Remove(currentPlayer);
                    }
                }
                for(int i = 0; i< possibleTargets.Count; i++)
                {
                    possibleIndex.Add(locator.locateObject(possibleTargets[i]));
                }
                
            }
            else if (currentMove.HasProperty(targetProperties.SELF))
            {
                possibleIndex.Add(turn.turnNum);
            }
            createSingleTarget(0, possibleIndex[0], possibleIndex);
        }
        else if (currentMove.HasProperty(targetProperties.MULTITARGET))
        {
            List<GameObject> possibleTargets = new List<GameObject>();
            if (currentMove.HasProperty(targetProperties.PLAYERS))
            {
                possibleTargets = locator.getAll(true);
                //add all players not including self

                if (!currentMove.HasProperty(targetProperties.SELF) && possibleTargets.Contains(currentPlayer))
                {
                    possibleTargets.Remove(currentPlayer);
                }
            }
            if (currentMove.HasProperty(targetProperties.ENEMIES))
            {
                possibleTargets = locator.getAll(false);
                //add all players not including self
                if (!currentMove.HasProperty(targetProperties.SELF) && possibleTargets.Contains(currentPlayer))
                {
                    possibleTargets.Remove(currentPlayer);
                }
            }
            for (int i = 0; i < possibleTargets.Count; i++)
            {
                possibleIndex.Add(locator.locateObject(possibleTargets[i]));
            }
            createMultiTarget(possibleIndex);
        }
        /*
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
                    createTargets(0, currentMove.targetPos[0], getRange(currentMove));
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
                    *//*createTargets(0, currentMove.targetPos[0], new List<int>() { 0, 1, 2, 3 });
                    createTargets(1, currentMove.targetPos[1], new List<int>() { 0, 1, 2, 3 });
                    createTargets(2, currentMove.targetPos[2], new List<int>() { 0, 1, 2, 3 });
                    createTargets(3, currentMove.targetPos[3], new List<int>() { 0, 1, 2, 3 });*//*
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
        }*/
        
        Targeting();
    }

    void findRange(move currentMove)
    {
        for (int i = 0; i < locator.numObjects(); i++)
        {
            if (currentMove.HasProperty(targetProperties.PLAYERS) && locator.locateObject(i) && (locator.locateObject(i) != this.gameObject))
            {

            }
        }
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
        //createTargets(0, targetPositions[0], targetPositions);
        GameObject target = GameObject.Find("Targets/Target0");
        target.transform.position = locator.locateObject(targetPositions[0]).transform.position;
        Targeting();
    }
    

    void createSingleTarget(int id, int pos, List<int>possible)
    {
        print("creating targets");
        GameObject targetRef;
        targetRef = Instantiate<GameObject>(movableTargetPrefab, GameObject.Find("Targets").transform);
        targetRef.name = "Target" + id;
        targetRef.GetComponent<movableTarget>().position = pos;
        targetRef.GetComponent<movableTarget>().possiblePositions = possible;
        targetRef.transform.position= locator.locateObject(pos).transform.position;
    }
    void createMultiTarget(List<int> possible)
    {
        print("creating targets");
        GameObject targetRef;
        int id = 0;
        for(int i = 0; i<possible.Count; i++)
        {
            targetRef = Instantiate<GameObject>(movableTargetPrefab, GameObject.Find("Targets").transform);
            targetRef.GetComponent<movableTarget>().position = possible[i];
            targetRef.GetComponent<movableTarget>().possiblePositions = new List<int> { possible[i] };
            targetRef.name = "Target" + id;
            targetRef.transform.position = locator.locateObject(possible[i]).transform.position;
        }
        
    }

    private void OnDisable()
    {
        menuState.SelectMove -= placeTargets;
        menuState.SelectItem -= placeItemTargets;
    }
}
