using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinHandler : MonoBehaviour
{
    SpawnScript spawn;
    ObjectLocator locator;
    MoveCoroutine movement;
    turnManagement turn;
    private void Awake()
    {
        spawn = gameObject.GetComponent<SpawnScript>();
        locator = gameObject.GetComponent<ObjectLocator>();
        movement = gameObject.GetComponent<MoveCoroutine>();
        turn = gameObject.GetComponent<turnManagement>();
    }
    public void spin(GameObject target)
    {

        int lowest = 0;
        if (locator.locateObject(target) >= 4)
        {
            lowest = 4;
        }
        GameObject temp0 = spawn.unitList[lowest];
        GameObject temp1 = spawn.unitList[lowest + 1];
        GameObject temp2 = spawn.unitList[lowest + 2];
        GameObject temp3 = spawn.unitList[lowest + 3];

        spawn.unitList[lowest] = temp2;
        spawn.unitList[lowest + 1] = temp0;
        spawn.unitList[lowest + 2] = temp3;
        spawn.unitList[lowest + 3] = temp1;
        createList(lowest, true);
        //3->1          1               0
        //2->3      3       0  ->   1       2
        //1->0          2               3
        //0->2
        spawn.updateLayer();

    }
    public void reverseSpin(GameObject target)
    {
        int lowest = 0;
        if (locator.locateObject(target) >= 4)
        {
            lowest = 4;
        }
        GameObject temp0 = spawn.unitList[lowest];
        GameObject temp1 = spawn.unitList[lowest + 1];
        GameObject temp2 = spawn.unitList[lowest + 2];
        GameObject temp3 = spawn.unitList[lowest + 3];

        spawn.unitList[lowest] = temp1;
        spawn.unitList[lowest + 1] = temp3;
        spawn.unitList[lowest + 2] = temp0;
        spawn.unitList[lowest + 3] = temp2;
        createList(lowest, false);
        //3->1          1               3
        //2->3      3       0  ->   2       1
        //1->0          2               0
        //0->2
        spawn.updateLayer();

    }
    public void createList(int lowest, bool left)
    {
        //spawn.updatePositions();

        List<GameObject> objects = new List<GameObject>();
        List<Vector2> positions = new List<Vector2>();
        for (int i = lowest; i < lowest + 4; i++)
        {
            objects.Add(locator.locateObject(i));
            if (i < 4)
            {
                //print(positions[0]);
                positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + i).transform.position);
            }
            else
            {
                positions.Add(GameObject.Find("BattleHandler/Positions/EnemyPositions/position" + i).transform.position);
            }

        }
        movement.Move(objects, positions, left);
    }
    public bool checkSpin()
    {
        if (locator.locateObject(0).GetComponent<BattleUnitHealth>() == null || locator.locateObject(0).GetComponent<BattleUnitHealth>().health <= 0)
        {
            return true;
        }
        
        return false;
    }
}
