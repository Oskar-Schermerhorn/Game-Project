using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitData : BattleUnitData
{
    [SerializeField] private string subfolder;
    public override void data()
    {
        print("reading data");
        string path = "AttackMoves/enemies/";
        if(subfolder.Length > 0)
        {
            path += subfolder;
        }
        path += this.gameObject.name;
        print(path);
        moveProperty[] allMoves = Resources.LoadAll<moveProperty>(path);
        print("moves found: " + allMoves.Length);
        for (int i = 0; i < allMoves.Length; i++)
        {
            print(allMoves[i].name);
            moveset.Add(new move(allMoves[i]));
        }
        //Resources.LoadAll("AttackMoves/" + this.gameObject.name);
    }

}
