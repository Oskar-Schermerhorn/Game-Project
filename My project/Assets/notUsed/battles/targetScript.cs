using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class targetScript : MonoBehaviour
{
    [SerializeField] GameObject menu;
    //[SerializeField] BattleInputs controls;

    BattleInputs targetControls;
    public int selection = 0;
    public int minSelection = 0;
    public int numSelections = 0;

    private void Awake()
    {
        targetControls = new BattleInputs();

    }
    private void Update()
    {
        if (targetControls.Target.Navigate.triggered)
        {
            cycleTarget(targetControls.Target.Navigate.ReadValue<float>());
        }
        if (targetControls.Target.Select.triggered)
        {
            selectTarget();
        }
        if (targetControls.Target.Back.triggered)
        {
            backTarget();
        }

    }
    private void OnEnable()
    {   
        targetControls.Enable();
        target();
        //moveTarget(menu.GetComponent<menuScript>().spawn.GetComponent<SpawnScript>().unitList[selection].getPosition());
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnDisable()
    {
        targetControls.Disable();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<targetScript>().enabled = false;
    }

    void cycleTarget(float input)
    {/*
        if (input > 0)
        {
            selection++;
            if (selection > numSelections - 1)
            {
                selection = minSelection;
            }
        }
        if (input < 0)
        {
            selection--;
            if (selection < minSelection)
            {
                selection = numSelections - 1;
            }
        }
        moveTarget(menu.GetComponent<menuScript>().spawn.GetComponent<SpawnScript>().unitList[selection].getPosition());*/
    }

    void selectTarget()
    {/*
        int index = menu.GetComponent<menuScript>().turnManager.GetComponent<turnManagement>().turnNum ;
        GameObject enemy = menu.GetComponent<menuScript>().spawn.GetComponent<SpawnScript>().unitList[selection].getUnitGO();
        //menu.GetComponent<menuScript>().spawn.GetComponent<SpawnScript>().unitList[index].getUnitGO().GetComponent<battleScript>().Attack(enemy, critical);

        OnDisable();*/
    }

    void backTarget()
    {
        OnDisable();
        menu.GetComponent<menuScript>().enableControls();
    }

    List<int> checkRange()
    {
        //return menu.GetComponent<menuScript>().spawn.GetComponent<SpawnScript>().unitList[menu.GetComponent<menuScript>().turnManager.GetComponent<turnManagement>().turnNum].getRangeList();
        return new List<int>();
    }

    int target()
    {
        selection = checkRange()[0];
        minSelection = selection;
        numSelections = checkRange().Count;
        /*
        if(menu.GetComponent<menuScript>().turnManager.GetComponent<turnManagement>().turnState == battleState.PLAYERTURN)
        {
            selection = 4;
            minSelection = selection;
            numSelections = 4 + checkRange();
        }
        else if(menu.GetComponent<menuScript>().turnManager.GetComponent<turnManagement>().turnState == battleState.ENEMYTURN)
        {
            selection = 0;
            minSelection = selection;
            numSelections = checkRange();
        }*/
        return selection;
    }

    public void moveTarget(Vector2 target)
    {
        if (target != new Vector2(this.transform.position.x, this.transform.position.y))
        {
            this.GetComponent<Transform>().position = new Vector2((target.x), target.y);
        }
    }
}
