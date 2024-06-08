using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleUnitData : MonoBehaviour
{
    public List<move> moveset { get; private set; } = new List<move>();
    [SerializeField] protected List<string> moveNames;
    public static event Action<GameObject> movesetUpdated;

    private void Start()
    {
        data();
    }
    public void updateData()
    {
        moveset.Clear();
        data();
    }
    public virtual void data()
    {
        print("reading data");
        move[] princeMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A}, 0,0,0,0)),
            new move(new string[] { "Special1" }, 2, new int[] {1, 2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A, commandButton.A}, 0,0,0,0)),
            //new move(new string[] { "Special2" }, 2, new int[] {2, 2, 2, 2 ,5}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[]{commandButton.A, commandButton.A, commandButton.A, commandButton.A, commandButton.A}, 0,0,0,0)),
            new move(new string[] { "Buff" }, 1, new int[] {}, moveTargets.SELF, new effect("none","attack+"), new int[] {0}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Tornado" }, 1, new int[] {1,1,2,2,2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Heal1" }, 3, new int[] {-4}, moveTargets.ALLY, new effect("none","attackDown"), new int[] {0}, targetType.SINGLE, new actionCommand())};
        move[] treeMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.HOLD, new commandButton[]{commandButton.A4}, 1.5f,0,0,0)),
            new move(new string[] { "Special1" }, 5, new int[] {1, 1, 1, 1, 1}, moveTargets.ENEMY, new effect("none","none"), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A8}, 3f, 10, 5,2)),
            new move(new string[] { "Swap" }, 0, new int[] {}, moveTargets.SELF, new effect("none","none"), new int[] {0, 1}, targetType.PAIRS, new actionCommand()),
            //new move(new string[] { "CC" }, 1, new int[] {}, moveTargets.ENEMY, new effect("thorns","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Position1" }, 1, new int[] {1, 1, 1, 1, 0}, moveTargets.ENEMY, new effect("spin","none"), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.HOLD, new commandButton[]{commandButton.A2}, 1.5f,0,0,0)),
            //new move(new string[] { "Position2" }, 2, new int[] {}, moveTargets.ENEMY, new effect("none","none", "cactus", "noneField"), new int[] {4,5,6,7}, targetType.UNMOVABLE, new actionCommand()),
            //new move(new string[] { "Special2" }, 3, new int[] {}, moveTargets.ENEMY, new effect("none","none", "flowerBomb"), new int[] {4}, targetType.SINGLE, new actionCommand())
            };
        move[] phoenixMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {1, 1}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A4, commandButton.A6}, 2f,15,10,1)),
            new move(new string[] { "Special1" }, 0, new int[] {2}, moveTargets.ENEMY, new effect("none","recoil"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A2, commandButton.A8}, 2f,10,5,2)),
            new move(new string[] { "Status" }, 1, new int[] {}, moveTargets.ENEMY, new effect("flare","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            //new move(new string[] { "Position" }, 1, new int[] {3,3,3}, moveTargets.ENEMY, new effect("reverseSpin","none"), new int[] {4,6,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A4, commandButton.A2, commandButton.A6, commandButton.A8 }, 3f,10,2,1)),
            //new move(new string[] { "Recoil1" }, 1, new int[] {4,4}, moveTargets.ENEMY, new effect("none","recoil"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.MASH, new commandButton[]{commandButton.A, commandButton.B}, 3f,15,5,3)),
            };
        move[] mulanMoveset0 = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A6, commandButton.A}, 0.5f,0,0,0)),
            new move(new string[] { "Special1", "Special1v2" }, 3, new int[] {2, 2}, moveTargets.ENEMY, new effect("defenseDown","none"), new int[] {4,5,6}, targetType.PAIRS, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4, commandButton.B}, 0.5f,0,0,0)),
            new move(new string[] { "Special2" }, 4, new int[] {4, 4}, moveTargets.ENEMY, new effect("none","none"), new int[] {4,7}, targetType.UNMOVABLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A2, commandButton.A3,commandButton.A6, commandButton.A}, 1.5f,0,0,0)),
            new move(new string[] { "Buff" }, 1, new int[] {}, moveTargets.SELF, new effect("none","defense+"), new int[] {0}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChangeUp" }, 1, new int[] {}, moveTargets.SELF, new effect("none","UpForm"), new int[] {0}, targetType.UNMOVABLE, new actionCommand())
            };
        move[] mulanMoveset1 = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {3}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4,commandButton.A6, commandButton.A}, 1.3f,0,0,0)),
            new move(new string[] { "Special1" }, 3, new int[] {2,2,1}, moveTargets.ENEMY, new effect("shock","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A6,commandButton.A4, commandButton.B}, 1f,0,0,0)),
            new move(new string[] { "Special2pt1" }, 1, new int[] {}, moveTargets.SELF, new effect("counter","none"), new int[] {0}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChangeDown" }, 1, new int[] {}, moveTargets.SELF, new effect("none","DownForm"), new int[] {0}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChangeUp" }, 1, new int[] {}, moveTargets.SELF, new effect("none","UpForm"), new int[] {0}, targetType.UNMOVABLE, new actionCommand())};
        move[] mulanMoveset2 = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2, 2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A6,commandButton.A2, commandButton.A3, commandButton.A}, 1.5f,0,0,0)),
            new move(new string[] { "Special" }, 2, new int[] {3, 3, 3, 3, 3}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand(actionCommandType.FIGHT, new commandButton[]{commandButton.A4,commandButton.A6, commandButton.A}, 1.5f,0,0,0)),
            new move(new string[] { "Buff" }, 1, new int[] {}, moveTargets.SELF, new effect("none","none"), new int[] {0}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "StanceChangeDown" }, 1, new int[] {}, moveTargets.SELF, new effect("none","DownForm"), new int[] {0}, targetType.UNMOVABLE, new actionCommand())};
        move[] lizardMoveset = new move[] {
            new move(new string[] { "Attack" }, -1, new int[] {2}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Buff1", "Debuff1" }, 1, new int[] {}, moveTargets.BOTH, new effect("attack+","none"), new int[] {0}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Buff2" }, 2, new int[] {}, moveTargets.ENEMY, new effect("attack+","none"), new int[] {0,1,2,3}, targetType.UNMOVABLE, new actionCommand()),
            new move(new string[] { "Status1", "Status2", "Status3" }, 1, new int[] {}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            //make special 2 only able to target 5,6 but deal bonus damage on status?
            new move(new string[] { "Special1", "Special2" }, 1, new int[] {3}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Special3" }, 1, new int[] {1}, moveTargets.ENEMY, new effect("none","none"), new int[] {4}, targetType.SINGLE, new actionCommand()),
            new move(new string[] { "Invis" }, 1, new int[] {}, moveTargets.SELF, new effect("none","UpForm"), new int[] {0}, targetType.SINGLE, new actionCommand())};
        switch (this.gameObject.name)
        {
            case ("Prince"):
                print("prince loaded");
                moveset.AddRange(princeMoveset);
                //modData(new int[] { 0, 1, 8 });
                break;
            case ("Tree"):
                print("tree loaded");
                moveset.AddRange(treeMoveset);
                //modData(new int[] { 8 });
                break;
            case ("Phoenix"):
                print("phoenix loaded");
                moveset.AddRange(phoenixMoveset);
                break;
            case ("Mulan"):
                print("mulan loaded");
                if (this.gameObject.GetComponent<BattleUnitForm>().form == 0)
                {
                    moveset.AddRange(mulanMoveset0);
                }
                else if (this.gameObject.GetComponent<BattleUnitForm>().form == 1)
                {
                    moveset.AddRange(mulanMoveset1);
                }
                else if (this.gameObject.GetComponent<BattleUnitForm>().form == 2)
                {
                    moveset.AddRange(mulanMoveset2);
                }
                break;
            case ("Lizard"):
                print("lizard loaded");
                moveset.AddRange(lizardMoveset);
                break;
            default:
                moveset.Add(new move(new string[] { "Attack" }, -1, new int[] { 1 }, moveTargets.ENEMY, new effect("none", "none"), new int[] { 4 }, targetType.SINGLE, new actionCommand()));
                break;
        }
        moveNames.Clear();
        for (int i = 0; i < moveset.Count; i++)
        {
            moveNames.Add(moveset[i].animationNames[0]);
        }
        movesetUpdated(this.gameObject);
    }
}
