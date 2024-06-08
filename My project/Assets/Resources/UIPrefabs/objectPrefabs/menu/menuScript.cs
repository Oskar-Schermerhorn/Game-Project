using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    public turnManagement turnManager;
    public GameObject spawn;
    [SerializeField] BattleInputs controls;
    [SerializeField] GameObject target;
    [SerializeField] Transform moveDisplay;
    [SerializeField] GameObject moveBox;
    [SerializeField] GameObject spin;
    private ActionCommandsScript actions;
    private GameObject data;
    private GameObject position;
    GameObject battery;
    GameObject buttonUI;
    public Vector3 spawnposition;

    private BattleInputs battleControls;
    [SerializeField] List<Sprite> stateList = default;
    [SerializeField] List<Sprite> buttonSprites = default;

    public int selection = 0;

    public int pick = 0;
    List<GameObject> movelist = new List<GameObject> { };
    List<GameObject> itemList = new List<GameObject> { };

    public int targetselect = 0;
    public int secondaryTarget = 1;
    public List<int> targetlist = new List<int> { };
    List<GameObject> targetclump = new List<GameObject> { };

    bool unprocessedTurnSwitch = false;

    bool canSwitch = true;
    bool refund = true;
    public int numSwitches = 0;
    bool list = false;
    bool itemsAreListed = false;
    bool targeting = false;
    bool itemTargeting = false;
    //look into animation component

    private void Awake()
    {
        battleControls = new BattleInputs();
        battleControls.Enable();
    }

    private void Start()
    {
        data = GameObject.Find("DataRecorder");
        spawn = GameObject.FindWithTag("BattleHandler");
        actions = GameObject.Find("ActionCommandsSystem").GetComponent<ActionCommandsScript>();
        turnManager = GameObject.FindWithTag("BattleHandler").GetComponent<turnManagement>();
        position = GameObject.Find("BattleHandler/Positions");
        moveMenu(new Vector2(position.GetComponentsInChildren<Transform>()[0].GetComponentsInChildren<Transform>()[0].position.x, position.GetComponentsInChildren<Transform>()[0].GetComponentsInChildren<Transform>()[0].position.y));
        Invoke("display", 0.7f);
        //display();
        battery = GameObject.Find("battery");
        buttonUI = GameObject.Find("buttonUIHolder");
    }
    private void Update()
    {
        if (battleControls.BattleMenu.Navigate.triggered)
        {
            cycle(battleControls.BattleMenu.Navigate.ReadValue<float>());
        }
        if (battleControls.BattleMenu.Select.triggered)
        {
            select();
        }
        if (battleControls.BattleMenu.Back.triggered)
        {
            back();
        }
        if (battleControls.BattleMenu.Swap.triggered)
        {
            swap();
            back();
            back();
            back();
          //  turnManager.handleSwitch();
        }
        if (battleControls.BattleMenu.UndoSwap.triggered && numSwitches > 0)
        {
            undoSwap();
            back();
            back();
            back();
          //  turnManager.handleUnswitch();
        }
    }


    public void enableControls()
    {
        battleControls.Enable();
        selection = 0;
        pick = 0;
        changeImageSprite(selection);
    }
    public void disableControls()
    {
        OnDisable();
    }

    public void OnEnable()
    {
        battleControls.Enable();
    }

    public void OnDisable()
    {
        battleControls.Disable();
    }


    void cycle(float input)
    {/*
        if (input < 0)
        {
            if (!targeting && !list && !itemsAreListed && !itemTargeting)
            {
                selection++;
                if (selection > 2)
                {
                    selection = 0;
                }
            }
            else if(list)
            {
                dehighlightList(pick);
                pick++;
                if(pick > movelist.Count - 1)
                {
                    pick = 0;
                }
            }
            else if (itemsAreListed)
            {
                dehighlightList(pick);
                pick++;
                if (pick > itemList.Count - 1)
                {
                    pick = 0;
                }
            }
            else if (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetType != targetType.UNMOVABLE)
            {
                switch (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetType)
                {
                    case targetType.SINGLE:
                        targetselect++;
                        if (targetselect > targetlist.Count - 1)
                        {
                            targetselect = 0;
                        }
                        break;
                    case targetType.PAIRS:
                        secondaryTarget++;
                        if (secondaryTarget > spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetPos.Length - 1)
                        {
                            secondaryTarget = 1;
                        }
                        break;
                }
                
            }
            
        }
        if (input > 0)
        {
            if (!targeting && !list && !itemsAreListed && !itemTargeting)
            {
                selection--;
                if (selection < 0)
                {
                    selection = 2;
                }
            }
            else if (list)
            {
                dehighlightList(pick);
                pick--;
                if (pick < 0)
                {
                    pick = movelist.Count - 1;
                }

            }
            else if (itemsAreListed)
            {
                dehighlightList(pick);
                pick--;
                if (pick < 0)
                {
                    pick = itemList.Count - 1;
                }
            }
            else if (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetType != targetType.UNMOVABLE)
            {
                switch (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetType)
                {
                    case targetType.SINGLE:
                        targetselect--;
                        if (targetselect < 0)
                        {
                            targetselect = targetlist.Count - 1;
                        }
                        break;
                    case targetType.PAIRS:
                        secondaryTarget--;
                        if (secondaryTarget < 1)
                        {
                            secondaryTarget = spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetPos.Length-1;
                        }
                        break;
                }
            }
            
        }
        if (!targeting && !list && !itemsAreListed && !itemTargeting)
            changeImageSprite(selection);
        else if (list || itemsAreListed)
            highlightList(pick);
        else
        {
            if(targetclump.Count == 1)
            {
                moveTarget(spawn.GetComponent<SpawnScript>().unitList[targetlist[targetselect]].getPosition(), 0);
            }
            else if(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetType == targetType.PAIRS)
            {
                moveTarget(spawn.GetComponent<SpawnScript>().unitList[spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].targetPos[secondaryTarget]].getPosition(), 1);
            }
        }
            */
    }
    void select()
    {/*
        if(!targeting && !list && !itemsAreListed && !itemTargeting)
        {
            if (selection == 0)
            {
                changeImageSprite(3);
                setupTargets(0);
            }
            else if (selection == 1)
            {
                changeImageSprite(4);
                list = true;
                setupList();
            }
            else if (selection == 2)
            {
                changeImageSprite(5);
                itemsAreListed = true;
                setupItemList();
            }
        }
        else if (list)
        {
            if(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick+1].cost <= battery.GetComponent<batteryScript>().getBP())
            {
                list = false;
                for (int i = 0; i < movelist.Count; i++)
                {
                    Destroy(movelist[i]);
                }
                //setupTargets(spawn.GetComponent<SpawnScript>().unitList[turnTracker].getUnitGO().GetComponent<battleScript>().moveset[pick+1].targets);
                setupTargets(pick + 1);
                movelist.Clear();
                pick++;
            }
        }
        else if (itemsAreListed)
        {
            itemsAreListed = false;
            for (int i = 0; i < itemList.Count; i++)
            {
                Destroy(itemList[i]);
            }
            itemList.Clear();
            setupTargetsForItems(data.GetComponent<DataRecorderScript>().items[pick]);
        }
        else if (itemTargeting)
        {
            print("useing item: " + data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).itemName);
            print(data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).hpRestore);
            itemTargeting = false;
            GameObject consumer = spawn.GetComponent<SpawnScript>().unitList[targetlist[targetselect]].getUnitGO();
            if (data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).hpRestore != 0)
            {
                print("heal");
                consumer.GetComponent<BattleUnitHealth>().health += data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).hpRestore;
                if (consumer.GetComponent<BattleUnitHealth>().health > consumer.GetComponent<BattleUnitHealth>().maxhealth)
                {
                    consumer.GetComponent<BattleUnitHealth>().health = consumer.GetComponent<BattleUnitHealth>().maxhealth;
                }
               // if (spawn.GetComponent<SpawnScript>().checkWonBattle())
             //   {
              //      print("Won");

                //    turnManager.endBattle();
              //  }
                else
                {
                    if (!spawn.GetComponent<SpawnScript>().unitList[0].getAlive())
                        spawn.GetComponent<SpawnScript>().spin();
                    if (!spawn.GetComponent<SpawnScript>().unitList[4].getAlive())
                        spawn.GetComponent<SpawnScript>().spinEnemy();
                }

            }
            if(data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).bpRestore != 0)
            {
                battery.GetComponent<batteryScript>().regenBP(data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).bpRestore);
            }
            if (data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).effect != null)
            {
                print("effect");
                consumer.GetComponent<BattleUnitStatus>().AddStatus(data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[pick]).effect);
            }
            data.GetComponent<DataRecorderScript>().usedItem(pick);
            pick = 0;
            while (targetclump.Count > 0)
            {
                Destroy(targetclump[0]);
                targetclump.RemoveAt(0);
            }
            back();
        }
        else
        {
            targeting = false;
            for (int i = 4; i < 8; i++)
            {
                if (spawn.GetComponent<SpawnScript>().unitList[i].getAlive())
                {
                    spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponent<EnemyUnit>()?.hideHP();
                }
            }
            int index = turnManager.turnNum;
            GameObject enemy = spawn.GetComponent<SpawnScript>().unitList[targetlist[targetselect]].getUnitGO();
            print("from menu");
            //actions.actionCommandTime();
            if(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick].animationNames.Length == 1)
            {
                spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnit>().Use(
                spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[pick], enemy, 0);
            }
            //spawn.GetComponent<SpawnScript>().unitList[turnTracker].getUnitGO().GetComponent<battleScript>().Use(
            //    spawn.GetComponent<SpawnScript>().unitList[turnTracker].getUnitGO().GetComponent<battleScript>().moveset[pick], enemy, secondaryTarget);
            if (numSwitches > 0)
                refund = false;
            numSwitches = 0;
            
            while (targetclump.Count > 0)
            {
                Destroy(targetclump[0]);
                targetclump.RemoveAt(0);
            }
            
        }
        */
    }

    void setupList()
    {
        /*
        for(int i = 1; i < spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset.Count; i++)
        {
            GameObject move = Instantiate(moveBox, moveDisplay);
            move.GetComponentInChildren<Text>().text = spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[i].animationNames[0];
            if (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[i].cost > battery.GetComponent<batteryScript>().getBP())
            {
                move.GetComponentInChildren<Text>().color = new Color(241 / 255f, 90 / 255f, 96 / 255f, 255 / 255f);
            }
            movelist.Add(move);
        }
        highlightList(0);*/
    }

    void highlightList(int index)
    {
        if(list)
            movelist[index].GetComponent<Image>().color = new Color(0,149/255f,153/255f);
        else if(itemsAreListed)
            itemList[index].GetComponent<Image>().color = new Color(0, 149 / 255f, 153 / 255f);
    }

    void dehighlightList(int index)
    {
        if(list)
            movelist[index].GetComponent<Image>().color = new Color(199/255f,238/255f,235/255f);
        else if(itemsAreListed)
            itemList[index].GetComponent<Image>().color = new Color(199 / 255f, 238 / 255f, 235 / 255f);
    }

    void setupTargets(int moveindex)
    {/*
        print("setup Targets");

        print(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset);
        print(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].animationNames[0]);
        targeting = true;
        targetselect = 0;
        secondaryTarget = 1;
        targetlist.Clear();
        for(int i = 4; i<8; i++)
        {
            if (spawn.GetComponent<SpawnScript>().unitList[i].getAlive())
            {
                spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponent<EnemyUnit>()?.showHP();
            }
        }
        //print(moveindex);
        //print(spawn.GetComponent<SpawnScript>().unitList[turnTracker].getUnitGO().GetComponent<battleScript>().moveset[moveindex].whom);
        switch (spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].moveTargetType)
        {
            case moveTargets.ALLY:
                targetlist.Add(0);
                targetlist.Add(1);
                targetlist.Add(2);
                targetlist.Add(3);
                break;
            case moveTargets.ENEMY:
                targetlist.AddRange(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getRangeList());
                break;
            case moveTargets.BOTH:
                targetlist.AddRange(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getRangeList());
                targetlist.Add(0);
                targetlist.Add(1);
                targetlist.Add(2);
                targetlist.Add(3);
                break;
            case moveTargets.SELF:
                print("target self");
                targetlist.Add(turnManager.turnNum);
                break;
        }
        print("test");
        createTargets(spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetPos.Length, moveindex);
        */
    }
    

    void createTargets(int t, int moveindex)
    {/*
        print("Create targets" + t);
        if(t == 1)
        {
            targetclump.Add(Instantiate(target, spawnposition, new Quaternion()));
            moveTarget(spawn.GetComponent<SpawnScript>().unitList[targetlist[0]].getPosition(), 0);
        }
        else if(moveindex != -1 && spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetType == targetType.PAIRS)
        {
            for (int i = 0; i < spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetPos.Length-1; i++)
            {
                targetclump.Add(Instantiate(target, spawnposition, new Quaternion()));
                moveTarget(spawn.GetComponent<SpawnScript>().unitList[
                    spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetPos[i]].getPosition(), i);
            }
        }
        else if(moveindex != -1)
        {
            for (int i = 0; i < spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetPos.Length; i++)
            {
                targetclump.Add(Instantiate(target, spawnposition, new Quaternion()));
                moveTarget(spawn.GetComponent<SpawnScript>().unitList[
                    spawn.GetComponent<SpawnScript>().unitList[turnManager.turnNum].getUnitGO().GetComponent<BattleUnitData>().moveset[moveindex].targetPos[i]].getPosition(), i);
            }
        }
        //numberoftargets = t;
        
       */ 
    }

    void changeImageSprite(int index)
    {
        GetComponent<Image>().sprite = stateList[index];
    }

    void back()
    {/*
        itemsAreListed = false;
        list = false;
        pick = 0;
        targeting = false;
        for (int i = 4; i < 8; i++)
        {
            if (spawn.GetComponent<SpawnScript>().unitList[i].getAlive())
            {
                spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponent<EnemyUnit>().hideHP();
            }
        }
        itemTargeting = false;
        changeImageSprite(selection);
        while(targetclump.Count > 0)
        {
            Destroy(targetclump[0]);
            targetclump.RemoveAt(0);
        }
        for(int i = 0; i< movelist.Count; i++)
        {
            Destroy(movelist[i]);
        }
        movelist.Clear();
        for (int i = 0; i < itemList.Count; i++)
        {
            Destroy(itemList[i]);
        }
        itemList.Clear();*/
    }

    void swap()
    { /*
        if (canSwitch)
        {
            canSwitch = false;
            spawn.GetComponent<SpawnScript>().spin();
            numSwitches++;
        }
        else if(battery.GetComponent<batteryScript>().getBP() >= 1)
        {
            spawn.GetComponent<SpawnScript>().spin();
            numSwitches++;
            battery.GetComponent<batteryScript>().useBP(1);
        }
        */
    }
    
    void undoSwap()
    {/*
        if(numSwitches == 1 && refund)
            canSwitch = true;
        else
            battery.GetComponent<batteryScript>().useBP(-1);
        spawn.GetComponent<SpawnScript>().undoSpin();
        numSwitches--;*/
    }

    public void moveTarget(Vector2 moveto, int index)
    {
        print(targetclump);
        targetclump[index].transform.position = moveto;
    }

    public void display()
    {/*
        GetComponent<Image>().enabled = true;
        enableControls();
        for (int i = 0; i < 8; i++)
        {
            if (spawn.GetComponent<SpawnScript>().unitList[i].getAlive())
            {
                for (int j = 0; j < spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponent<BattleUnitStatus>().myStatus.Count; j++)
                {
                    if (spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponentsInChildren<statusIcon>() != null) { }
                        //spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponentsInChildren<statusIcon>()[j].showIcon();
                }
            }
        }
        */
    }

    public void undisplay()
    {/*
        GetComponent<Image>().enabled = false;
        disableControls();
        for(int i = 0; i<8; i++)
        {
            if (spawn.GetComponent<SpawnScript>().unitList[i].getAlive())
            {
                for (int j = 0; j < spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponent<BattleUnitStatus>().myStatus.Count; j++)
                {
                    //spawn.GetComponent<SpawnScript>().unitList[i].getUnitGO().GetComponentsInChildren<statusIcon>()[j].hideIcon();
                }
            }
        }
        */
    }

    public void moveMenu(Vector2 player)
    {
        if (player != new Vector2(transform.position.x, transform.position.y))
        {
            //menu offset
            GetComponent<Transform>().position = new Vector3(player.x, player.y, 0f);
            selection = 0;
            //numSelections = 3;
        }
    }

    void setupItemList()
    {
        for (int i = 0; i < data.GetComponent<DataRecorderScript>().items.Length; i++)
        {
            GameObject item = Instantiate(moveBox, moveDisplay);
            item.GetComponentInChildren<Text>().text = data.GetComponent<DataRecorderScript>().getItem(data.GetComponent<DataRecorderScript>().items[i]).itemName;
            itemList.Add(item);
        }
        highlightList(0);
    }
    void setupTargetsForItems(int itemIndex)
    {
        itemTargeting = true;
        targetselect = 0;
        targetlist.Clear();

        print(data.GetComponent<DataRecorderScript>().getItem(itemIndex).itemName);
        print(data.GetComponent<DataRecorderScript>().getItem(itemIndex).useOnPlayer + " " + data.GetComponent<DataRecorderScript>().getItem(itemIndex).useOnEnemy);
        if (data.GetComponent<DataRecorderScript>().getItem(itemIndex).useOnPlayer)
        {
            print("can use on players");
            targetlist.Add(0);
            targetlist.Add(1);
            targetlist.Add(2);
            targetlist.Add(3);
        }
        if (data.GetComponent<DataRecorderScript>().getItem(itemIndex).useOnEnemy)
        {
            print("can use on enemies");
            targetlist.Add(4);
            targetlist.Add(5);
            targetlist.Add(6);
            targetlist.Add(7);
        }
        createTargets(1, -1);
    }
}
