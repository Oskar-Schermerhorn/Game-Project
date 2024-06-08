using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class menuState : MonoBehaviour
{
    [SerializeField] menuExecute execute;
    [SerializeField] menuHandleInput input;
    [SerializeField] MenuInput control;
    [SerializeField] menuAnimator animator;
    [SerializeField] int currentSelection = 0;
    [SerializeField] int numSelections = 3;
    public static event Action<int> SelectMove;
    public static event Action<int> SelectItem;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<menuAnimator>();
        control = this.gameObject.GetComponent<MenuInput>();
        input = this.gameObject.GetComponent<menuHandleInput>();
        execute = this.gameObject.GetComponent<menuExecute>();
        
        SpawnScript.battleSetup += display;
        targetInput.Cancel += retreat;
        optionsInput.Cancel += retreat;
        optionsController.confirmedMove += moveChosen;
        optionsController.confirmedItem += itemChosen;
    }
    public void advance()
    {
        switch (currentSelection)
        {
            case 0:
                animator.UpdateImages(currentSelection, true);
                SelectMove(0);
                execute.resetIsItem();
                break;
            case 1:
                animator.UpdateImages(currentSelection, true);
                currentSelection = 0;
                execute.createMoveList();
                break;
            case 2:
                animator.UpdateImages(currentSelection, true);
                currentSelection = 0;
                execute.createItemList();
                //numSelections = number of items;
                break;
        }
        control.disableControls();
    }
    public void retreat()
    {
        currentSelection = 0;
        numSelections = 3;
        animator.resetMenu();
            
       
        control.enableControls();
        //execute.changeDisplay();
    }
    public void moveChosen(int index)
    {
        SelectMove(index);
    }
    public void itemChosen(int index)
    {
        SelectItem(index);
    }
    public void increaseCurrent()
    {
        currentSelection++;
        if (currentSelection >= numSelections)
        {
            currentSelection = 0;
        }
        animator.UpdateImages(currentSelection, false);
    }
    public void decreaseCurrent()
    {
        currentSelection--;
        if (currentSelection < 0)
        {
            currentSelection = numSelections - 1;
        }
        animator.UpdateImages(currentSelection, false);
    }
    public void display()
    {
        control.enableControls();
        currentSelection = 0;
        animator.UpdateImages(currentSelection, false);
    }
    public void undisplay()
    {
        print("end of battle undisplay");
        currentSelection = 0;
        //animator.UpdateImages(currentSelection, false);
        control.disableControls();
        SpawnScript.battleSetup -= display;
    }
    private void OnDisable()
    {
        SpawnScript.battleSetup -= display;
        targetInput.Cancel -= retreat;
        optionsInput.Cancel -= retreat;
        optionsController.confirmedMove -= moveChosen;
        optionsController.confirmedItem -= itemChosen;
    }
}
