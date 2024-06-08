using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionCommandHandler : MonoBehaviour
{
    move currentMove;
    bool allowAction = true;
    bool timedAction = false;
    bool parryTime = false;
    int nextButton = 0;
    public static event Action<bool> Successful;
    public static event Action<int> Additional;
    public static event Action<bool> Parried;
    public static event Action ResumeAnimation;
    public static event Action<commandButton> buttonPressed;
    public static event Action unpressButtons;
    public static event Action<int> incrementTimer;
    public static event Action<int> updateBar;
    public Queue<int> buttonOrder = new Queue<int>();
    private void Awake()
    {
        ActionCommandInputs.Button += trackNewInputs;
        menuMoveHolder.moveData += SetMove;
        BattleUnitActionCommands.AcceptTime += AcceptTimedInput;
        BattleUnitActionCommands.AcceptParryTime += AcceptParry;
        BattleUnitActionCommands.ResetTime += ResetTimedInput;
        BattleUnitActionCommands.MoveToNext += MoveToNextButton;
    }
    public void SetMove(move selectedMove)
    {
        allowAction = true;
        timedAction = false;
        parryTime = false;
        currentMove = selectedMove;
        Successful(false);
        nextButton = 0;
        buttonOrder.Clear();
        if (selectedMove.action.type == actionCommandType.NONE || selectedMove.action.type == actionCommandType.DEFENSE)
            Successful(true);
    }
    private void trackNewInputs(commandButton currentInputButton)
    {
        
        int currentInput = buttonToInt(currentInputButton);
        //print(currentInput);
        NewInput(currentInput);
        buttonPressed(currentInputButton);
    }
    private void NewInput(int input)
    {
        switch (currentMove.action.type)
        {
            case actionCommandType.DEFENSE:
                Defense(input);
                break;
            case actionCommandType.TIMED:
                Timed(input);
                break;
            case actionCommandType.HOLD:
                Hold(input);
                break;
            case actionCommandType.MASH:
                Mash(input);
                break;
            case actionCommandType.FIGHT:
                Fight(input);
                break;
            case actionCommandType.AIM:
                break;
            case actionCommandType.BALANCE:
                break;
            case actionCommandType.COMBO:
                break;
        }
    }
    private void Timed(int input)
    {
        print("timed");
        print(input + " expecting: " + buttonToInt(currentMove.action.buttons[nextButton]));
        if (input == buttonToInt(currentMove.action.buttons[nextButton]))
        {
            if (allowAction)
            {
                if (timedAction)
                {
                    CancelInvoke();
                    timedAction = false;
                    Successful(true);
                    nextButton++;
                }
                else
                {
                    allowAction = false;
                    timedAction = false;
                    Invoke("blockTimer", 0.5f);
                }
            }
        }
    }
    private void MoveToNextButton(int button)
    {
        if(buttonToInt(currentMove.action.buttons[nextButton]) != buttonToInt(currentMove.action.buttons[button]))
        {
            print(nextButton);
            nextButton = button;
            print(nextButton);
        }
    }
    private void AcceptTimedInput()
    {
        timedAction = true;
    }
    private void AcceptParry()
    {
        parryTime = true;
    }
    private void ResetTimedInput()
    {
        allowAction = true;
        timedAction = false;
        parryTime = false;
        Successful(true);
        Parried(false);
    }
    private void blockTimer()
    {
        print("allow");
        allowAction = true;
    }
    private void Hold(int input)
    {
        print(input);
        if (input == buttonToInt(currentMove.action.buttons[0]))
        {
            timedAction = false;
            StartCoroutine(HoldButtonCoroutine(input));
        }
        else if(input == 5 && timedAction)
        {
            StopCoroutine(HoldButtonCoroutine(input));
            print("success");
            Successful(true);
            timedAction = false;
            ResumeAnimation();
        }
        else
        {
            StopCoroutine(HoldButtonCoroutine(input));
            timedAction = false;
            ResumeAnimation();
        }
    }
    IEnumerator HoldButtonCoroutine(int input)
    {
        print("coroutine started");
        incrementTimer(1);
        yield return new WaitForSeconds(currentMove.action.time/4);
        incrementTimer(2);
        yield return new WaitForSeconds(currentMove.action.time/4);
        incrementTimer(3);
        yield return new WaitForSeconds(currentMove.action.time/4);
        incrementTimer(4);
        timedAction = true;
        yield return new WaitForSeconds(currentMove.action.time / 4);
        ResumeAnimation();
    }

    private void Mash(int input)
    {
        print(buttonOrder.Count);
        if(buttonOrder.Count == 0 && input == buttonToInt(currentMove.action.buttons[0]))
        {
            StartCoroutine(ChargeCoroutine(currentMove.action.time, currentMove.action.minimum, currentMove.action.additional, currentMove.action.maximum));
        }
        if(input == buttonToInt(currentMove.action.buttons[nextButton]))
        {
            buttonOrder.Enqueue(input);
            nextButton++;
            if (nextButton >= currentMove.action.buttons.Length)
                nextButton = 0;
            updateBar(buttonOrder.Count);
        }
        else
        {
            unpressButtons();
        }
    }
    IEnumerator ChargeCoroutine(float time, int minimum, int additional, int max)
    {
        print("coroutine started");
        yield return new WaitForSeconds(time);
        print(buttonOrder.Count);
        if(buttonOrder.Count >= minimum) 
        {
            Successful(true);
            int add = (buttonOrder.Count - minimum) / additional;
            if (add > max)
                add = max;
            Additional(add);
        }
        else
        {
            Successful(false);
        }
        buttonOrder.Clear();
        ResumeAnimation();
    }
    private void Fight(int input)
    {
        if(input != 5 && nextButton < currentMove.action.buttons.Length)
        {
            print("Fight input start");
            print(input+ " expecting: " + buttonToInt(currentMove.action.buttons[nextButton]));
            if (input == buttonToInt(currentMove.action.buttons[nextButton]))
            {
                buttonOrder.Enqueue(input);
                StartCoroutine(FightCountDown(currentMove.action.time));
                nextButton++;
                if (nextButton == currentMove.action.buttons.Length)
                {
                    Successful(true);
                    ResumeAnimation();
                }
            }
            else
            {
                print("wrong input:" + input);
                StopCoroutine(FightCountDown(currentMove.action.time));
                Successful(false);
                ResumeAnimation();
                buttonOrder.Clear();
                nextButton = 0;
            }
        }
        
    }
    IEnumerator FightCountDown(float time)
    {
        yield return new WaitForSeconds(time);
        if(buttonOrder.Count>0)
            buttonOrder.Dequeue();
        print("Failed");
        Successful(false);
        ResumeAnimation();
        buttonOrder.Clear();
        nextButton = 0;
    }
    /*private void Fight()
        {
            if (!spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().getActionable())
            {
                timer = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.time;
                if (!pressed && openPrompts.Count == 0)
                {
                    for (int i = 0; i < spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons.Length; i++)
                    {
                        GameObject newPrompt = Instantiate<GameObject>(additionalPrompt, prompt.transform);
                        newPrompt.transform.position = new Vector3(newPrompt.transform.position.x + 0.7f * i, newPrompt.transform.position.y, newPrompt.transform.position.z);
                        //print(spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].button[i].ToString());
                        newPrompt.GetComponent<Animator>().Play(spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons[i].ToString());
                        openPrompts.Add(newPrompt);
                    }
                }
            }
            else
            {
                trackNewInputs();
                if (inputRecord.Count != 0)
                {
                    fromInputQueue = inputRecord.Dequeue();
                    //pressed = true;
                    timer = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.time;
                }
                if (pressed)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    print("time up");
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = false;
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().resumeAnimation();
                    inputIndex = 0;
                    while (openPrompts.Count > 0)
                    {
                        Destroy(openPrompts[0]);
                        openPrompts.RemoveAt(0);
                        print(openPrompts.Count);
                    }

                }
                else if (translateInttoButton(fromInputQueue) == currentButton)
                {
                    print(timer + "Time left");
                    timer = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.time;
                    print("Time reset to " + timer);
                    pressed = true;

                    inputIndex++;
                    if (inputIndex < spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons.Length)
                    {
                        openPrompts[inputIndex - 1].GetComponent<Animator>().Play("Press" + currentButton.ToString());
                        currentButton = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons[inputIndex];
                    }
                    else
                    {
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = true;
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().resumeAnimation();
                        inputIndex = 0;
                        print(openPrompts.Count);
                        while (openPrompts.Count > 0)
                        {
                            Destroy(openPrompts[0]);
                            openPrompts.RemoveAt(0);
                            print(openPrompts.Count);
                        }
                    }
                }
                else if (fromInputQueue != 0 &&
                    inputIndex > 0 && translateInttoButton(fromInputQueue) != spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons[inputIndex - 1])
                {
                    print("wrong input");
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = false;
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().resumeAnimation();
                    inputIndex = 0;
                    while (openPrompts.Count > 0)
                    {
                        Destroy(openPrompts[0]);
                        openPrompts.RemoveAt(0);
                        print(openPrompts.Count);
                    }
                }
            }
        }

        private void Alternate()
        {
            if (!spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().getActionable())
            {
                timer = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.time;
                prompt.GetComponentInChildren<Slider>().maxValue = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.maximumCharge;
                if (!pressed)
                {
                    if (prompt.GetComponentInChildren<Image>().enabled == false)
                    {
                        for (int i = 0; i < spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons.Length; i++)
                        {
                            GameObject newPrompt = Instantiate<GameObject>(additionalPrompt, prompt.transform);
                            newPrompt.transform.position = new Vector3(newPrompt.transform.position.x + 0.7f * i, newPrompt.transform.position.y, newPrompt.transform.position.z);
                            //print(spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].button[i].ToString());
                            newPrompt.GetComponent<Animator>().Play(spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons[i].ToString());
                            openPrompts.Add(newPrompt);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            prompt.GetComponentsInChildren<Image>()[i].enabled = true;
                        }
                    }
                    //changePrompt(currentButton.ToString());
                }
            }
            else
            {
                trackNewInputs();
                if (inputRecord.Count != 0)
                {
                    fromInputQueue = inputRecord.Dequeue();
                }
                if (pressed)
                {
                    timer -= Time.deltaTime;
                    currentCharge -= (Time.deltaTime * spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.chargeDecay);
                }
                if (!pressed || timer >= 0)
                {
                    //print(fromInputQueue);
                    if (translateInttoButton(fromInputQueue) == currentButton)
                    {
                        //print("correct button");
                        for (int i = 0; i < openPrompts.Count; i++)
                        {
                            openPrompts[inputIndex].GetComponent<Animator>().Play(currentButton.ToString());
                        }

                        inputIndex++;
                        if (inputIndex >= openPrompts.Count)
                        {
                            inputIndex = 0;
                        }

                        currentButton = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.buttons[inputIndex];
                        openPrompts[inputIndex].GetComponent<Animator>().Play("Press" + currentButton.ToString());
                        pressed = true;
                        print("charge");

                        currentCharge++;
                    }
                }
                if (timer < 0 && pressed)
                {
                    if (currentCharge >= spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.maximumCharge)
                    {
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = true;

                    }
                    else
                    {
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = false;
                    }
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().resumeAnimation();
                    changePrompt("none");
                    inputIndex = 0;
                    while (openPrompts.Count > 0)
                    {
                        Destroy(openPrompts[0]);
                        openPrompts.RemoveAt(0);
                        print(openPrompts.Count);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        prompt.GetComponentsInChildren<Image>()[i].enabled = false;
                    }
                }
                prompt.GetComponentInChildren<Slider>().value = currentCharge;
            }
        }

        private void Mash()
        {
            if (!spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().getActionable())
            {
                timer = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.time;
                prompt.GetComponentInChildren<Slider>().maxValue = spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.maximumCharge;
                if (!pressed)
                {
                    if (prompt.GetComponentInChildren<Image>().enabled == false)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            prompt.GetComponentsInChildren<Image>()[i].enabled = true;
                        }
                    }
                    changePrompt(currentButton.ToString());
                }
            }
            else
            {
                trackNewInputs();
                if (pressed)
                {
                    timer -= Time.deltaTime;
                    currentCharge -= (Time.deltaTime * spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.chargeDecay);
                }
                if (!pressed || timer >= 0)
                {
                    switch (currentButton)
                    {
                        case commandButton.A4:
                            if (inputRecord.Count != 0)
                            {
                                if (inputRecord.Dequeue() == 4)
                                {
                                    pressed = true;
                                    print("charge");
                                    changePrompt("Press" + currentButton.ToString());
                                    currentCharge++;
                                }
                            }
                            break;
                        case commandButton.A8:
                            if (inputRecord.Count != 0)
                            {
                                if (inputRecord.Dequeue() == 8)
                                {
                                    pressed = true;
                                    print("charge");
                                    changePrompt("Press" + currentButton.ToString());
                                    currentCharge++;
                                }
                            }

                            break;
                    }
                }
                if (timer < 0 && pressed)
                {
                    if (currentCharge >= spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().moveset[menu.pick].action.maximumCharge)
                    {
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = true;

                    }
                    else
                    {
                        spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().actionComplete = false;
                    }
                    spawn.unitList[turn.turnNum].getUnitGO().GetComponent<battleScript>().resumeAnimation();
                    changePrompt("none");
                    for (int i = 0; i < 3; i++)
                    {
                        prompt.GetComponentsInChildren<Image>()[i].enabled = false;
                    }
                }
                prompt.GetComponentInChildren<Slider>().value = currentCharge;
            }
        }

        
    */

    /*
        private static void Hazards()
        {
            if (actionCommands.ActionCommands.enabled)
            {
                stopActionCommands();
            }
        }
    */
    private void Defense(int input)
    {
        if (allowAction)
        {
            if (input == 10 && timedAction)
            {
                CancelInvoke();
                timedAction = false;
                Successful(false);
            }
            else if (input == 11 && parryTime)
            {
                CancelInvoke();
                timedAction = false;
                parryTime = false;
                Successful(false);
                Parried(true);
            }
            else
            {
                print("miss block");
            }
            allowAction = false;
            Invoke("blockTimer", 0.5f);
                
        }
    }
    private commandButton translateInttoButton(int a)
    {
        switch (a)
        {
            case 0:
                return commandButton.NONE;
            case 1:
                return commandButton.A1;
            case 2:
                return commandButton.A2;
            case 3:
                return commandButton.A3;
            case 4:
                return commandButton.A4;
            case 5:
                return commandButton.A5;
            case 6:
                return commandButton.A6;
            case 7:
                return commandButton.A7;
            case 8:
                return commandButton.A8;
            case 9:
                return commandButton.A9;
            case 10:
                return commandButton.A;
            case 11:
                return commandButton.B;
        }
        return commandButton.NONE;
    }
    private int buttonToInt(commandButton button)
    {
        switch (button)
        {
            case commandButton.A:
                return 10;
            case commandButton.B:
                return 11;
            case commandButton.A1:
                return 1;
            case commandButton.A2:
                return 2;
            case commandButton.A3:
                return 3;
            case commandButton.A4:
                return 4;
            case commandButton.A5:
                return 5;
            case commandButton.A6:
                return 6;
            case commandButton.A7:
                return 7;
            case commandButton.A8:
                return 8;
            case commandButton.A9:
                return 9;
            case commandButton.L:
                return 12;
            case commandButton.R:
                return 13;
        }
        return -1;
    }

    private void OnDisable()
    {
        ActionCommandInputs.Button -= trackNewInputs;
        menuMoveHolder.moveData -= SetMove;
        BattleUnitActionCommands.AcceptTime -= AcceptTimedInput;
        BattleUnitActionCommands.AcceptParryTime -= AcceptParry;
        BattleUnitActionCommands.ResetTime -= ResetTimedInput;
        BattleUnitActionCommands.MoveToNext -= MoveToNextButton;
    }

}
