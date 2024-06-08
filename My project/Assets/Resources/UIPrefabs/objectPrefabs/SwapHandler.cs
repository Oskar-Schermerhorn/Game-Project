using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapHandler : MonoBehaviour
{
    SpawnScript spawn;
    ObjectLocator locator;
    MoveCoroutine movement;
    private void Awake()
    {
        BattleUnitInflict.SwapPositions += SwapPos;
        spawn = gameObject.GetComponent<SpawnScript>();
        locator = gameObject.GetComponent<ObjectLocator>();
        movement = gameObject.GetComponent<MoveCoroutine>();
    }
    void SwapPos(int position1, int position2)
    {
        print(position1 + " " + position2);
        GameObject temp1 = spawn.unitList[position1];
        GameObject temp2 = spawn.unitList[position2];
        spawn.unitList[position1] = temp2;
        spawn.unitList[position2] = temp1;
        createList(position1/4, true);
        spawn.updateLayer();
    }
    public void createList(int lowest, bool left)
    {
        //spawn.updatePositions();
        print("lowest" + lowest);
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

    private void OnDisable()
    {
        BattleUnitInflict.SwapPositions -= SwapPos;
    }
}
