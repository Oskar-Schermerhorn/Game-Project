using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDescription : MonoBehaviour
{
    [SerializeField] GameObject Description;
    [SerializeField] GameObject descriptionInstance;
    private void Awake()
    {
        menuMoveHolder.moveData += createNewDescription;
        targetInput.Confirm += destroyDescription;
        targetInput.Cancel += destroyDescription;
    }
    void createNewDescription(move currentMove)
    {
        descriptionInstance = Instantiate<GameObject>(Description, GameObject.Find("Canvas").transform);
        descriptionInstance.GetComponentInChildren<Text>().text = getText(currentMove);
    }
    void destroyDescription()
    {
        Destroy(descriptionInstance);
    }
    string getText(move currentMove)
    {
        switch (currentMove.action.type)
        {
            case actionCommandType.TIMED:
                return "Press the button at the correct time";
            case actionCommandType.HOLD:
                return "Hold down the button, then release once the timer is full";
            case actionCommandType.FIGHT:
                return "Press the correct buttons in quick succession";
            case actionCommandType.AIM:
                return "Press any button to aim at a target";
            case actionCommandType.BALANCE:
                return "Press up and down to keep the indicator in the middle";
            case actionCommandType.MASH:
                return "Press the buttons rapidly and in the correct order";
            case actionCommandType.COMBO:
                return "Press the correct buttons as they apear";
            default:
                destroyDescription();
                return "";
        }
    }
    private void OnDisable()
    {
        menuMoveHolder.moveData -= createNewDescription;
        targetInput.Confirm -= destroyDescription;
        targetInput.Cancel -= destroyDescription;
    }
}
