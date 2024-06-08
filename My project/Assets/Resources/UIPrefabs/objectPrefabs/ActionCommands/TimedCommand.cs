using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedCommand : Command
{

    public override void Perform(move selectedMove)
    {
        //ActionCommandInputs.A += correctButton;


        /*if (actionCommands.ActionCommands.A.triggered && allowAction)
        {
            allowAction = false;
            
            if (spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().getActionable())
            {
                spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = true;
                print("Action");
                CancelInvoke();
                allowAction = true;
            }
        }*/
    }
    private void correctButton()
    {
        if (true)
        {
            success = true;
        }
        else
        {
            Invoke("Timer", 0.5f);
        }
        
    }
    private void Timer()
    {

    }
    private void incorrectButton()
    {

    }
}
