using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleCoroutine : MonoBehaviour
{
    MoveCoroutine movement;
    public void StartBattle(GameObject[] objects, Vector2[] destinations)
    {
        movement = GameObject.Find("BattleHandler").GetComponent<MoveCoroutine>();
        StartCoroutine(Setup(objects, destinations));
    }

    IEnumerator Setup(GameObject[] objects, Vector2[] destinations)
    {
        print("Setup Coroutine started");
        bool movementComplete = false;
        MoveCoroutine.spinComplete += ready;
        void ready(bool _)
        {
            movementComplete = true;
        }
        movement.Move(objects, destinations);
        yield return new WaitUntil(() => movementComplete);
        MoveCoroutine.spinComplete -= ready;
        print("waited for movement");
        GameObject.Find("BattleHandler").GetComponent<SpawnScript>().StartBattle();
    }
}
