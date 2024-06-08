using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionCommandPropmt : MonoBehaviour
{
    move currentMove;
    [SerializeField] Vector2 PromptLocation = Vector2.zero;
    [SerializeField] Vector2 offset = new Vector2(.5f, 0f);
    [SerializeField] GameObject A1;
    [SerializeField] GameObject A2;
    [SerializeField] GameObject A3;
    [SerializeField] GameObject A4;
    [SerializeField] GameObject A5;
    [SerializeField] GameObject A6;
    [SerializeField] GameObject A7;
    [SerializeField] GameObject A8;
    [SerializeField] GameObject A9;
    [SerializeField] GameObject A;
    [SerializeField] GameObject B;
    [SerializeField] GameObject L;
    [SerializeField] GameObject R;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject Bar;
    [SerializeField] List<GameObject> activePrompts = new List<GameObject>();

    private void Awake()
    {
        menuMoveHolder.moveData += PrepareMove;
        targetInput.Confirm += createPrompts;
        targetInput.Cancel += deletePrompts;
        BattleUnitActionCommands.RemovePromptsEarly += deletePrompts;
        BattleUnitInflict.nextActionCommand += showNextPrompt;
        BattleUnitActionCommands.MoveToNext += showNextPrompt;
        ActionCommandHandler.buttonPressed += press;
        ActionCommandHandler.unpressButtons += unpress;
        BattleUnitInflict.actionCommandsDone += afterAction;
        ActionCommandHandler.incrementTimer += handleTimer;
        BattleUnitActionCommands.PromptLocation += setLocation;
    }
    void PrepareMove(move selected) 
    {
        currentMove = selected;
        
    }
    void setLocation(Vector2 ButtonPromptLocation)
    {
        for(int i = 0; i< activePrompts.Count; i++)
        {
            activePrompts[i].transform.position = ButtonPromptLocation;
            if (activePrompts[i].name != "Timer")
                activePrompts[i].transform.position = ButtonPromptLocation + offset *i;
            if(activePrompts[i].name == "Bar")
                activePrompts[i].transform.position = ButtonPromptLocation - offset ;
        }
    }
    private void createPrompts()
    {
        if(currentMove.action.type != actionCommandType.NONE)
        {
            for (int i = 0; i < currentMove.action.buttons.Length; i++)
            {
                GameObject newButton = Instantiate<GameObject>(buttonToPrefab(currentMove.action.buttons[i]), this.gameObject.transform);
                activePrompts.Add(newButton);
                if (currentMove.action.type == actionCommandType.TIMED)
                    break;
            }
            if (currentMove.action.type == actionCommandType.HOLD)
            {
                GameObject newButton = Instantiate<GameObject>(Timer, this.gameObject.transform);
                newButton.name = "Timer";
                activePrompts.Add(newButton);
            }
            if(currentMove.action.type == actionCommandType.MASH)
            {
                GameObject newButton = Instantiate<GameObject>(Bar, this.gameObject.transform);
                newButton.name = "Bar";
                newButton.GetComponent<CompletionBar>().CreateGradient(currentMove);
                activePrompts.Add(newButton);
            }
        }
    }
    private void afterAction()
    {
        Invoke("deletePrompts", 0.5f);
    }
    private void deletePrompts()
    {
        for(int i = 0; i<activePrompts.Count; i++)
        {
            Destroy(activePrompts[i]);
        }
        activePrompts.Clear();
    }
    private void showNextPrompt(int index)
    {
        Vector2 previousPromptPosition = Vector2.zero;
        if(activePrompts.Count>0)
            previousPromptPosition = activePrompts[0].transform.position;
        for (int i = 0; i < activePrompts.Count; i++)
        {
            Destroy(activePrompts[i]);
        }
        activePrompts.Clear();
        if(index < currentMove.action.buttons.Length)
        {
            GameObject newButton = Instantiate<GameObject>(buttonToPrefab(currentMove.action.buttons[index]), this.gameObject.transform);
            newButton.transform.position = previousPromptPosition;
            activePrompts.Add(newButton);
        }
    }
    private void press(commandButton button)
    {
        //print("pressed a button");
        int index = searchForButton(currentMove, button);
        if(index != -1 && activePrompts.Count > index)
        {
            //print("real");
            activePrompts[index].GetComponent<ButtonPrompt>().Select();
        }
    }
    void unpress()
    {
        for(int i=0; i < activePrompts.Count; i++)
        {
            if (activePrompts[i].GetComponent<ButtonPrompt>() != null)
            {
                //print("real");
                activePrompts[i].GetComponent<ButtonPrompt>().Deselect();
            }
        }
        
    }
    private int searchForButton(move currentMove, commandButton button)
    {
        for(int i =0; i<currentMove.action.buttons.Length; i++)
        {
            if(currentMove.action.buttons[i] == button)
            {
                return i;
            }
        }
        return -1;
    }
    private void handleTimer(int index)
    {
        print("handle Timer " + index);
        if(activePrompts.Count>0 && activePrompts[activePrompts.Count-1].name == "Timer")
        {
            activePrompts[activePrompts.Count - 1].GetComponent<ButtonPrompt>().Select(index);
        }
    }
    private void changePrompt(string newState)
    {
        /*if (currentState != newState)
        {
            //print(newState);
            prompt.GetComponent<Animator>().Play(newState);
            currentState = newState;
        }*/
    }
    private GameObject buttonToPrefab(commandButton button)
    {
        switch (button)
        {
            case commandButton.A:
                return A;
            case commandButton.B:
                return B;
            case commandButton.A1:
                return A1;
            case commandButton.A2:
                return A2;
            case commandButton.A3:
                return A3;
            case commandButton.A4:
                return A4;
            case commandButton.A5:
                return A5;
            case commandButton.A6:
                return A6;
            case commandButton.A7:
                return A7;
            case commandButton.A8:
                return A8;
            case commandButton.A9:
                return A9;
            case commandButton.L:
                return L;
            case commandButton.R:
                return R;
        }
        return null;
    }
    private void OnDisable()
    {
        menuMoveHolder.moveData -= PrepareMove;
        targetInput.Confirm -= createPrompts;
        targetInput.Cancel -= deletePrompts;
        BattleUnitActionCommands.RemovePromptsEarly -= deletePrompts;
        BattleUnitInflict.nextActionCommand -= showNextPrompt;
        BattleUnitActionCommands.MoveToNext -= showNextPrompt;
        ActionCommandHandler.buttonPressed -= press;
        ActionCommandHandler.unpressButtons -= unpress;
        BattleUnitInflict.actionCommandsDone -= afterAction;
        ActionCommandHandler.incrementTimer -= handleTimer;
        BattleUnitActionCommands.PromptLocation -= setLocation;
    }
}
