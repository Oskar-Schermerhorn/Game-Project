using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitData : BattleUnitData
{
    override public void data()
    {
        move[] mothMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {15}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            //new move(new string[] { "Special" }, 0, new int[] {3}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            //new move(new string[] { "Status" }, 0, new int[] {}, moveTargets.ENEMY, new effect("poison","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Absorb" }, 0, new int[] {99}, moveTargets.ALLY, new effect("none","fullHeal"), new int[] {4}, targetType.SINGLE, new actionCommand())
            };
        move[] mothSorcererMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {4}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Special" }, 0, new int[] {3}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Absorb" }, 0, new int[] {99}, moveTargets.ALLY, new effect("none","fullHeal"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Status" }, 0, new int[] {}, moveTargets.ENEMY, new effect("poison","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE))};
        move[] babyBatMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {2}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),};
        move[] pigBatMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {4}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Bite" }, 0, new int[] {1,1,1}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),};
        move[] fruitBatMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {1}, moveTargets.ENEMY, new effect("poison","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Bite" }, 0, new int[] {1}, moveTargets.ENEMY, new effect("heavy poison","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),};
        move[] upsideDownBatMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {2,2,2}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Glare" }, 0, new int[] {1}, moveTargets.ENEMY, new effect("heavy defenseDown","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),};
        move[] screechingBatMoveset = new move[] {
            new move(new string[] { "Attack" }, 0, new int[] {5}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Sonic" }, 0, new int[] {}, moveTargets.ENEMY, new effect("defenseDown","none"), new int[] {0,1,2,3}, targetType.UNMOVABLE, new actionCommand(actionCommandType.DEFENSE)),};
        switch (this.gameObject.tag)
        {
            case ("moth"):
                moveset.AddRange(mothMoveset);
                break;
            case ("mothSorcerer"):
                break;
        }
        switch (this.gameObject.name)
        {
            case "BabyBat(Clone)":
                moveset.AddRange(babyBatMoveset);
                break;
            case "PigBat(Clone)":
                moveset.AddRange(pigBatMoveset);
                break;
            case "FruitBat(Clone)":
                moveset.AddRange(fruitBatMoveset);
                break;
            case "UpsideDownBat(Clone)":
                moveset.AddRange(upsideDownBatMoveset);
                break;
            case "ScreechingBat(Clone)":
                moveset.AddRange(screechingBatMoveset);
                break;
        }
        moveNames.Clear();
        for (int i = 0; i < moveset.Count; i++)
        {
            moveNames.Add(moveset[i].animationNames[0]);
        }
    }

}
