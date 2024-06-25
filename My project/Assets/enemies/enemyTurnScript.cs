using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum targeting { RANDOM, LOWHP }
public enum changeCondition { NONE, HALFHEALTH, PRESENCE, UNIQUE }
public class enemyTurnScript : EnemyUnit
{
    
    override public void data()
    {
        /*move[] mothMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Status" }, 0, new int[] {}, moveTargets.ENEMY, new effect(poison,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Absorb" }, 0, new int[] {99}, moveTargets.ALLY, new effect(none,fullHeal), new int[] {}, targetType.SINGLE, new actionCommand())};
        move[] mothSorcererMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {4}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Special" }, 0, new int[] {3}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Absorb" }, 0, new int[] {99}, moveTargets.ALLY, new effect(none,fullHeal), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Status" }, 0, new int[] {}, moveTargets.ENEMY, new effect(poison,none), new int[] {}, targetType.SINGLE, new actionCommand())};
        switch (this.gameObject.tag)
        {
            case ("moth"):
                moveset.AddRange(mothMoveset);
                break;
            case ("mothSorcerer"):
                break;
        }*/

    }
}
