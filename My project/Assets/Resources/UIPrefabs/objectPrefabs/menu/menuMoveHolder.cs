using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class menuMoveHolder : MonoBehaviour
{
    [SerializeField] GameObject currentPlayer;
    public move currentMove {get; private set;}
    public item currentItem {get; private set;}
    [SerializeField] string moveName;
    public int moveIndex;
    ObjectLocator locator;
    turnManagement turn;
    DataRecorderItems dataItem;
    move ItemMove;
    public static event Action<move> moveData;
    private void Awake()
    {
        turnManagement.NewTurn += setPlayer;
        turnManagement.SpinTurn += setPlayer;
        menuState.SelectMove += setMove;
        EnemyUnitMoveSelect.moveSelected += setMove;
        optionsController.confirmedItem += setItem;
        PhoenixRevive.reviveMove += setMove;
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        dataItem = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
    }
    public void setPlayer()
    {
        if(turn.turnNum < 8)
        {
            GameObject player = locator.locateObject(turn.turnNum);
            if (player.GetComponent<BattleUnitData>() != null)
            {
                currentPlayer = player;
            }
        }
    }
    public void setMove(int index)
    {
        if (currentPlayer != null)
        {
            if (currentPlayer.GetComponent<BattleUnitData>().getMoveset().Count > index)
            {
                currentMove = currentPlayer.GetComponent<BattleUnitData>().getMoveset()[index];
                moveName = currentMove.Name;
                moveIndex = index;
                moveData(currentMove);
            }
        }
    }
    public void setMove(move newMove)
    {
        currentMove = newMove;
        moveName = currentMove.Name;
        moveIndex = -1;
        moveData(currentMove);
    }
    public void setItem(int index)
    {
        currentItem = dataItem.getItem(dataItem.items[index]);
        int damage = 0;
        currentMove = ItemMove;
        if (currentItem.hpRestore != 0)
        {
            damage = currentItem.hpRestore * -1;
        }
        else
        {
            currentMove.MoveProperties.Add(moveProperties.NULL);
        }

        
        currentMove.cost = currentItem.bpRestore * -1;
        currentMove.Damage = damage;
        currentMove.MoveEffects.Add(new effect(currentItem.effect, statusTarget.INFLICT, effectCondition.ALWAYS));
        moveName = currentMove.Name;
        moveIndex = index;
        moveData(currentMove);
    }
    private void OnDisable()
    {
        turnManagement.NewTurn -= setPlayer;
        menuState.SelectMove -= setMove;
        EnemyUnitMoveSelect.moveSelected -= setMove;
        turnManagement.SpinTurn -= setPlayer;
        optionsController.confirmedItem -= setItem;
        PhoenixRevive.reviveMove -= setMove;
    }
}
