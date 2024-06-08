using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleUnitActionCommands : MonoBehaviour
{
    ActionCommandInputs input;
    public static event Action AcceptTime;
    public static event Action AcceptParryTime;
    public static event Action ResetTime;
    public static event Action<Vector2> PromptLocation;
    public static event Action RemovePromptsEarly;
    public static event Action<int> MoveToNext;
    private void Awake()
    {
        input = GameObject.Find("BattleHandler").GetComponent<ActionCommandInputs>();
    }
    private void ActionOpen()
    {
        input.Open();
        print("Action commands have been enabled");
    }
    private void ActionClose()
    {
        input.Close();
        print("Action commands close");
    }
    void TimedInputAccept()
    {
        AcceptTime();
    }
    void AcceptParry()
    {
        AcceptParryTime();
    }
    void ResetTimedInput()
    {
        ResetTime();
    }
    void MoveToNextButton(int button)
    {
        MoveToNext(button);
    }
    void setPromptLocation(string location)
    {
        Vector2 prompt = new Vector2(float.Parse(location.Split(',')[0]), float.Parse(location.Split(',')[1]));
        PromptLocation(prompt + new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y));
    }
    void removePromptsEarly()
    {
        RemovePromptsEarly();
    }
    public void stopActionCommands()
    {
        /*
        activated = false;
        inputRecord.Clear();
        fromInputQueue = -1;
        pressed = false;
        print("Check 3");
        prompt.GetComponent<Animator>().Play("none");
        currentButton = commandButton.NONE;
        currentCharge = 0;
        for (int i = 0; i < 3; i++)
        {
            prompt.GetComponentsInChildren<Image>()[i].enabled = false;
        }
        actionCommands.Disable();
        print("Action commands have been disabled");
        */
    }

}
