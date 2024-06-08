using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatUnitTargeting : EnemyUnitMoveSelect
{
    protected override List<int> RandomTargets(move selectedMove, int position)
    {
        if(checkNumTargets(selectedMove) > 1)
        {
            return base.RandomTargets(selectedMove, position);
        }
        List<int> validTargets = new List<int>();
        if ((selectedMove.moveTargetType == moveTargets.ENEMY || selectedMove.moveTargetType == moveTargets.BOTH) && selectedMove.targetType == targetType.SINGLE)
        {
            validTargets.AddRange(base.getRange(position));
        }
        else if (selectedMove.moveTargetType == moveTargets.ENEMY || selectedMove.moveTargetType == moveTargets.BOTH)
        {
            validTargets.AddRange(selectedMove.targetPos);
        }
        if (selectedMove.moveTargetType == moveTargets.ALLY || selectedMove.moveTargetType == moveTargets.BOTH)
        {
            for (int i = 4; i < 8; i++)
            {
                if (i != position && locator.locateObject(i).GetComponent<BattleUnitHealth>() != null && locator.locateObject(i).GetComponent<BattleUnitHealth>().health > 0)
                {
                    validTargets.Add(i);
                }
            }
        }
        System.Random random = new System.Random();
        int pickedTarget = -1;
        int lowestHP = 999;
        List<int> SelectedTargets = new List<int>();
        print("finding lowest current health target");
        for (int i = 0; i < validTargets.Count; i++)
        {
            if(locator.locateObject(validTargets[i]).GetComponent<BattleUnitHealth>() != null && 
                locator.locateObject(validTargets[i]).GetComponent<BattleUnitHealth>().health > 0 &&
                locator.locateObject(validTargets[i]).GetComponent<BattleUnitHealth>().health <= lowestHP)
            {
                lowestHP = locator.locateObject(validTargets[i]).GetComponent<BattleUnitHealth>().health;
                pickedTarget = i;
            }
            
        }
        print(pickedTarget + " with hp of " + lowestHP);
        SelectedTargets.Add(validTargets[pickedTarget]);
        print("enemy target");
        for (int i = 0; i < SelectedTargets.Count; i++)
        {
            print("picked target: " + SelectedTargets[0]);
        }

        return SelectedTargets;
    }
}
