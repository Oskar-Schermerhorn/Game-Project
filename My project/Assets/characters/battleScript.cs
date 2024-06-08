using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class battleScript: BattleUnit
{
    [SerializeField] public Sprite[] icons;
    protected batteryScript battery;
    private ActionCommandsScript actionCommands;

    new protected void Start()
    {
        base.Start();
        battery = GameObject.Find("battery").GetComponent<batteryScript>();
        actionCommands = GameObject.Find("ActionCommandsSystem").GetComponent<ActionCommandsScript>();
    }
    
    

    override protected void finishAttackWithoutEnding()
    {
        base.finishAttackWithoutEnding();
        //actionCommands.stopActionCommands();
    }

    #region checkRange (0 references)
    public List<int> checkRange()
    {/*
        int mypos = -1;
        for (int i = 0; i < 8; i++)
        {
            if (spawn.unitList[i].getUnitGO() == this.gameObject)
            {
                mypos = i;
            }
        }
        if (mypos > -1)
        {
            return spawn.unitList[mypos].getRangeList();
        }
        else
        {*/
            return new List<int> { 0 };
        //}
    }
    #endregion

    #region Action related
    protected void pauseForActionCommandEvent()
    {
        anim.speed = 0;
        startActionCommand = true;
    }
    private void ActionOpen()
    {
        actionable = true;
    }
    private void ActionClose()
    {
        actionable = false;
    }
    public bool getActionable()
    {
        return actionable;
    }
    public void resumeAnimation()
    {
        if (startActionCommand)
            startActionCommand = false;
        anim.speed = 1;
    }
    #endregion


    //menu.GetComponent<menuScript>().undisplay();

    #region phoenix specific
    private void finishAttackAdaptive()
    {
        bool reviveOffensive = false;
        if (turnManager.turnState == battleState.PLAYERTURN)
        {
            changeAnimation("form2");
            transform.position = pos;
            GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
            //turnManager.endTurn();
            reviveOffensive = true;
        }
        else
        {
            changeAnimation("form2");
            transform.position = pos;
            GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
            reviveOffensive = false;
        }
        //actionCommands.stopActionCommands();
        //turnManager.checkBattleOver();
    } 

    /*if (health <= 0 && this.name.Equals("Phoenix") && form == 1)
        {
            print("Egg");
    int myPosition = spawn.GetComponent<SpawnScript>().findMe(this.gameObject);
            if (myPosition > -1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                myStatus.Clear();
                AddStatus(UpForm);
    AddStatus(egg);
    egg.makeIcon(this.transform);
    spawn.GetComponent<SpawnScript>().remove(myPosition);
    changeAnimation("form2");*/
    #endregion

    //read move data
    override public void data()
    { /*
        print("reading data");
        move[] princeMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A}, 0,0,0)),
            new move(new string[] { "Special1" }, 1, new int[] {2, 3}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A}, 0,0,0)),
            new move(new string[] { "Special2" }, 2, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A}, 0,0,0)),
            new move(new string[] { "Buff" }, 1, new int[] {}, moveTargets.SELF, new effect(none,attackPlus), new int[] {}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Heal1" }, 1, new int[] {-8}, moveTargets.ALLY, new effect(none,permaAttackDown), new int[] {}, targetType.SINGLE, new actionCommand())};
        move[] treeMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.HOLD, new commandButton[]{commandButton.A4}, 2f,0,0)),
            new move(new string[] { "Specail1" }, 2, new int[] {3}, moveTargets.ENEMY, new effect(none,none), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A8}, 3f, 10f, 2f)),
            new move(new string[] { "CC" }, 1, new int[] {}, moveTargets.ENEMY, new effect(thorns,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Position1" }, 1, new int[] {3}, moveTargets.ENEMY, new effect(swap,none), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.HOLD, new commandButton[]{commandButton.A2}, 1.5f,0,0)),
            new move(new string[] { "Position2" }, 2, new int[] {}, moveTargets.ENEMY, new effect(none,none, cactus, noneField), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Special2" }, 3, new int[] {}, moveTargets.ENEMY, new effect(none,none, flowerBomb), new int[] {}, targetType.SINGLE, new actionCommand())};
        move[] phoenixMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {1}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.ALTERNATE, new commandButton[]{commandButton.A4, commandButton.A6}, 2f,10f,2f)),
            new move(new string[] { "Special1" }, 1, new int[] {4}, moveTargets.ENEMY, new effect(none,recoil), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.ALTERNATE, new commandButton[]{commandButton.A2, commandButton.A8}, 2f,10f,3f)),
            new move(new string[] { "Status" }, 1, new int[] {}, moveTargets.ENEMY, new effect(flare,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Position" }, 1, new int[] {3}, moveTargets.ENEMY, new effect(reverseSwap,none), new int[] {4,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.ALTERNATE, new commandButton[]{commandButton.A4, commandButton.A2, commandButton.A6, commandButton.A8 }, 3f,10f,2f)),
            new move(new string[] { "Recoil1" }, 1, new int[] {4}, moveTargets.ENEMY, new effect(none,recoil), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.ALTERNATE, new commandButton[]{commandButton.A, commandButton.B}, 3f,12f,3f)),};
        move[] mulanMoveset1 = new move[] {
            new move(new string[] { "Attack1" }, -1, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A6, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Special1", "Special1v2" }, 1, new int[] {2}, moveTargets.ENEMY, new effect(defenseDown,none), new int[] {4,5,6}, targetType.PAIRS, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4, commandButton.B}, 0.3f,0,0)),
            new move(new string[] { "Special2" }, 2, new int[] {5}, moveTargets.ENEMY, new effect(none,none), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A2, commandButton.A3,commandButton.A6, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Buff" }, 1, new int[] {}, moveTargets.SELF, new effect(none,defensePlus), new int[] {}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChange1-2" }, 1, new int[] {}, moveTargets.SELF, new effect(none,UpForm), new int[] {}, targetType.UNMOVABLE, new actionCommand())};
        move[] mulanMoveset2 = new move[] {
            new move(new string[] { "Attack2" }, -1, new int[] {3}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4,commandButton.A6, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Special3" }, 1, new int[] {4}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A2, commandButton.A1, commandButton.A4, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Special4" }, 1, new int[] {}, moveTargets.SELF, new effect(counter,none), new int[] {}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChange2-1" }, 1, new int[] {}, moveTargets.SELF, new effect(none,DownForm), new int[] {}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChange2-3" }, 1, new int[] {}, moveTargets.SELF, new effect(none,UpForm), new int[] {}, targetType.UNMOVABLE, new actionCommand())};
        move[] mulanMoveset3 = new move[] {
            new move(new string[] { "Attack3" }, -1, new int[] {4}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A6,commandButton.A2, commandButton.A3, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Special5" }, 2, new int[] {3}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4,commandButton.A6, commandButton.A}, 0.3f,0,0)),
            new move(new string[] { "Buff2" }, 1, new int[] {}, moveTargets.SELF, new effect(none,none), new int[] {}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChange3-2" }, 1, new int[] {}, moveTargets.SELF, new effect(none,DownForm), new int[] {}, targetType.UNMOVABLE, new actionCommand())};
        move[] lizardMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Buff1", "Debuff1" }, 1, new int[] {}, moveTargets.BOTH, new effect(attackPlus,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Buff2" }, 2, new int[] {}, moveTargets.ENEMY, new effect(attackPlus,none), new int[] {0,1,2,3}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Status1", "Status2", "Status3" }, 1, new int[] {}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            //make special 2 only able to target 5,6 but deal bonus damage on status?
            new move(new string[] { "Special1", "Special2" }, 1, new int[] {3}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Special3" }, 1, new int[] {1}, moveTargets.ENEMY, new effect(none,none), new int[] {}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Invis" }, 1, new int[] {}, moveTargets.SELF, new effect(none,UpForm), new int[] {}, targetType.SINGLE, new actionCommand())};
        switch (this.gameObject.name)
        {
            case ("Prince"):
                print("prince loaded");
                moveset.AddRange(princeMoveset);
                modData(new int[] { 0, 1, 8 });
                break;
            case ("Tree"):
                print("tree loaded");
                moveset.AddRange(treeMoveset);
                modData(new int[] { 8 });
                break;
            case ("Phoenix"):
                print("phoenix loaded");
                moveset.AddRange(phoenixMoveset);
                break;
            case ("Mulan"):
                print("mulan loaded");
                if (form == 1)
                {
                    moveset.AddRange(mulanMoveset1);
                }
                else if (form == 2)
                {
                    moveset.AddRange(mulanMoveset2);
                }
                else if (form == 3)
                {
                    moveset.AddRange(mulanMoveset3);
                }
                break;
            case ("Lizard"):
                print("lizard loaded");
                moveset.AddRange(lizardMoveset);
                break;
            default:
                moveset.Add(new move(new string[] { "Attack" }, -1, new int[] {1}, moveTargets.ENEMY, new effect(none,none), new int[] {4}, targetType.SINGLE, new actionCommand()));
                break;
        }*/

    }
    //read mod related data
    private void modData(int[] applicable)
    {
        /*
        for(int i = 0; i<spawn.dataParty.GetComponent<DataRecorderScript>().getEquippedMods().Count; i++)
        {
            if(spawn.dataParty.GetComponent<DataRecorderScript>().getEquippedMods()[i].type == modType.MOVE
                || spawn.dataParty.GetComponent<DataRecorderScript>().getEquippedMods()[i].type == modType.BUFF
                || spawn.dataParty.GetComponent<DataRecorderScript>().getEquippedMods()[i].type == modType.SPECIAL)
            {
                for (int j = 0; j < applicable.Length; j++)
                {
                    if (spawn.dataParty.GetComponent<DataRecorderScript>().getEquippedMods()[i].id == applicable[j])
                    {
                        switch (applicable[j])
                        {
                            case 0://attack plus
                                for (int k = 0; k < moveset.Count; k++)
                                {
                                    for(int l = 0; l <moveset[k].damageValues.Length; l++)
                                    {
                                        if (moveset[k].damageValues[l] > 0)
                                            moveset[k].damageValues[l] ++;
                                    }
                                    
                                }
                                break;
                            case 1://prince tornado
                                moveset.Add(new move(new string[]{ "Tornado" },1, new int[] { 3}, moveTargets.ENEMY, new effect(swap, none), new int[] { 4, 5, 6 }, targetType.UNMOVABLE, new actionCommand()));
                                break;
                            //prince heal2
                            //new move(new int[]{ 0, 1, 2, 3}, targetType.UNMOVABLE, -10, 2, "Heal2", moveTargets.PLAYERS, false, actionCommand.NONE)}
                            //prince fireball
                            //new move( 3, 1, "Fireball", flare, none, moveTargets.ENEMIES, true, actionCommand.NONE),
                            case 8: //block plus
                                blockRate++;
                                break;
                        }
                    }
                }
            }
        }*/
    }
}
