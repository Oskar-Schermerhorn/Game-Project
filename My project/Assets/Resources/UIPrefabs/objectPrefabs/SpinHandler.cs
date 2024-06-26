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
    public void spin()
    {
        List<GameObject> players = locator.getAll(true);

        int playerIndex = players.Count - 1;

        //take the element at the end of the list
        GameObject temp = players[playerIndex];
        players.RemoveAt(playerIndex);

        //put it at the front of the list
        players.Insert(0, temp);

        //update unitlist order
        spawn.unitList.RemoveRange(0, players.Count);
        spawn.unitList.InsertRange(0, players);

        createList(true);
        spawn.updateLayer();

    }
    public void reverseSpin(GameObject target)
    {
        List<GameObject> players = locator.getAll(true);

        int playerIndex = 0;

        //take the element at the end of the list
        GameObject temp = players[playerIndex];
        players.RemoveAt(playerIndex);

        //put it at the front of the list
        players.Insert(players.Count, temp);

        //update unitlist order
        spawn.unitList.RemoveRange(0, players.Count);
        spawn.unitList.InsertRange(0, players);

        createList(false);
        spawn.updateLayer();

    }
    public void createList(bool left)
    {
        //spawn.updatePositions();

        List<GameObject> objects = new List<GameObject>();
        List<Vector2> positions = new List<Vector2>();
        for (int i = 0; i < locator.getAll(true).Count; i++)
        {
            GameObject player = locator.locateObject(i);

            objects.Add(player);
            positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + i).transform.position);
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
