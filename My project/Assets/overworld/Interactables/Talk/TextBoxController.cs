using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextBoxController : MonoBehaviour
{
    TalkTextBox talk;
    TextBoxWide wide;
    BattleInputs controls;
    bool talking = false;
    public static event Action ReturnControl;
    private void Awake()
    {
        talk = this.gameObject.GetComponent<TalkTextBox>();
        wide = this.gameObject.GetComponent<TextBoxWide>();
        controls = new BattleInputs();
        TalkTextBox.Reset += returnToRegularControls;
        TextBoxWide.Reset += returnToRegularControls;
        controls.Overworld.Interact.started += context => next();
        controls.Overworld.NavigateMenuPages.started += context => shift(context.ReadValue<float>());
    }
    public void StartText(List<string> listOfText, interactableType type)
    {
        print("start text");
        createTextBox(listOfText, type);
        controls.Enable();
        
        
    }
    void createTextBox(List<string> listOfText, interactableType type)
    {
        if(listOfText[0].Contains("~"))
        {
            talk.show(listOfText);
            talking = true;
        }
        else
        {
            wide.show(listOfText);
            talking = false;
        }
    }
    void next()
    {
        print("next");
        if (talking)
        {
            talk.next();
        }
        else
        {
            wide.next();
        }
    }
    void shift(float direction)
    {
        //print("shift");
        if (talking)
        {
            bool back = direction < 0;
            talk.recallText(back);
        }
    }
    void returnToRegularControls()
    {
        print("text box controls disabled");
        controls.Disable();
        ReturnControl();
    }
    private void OnDisable()
    {
        controls.Overworld.Interact.started -= context => next();
        controls.Overworld.NavigateMenuPages.started -= context => shift(context.ReadValue<float>());
        TalkTextBox.Reset -= returnToRegularControls;
        TextBoxWide.Reset -= returnToRegularControls;
    }
}
