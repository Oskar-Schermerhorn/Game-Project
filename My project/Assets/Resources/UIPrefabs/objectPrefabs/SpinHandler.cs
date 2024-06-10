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

        GameObject temp0 = spawn.unitList[0];
        GameObject temp1 = spawn.unitList[1];
        GameObject temp2 = spawn.unitList[2];

        spawn.unitList[0] = temp2;
        spawn.unitList[1] = temp0;
        spawn.unitList[2] = temp1;
        createList(true);
        //              1          0
        //2->1             0  ->     2
        //1->0          2          1
        //0->2
        spawn.updateLayer();

    }
    public void reverseSpin(GameObject target)
    {
        GameObject temp0 = spawn.unitList[0];
        GameObject temp1 = spawn.unitList[1];
        GameObject temp2 = spawn.unitList[2];

        spawn.unitList[0] = temp1;
        spawn.unitList[1] = temp2;
        spawn.unitList[2] = temp0;

        createList(false);
        //              1              2
        //2->0             0  ->          1
        //1->2          2              0
        //0->1
        spawn.updateLayer();

    }
    public void createList(bool left)
    {
        //spawn.updatePositions();

        List<GameObject> objects = new List<GameObject>();
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < 3; i++)
        {
            GameObject player = locator.locateObject(i);
            
            if (player.GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
            {
                objects.Add(player);
                positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + i).transform.position);
            }
        }
        movement.Move(objects, positions, left);
    }
    public bool checkSpin()
    {
        if (locator.locateObject(0).GetComponent<BattleUnitID>().UnitSide != side.PLAYER || locator.locateObject(0).GetComponent<BattleUnitHealth>().health <= 0)
        {
            return true;
        }
        
        return false;
    }
}
