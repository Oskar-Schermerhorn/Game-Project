using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuHandleInput : MonoBehaviour
{
    menuState state;
    menuExecute execute;
    ObjectLocator locator;
    private void Awake()
    {
        state = gameObject.GetComponent<menuState>();
        execute = gameObject.GetComponent<menuExecute>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        MenuInput.Arrows += cycle;
        MenuInput.A += select;
        MenuInput.B += back;
        MenuInput.R += reverseSwap;
        MenuInput.L += swap;
    }
    public void cycle(float direction)
    {
        //print("cycling");
        if(direction < 0)
        {
            state.increaseCurrent();
        }
        else if(direction > 0)
        {
            state.decreaseCurrent();
        }
    }
    public void select()
    {
        print("A");
        state.advance();
    }
    public void back()
    {
        print("B");
    }
    public void swap()
    {
        execute.Spin();
    }

    public void reverseSwap()
    {
        execute.undoSpin();
    }

    private void OnDisable()
    {
        MenuInput.Arrows -= cycle;
        MenuInput.A -= select;
        MenuInput.B -= back;
        MenuInput.R -= reverseSwap;
        MenuInput.L -= swap;
    }
}
