using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnSwap : MonoBehaviour
{
    turnManagement turn;
    private void Awake()
    {
        turn = this.gameObject.GetComponent<turnManagement>();
    }
    // handle switching
    private void turnManipulate()
    {
        /*
        print("handle switch");
        if (turnNum == 1 && !spawn.unitList[1].getAlive() && turnState == battleState.PLAYERTURN)
        {
            turnManipulated = true;
            turnNum++;
        }
        else if (turnNum == 2 && !spawn.unitList[2].getAlive() && spawn.unitList[3].getAlive() && turnState == battleState.PLAYERTURN)
        {
            turnManipulated = true;
            turnNum++;
        }
        else if (turnNum == 2 && !spawn.unitList[2].getAlive() && turnState == battleState.PLAYERTURN)
        {
            turnManipulated = true;
            turnNum--;
        }
        else if (turnNum == 3 && !spawn.unitList[3].getAlive() && turnState == battleState.PLAYERTURN)
        {
            turnManipulated = true;
            turnNum--;
        }*/
    }
    private void undoTurnManip()
    {/*
        if (turnManipulated)
        {
            if (turnNum == 2 && spawn.unitList[1].getAlive() && turnState == battleState.PLAYERTURN)
            {
                turnManipulated = false;
                turnNum--;
            }
            else if (turnNum == 2 && !spawn.unitList[1].getAlive() && turnState == battleState.PLAYERTURN)
            {
                turnManipulated = false;
                turnNum++;
            }
            else if (turnNum == 1 && spawn.unitList[2].getAlive() && turnState == battleState.PLAYERTURN)
            {
                turnManipulated = true;
                turnNum++;
            }
            else if (turnNum == 3 && turnState == battleState.PLAYERTURN)
            {
                turnManipulated = true;
                turnNum--;
            }
        }
    */}
}
