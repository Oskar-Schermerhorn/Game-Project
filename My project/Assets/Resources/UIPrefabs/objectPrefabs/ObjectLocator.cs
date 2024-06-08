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
        for (int i = 0; i < 8; i++)
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
}
