using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLocator : MonoBehaviour
{
    SpawnScript spawn;
    private void Awake()
    {
        spawn = GameObject.FindWithTag("BattleHandler").GetComponent<SpawnScript>();
    }

    public int locateObject(GameObject me)
    {
        for (int i = 0; i < spawn.unitList.Count; i++)
        {
            if (me.Equals(spawn.unitList[i]))
            {
                return i;
            }
        }
        return -1;
    }
    public GameObject locateObject(int pos)
    {
        return spawn.unitList[pos];
    }

    public int numObjects()
    {
        return spawn.unitList.Count;
    }

    public GameObject getFront(bool Player)
    {
        for(int i = 0; i<spawn.unitList.Count; i++)
        {
            if((Player && spawn.unitList[i].GetComponent<BattleUnitID>().UnitSide == side.PLAYER) ||
                (!Player && spawn.unitList[i].GetComponent<BattleUnitID>().UnitSide == side.ENEMY))
            {
                return spawn.unitList[i];
            }
        }
        return null;
    }

    public List<GameObject> getAll(bool Players)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < spawn.unitList.Count; i++)
        {
            if ((Players && spawn.unitList[i].GetComponent<BattleUnitID>().UnitSide == side.PLAYER) ||
                (!Players && spawn.unitList[i].GetComponent<BattleUnitID>().UnitSide == side.ENEMY))
            {
                list.Add(spawn.unitList[i]);
            }
        }
        return list;
    }
}
