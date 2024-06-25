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
                        validTargets.Add(locator.locateObject(players[i]));
                    }
                    print("here");
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
