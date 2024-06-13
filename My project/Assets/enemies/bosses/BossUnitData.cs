using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnitData : BattleUnitData
{
    public List<move> RegularMoves { get; private set; } = new List<move>();
    public List<move> DesperationMoveset { get; private set;} = new List<move>();
    public List<move> LimitedMoveset { get; private set; } = new List<move>();
    override public void data()
    {
        move[] forestBoss = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {5}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.UNMOVABLE, new actionCommand(actionCommandType.DEFENSE)),
            };
        move[] forestBossDesperation = new move[] {
            new move(new string[] { "Fly" }, 2, new int[] {7, 7, 7, 7}, moveTargets.ENEMY, new effect("none","none"), new int[] {0, 1, 2, 3}, targetType.UNMOVABLE, new actionCommand(actionCommandType.DEFENSE)),
            };
        move[] forestBossLimited = new move[] {
            new move(new string[] { "Heal" }, 1, new int[] {-8}, moveTargets.SELF, new effect("heavy attack+","heavy defense+"), new int[] {4}, targetType.UNMOVABLE, new actionCommand()),
            };
        move[] screechingBatMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {5}, moveTargets.ENEMY, new effect("none","none"), new int[] {0}, targetType.SINGLE, new actionCommand(actionCommandType.DEFENSE)),
            new move(new string[] { "Sonic" }, 1, new int[] {}, moveTargets.ENEMY, new effect("heavy defenseDown","none"), new int[] {0,1,2,3}, targetType.UNMOVABLE, new actionCommand(actionCommandType.DEFENSE)),};

        switch (this.gameObject.name)
        {
            case "ForestBoss(Clone)":
                RegularMoves.AddRange(forestBoss);
                DesperationMoveset.AddRange(forestBossDesperation);
                LimitedMoveset.AddRange(forestBossLimited);
                moveset.AddRange(RegularMoves);
                moveset.AddRange(DesperationMoveset);
                moveset.AddRange(LimitedMoveset);
                break;
            case "ScreechingBatVarient(Clone)":
                RegularMoves.AddRange(screechingBatMoveset);
                moveset.AddRange(RegularMoves);
                break;
        }
        //moveNames.Clear();
        for (int i = 0; i < RegularMoves.Count; i++)
        {
            //moveNames.Add(RegularMoves[i].animationNames[0]);
        }
        for (int i = 0; i < DesperationMoveset.Count; i++)
        {
            //moveNames.Add(DesperationMoveset[i].animationNames[0]);
        }
        for (int i = 0; i < LimitedMoveset.Count; i++)
        {
            //moveNames.Add(LimitedMoveset[i].animationNames[0]);
        }
    }
}
