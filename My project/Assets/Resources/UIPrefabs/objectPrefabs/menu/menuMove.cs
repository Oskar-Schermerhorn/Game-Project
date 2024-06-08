using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuMove : MonoBehaviour
{
    ObjectLocator locator;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        turnManagement.PlayerTurn += moveMenu;
    }
    public void moveMenu(int playerIndex)
    {
        GetComponent<Transform>().position = locator.locateObject(playerIndex).transform.position;
    }
    private void OnDisable()
    {
        turnManagement.PlayerTurn -= moveMenu;
    }
}
