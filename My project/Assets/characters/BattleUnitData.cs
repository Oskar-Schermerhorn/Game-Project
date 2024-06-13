using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleUnitData : MonoBehaviour
{
    [SerializeField] protected List<move> moveset = new List<move>();
    public static event Action<GameObject> movesetUpdated;

    public List<move> getMoveset()
    {
        return moveset;
    }

    private void Start()
    {
        data();
    }
    public void updateData()
    {
        moveset.Clear();
        data();
    }
    public virtual void data()
    {
        print("reading data");
        string path = "AttackMoves/" + this.gameObject.name;
        print(path);
        moveProperty[] allMoves = Resources.LoadAll<moveProperty>(path);
        print("moves found: " + allMoves.Length);
        for(int i = 0; i<allMoves.Length; i++)
        {
            print(allMoves[i].name);
            moveset.Add(new move(allMoves[i]));
        }
        //Resources.LoadAll("AttackMoves/" + this.gameObject.name);
    }
}
