using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public enum targetType { SINGLE, PAIRS, THREES, FOURS, ALL, UNMOVABLE, NONE }
public enum moveTargets { ALLY, ALLYINCLUDINGDEAD, ENEMY, BOTH, SELF }
public abstract class BattleUnit:MonoBehaviour
{

    [SerializeField] protected turnManagement turnManager;
    [SerializeField] protected menuScript menu;
    [SerializeField] protected SpawnScript spawn;
    protected BattleCamera zoomCamera;
    protected GameObject damageTextP;
    protected GameObject damageTextE;
    protected GameObject spark;
    protected GameObject blast;
    protected GameObject currentTarget;
    public List<move> moveset = new List<move> { };
    public move currentMove;
    public int damageModifier = 0;

    protected statusEffect none;
    protected statusEffect flare;
    protected statusEffect poison;
    protected statusEffect shock;
    protected statusEffect UpForm;
    protected statusEffect DownForm;
    protected statusEffect applyStatus;
    protected statusEffect applySelfStatus;
    protected statusEffect recoil;
    protected statusEffect egg;
    protected statusEffect revive;
    protected statusEffect swap;
    protected statusEffect reverseSwap;
    protected statusEffect attackPlus;
    protected statusEffect defensePlus;
    protected statusEffect attackDown;
    protected statusEffect defenseDown;
    protected statusEffect counter;
    protected statusEffect permaAttackDown;
    protected statusEffect thorns;
    protected statusEffect cleanse;
    protected statusEffect fullHeal;
    protected hazard flowerBomb;
    protected fieldEffects cactus;
    protected fieldEffects noneField;
    protected Animator anim;

    protected string currentState = "neutral";
    public int health; //
    public int maxhealth; //
    protected int tempLayer;
    public bool adjusted = false;
    public Vector2 pos;
    protected bool blockable = false;
    public bool blocked = false;
    protected bool parryable = false;
    public bool parried = false;
    
    public List<statusEffect> myStatus = new List<statusEffect> { };
    

    public int form = 1;
    [SerializeField] public Sprite[] formSprites;
    public int blockRate = 1;
    
    public bool startActionCommand = false;
    protected bool actionable = false;
    public bool actionComplete = false;
    
    public UnityEvent hit;

    protected void Start()
    {
        print("Start" + this.gameObject.name);
        anim = GetComponent<Animator>();
        print(GameObject.Find("TurnManager"));
        turnManager = GameObject.Find("TurnManager").GetComponent<turnManagement>();
        menu = GameObject.Find("Canvas/newMenu").GetComponent<menuScript>();
        spawn = GameObject.Find("SpawnController").GetComponent<SpawnScript>();
        SpawnScript.battleSetup +=data;
        print("set listener by " + gameObject.name);
        zoomCamera = GameObject.Find("CameraZoomFollow").GetComponent<BattleCamera>();
        damageTextP = Resources.Load<GameObject>("UIPrefabs/DamageText/DamageText(Player)");
        damageTextE = Resources.Load<GameObject>("UIPrefabs/DamageText/DamageText(Enemy)");

        none = Resources.Load<statusEffect>("statusEffects/none");
        flare = Resources.Load<statusEffect>("statusEffects/flare");
        poison = Resources.Load<statusEffect>("statusEffects/poison");
        shock = Resources.Load<statusEffect>("statusEffects/shock");
        UpForm = Resources.Load<statusEffect>("statusEffects/UpForm");
        DownForm = Resources.Load<statusEffect>("statusEffects/DownForm");
        recoil = Resources.Load<statusEffect>("statusEffects/recoil");
        egg = Resources.Load<statusEffect>("statusEffects/egg");
        revive = Resources.Load<statusEffect>("statusEffects/revive");
        swap = Resources.Load<statusEffect>("statusEffects/swap");
        reverseSwap = Resources.Load<statusEffect>("statusEffects/reverseSwap");
        attackPlus = Resources.Load<statusEffect>("statusEffects/attack+");
        defensePlus = Resources.Load<statusEffect>("statusEffects/defense+");
        attackDown = Resources.Load<statusEffect>("statusEffects/attackDown");
        defenseDown = Resources.Load<statusEffect>("statusEffects/defenseDown");
        counter = Resources.Load<statusEffect>("statusEffects/counter");
        permaAttackDown = Resources.Load<statusEffect>("statusEffects/permaAttackDown");
        thorns = Resources.Load<statusEffect>("statusEffects/thorns");
        cleanse = Resources.Load<statusEffect>("statusEffects/cleanse");

        flowerBomb = Resources.Load<hazard>("hazards/FlowerBomb");

        cactus = Resources.Load<fieldEffects>("fieldEffects/cactus");
        noneField = Resources.Load<fieldEffects>("fieldEffects/noneField");
    }

    virtual public void data()
    { 
        print("BattleUnit data");
        //read data (in respective child)
    }
    virtual public void takeDamage(int damage, bool blocked, bool parried)
    {
        checkStatus(statusTime.ONHIT);
        if (damage < 0)
            damage = 0;
        health -= damage;
        print(health + "/" + maxhealth);
        if (health <= 0)
        {
            print("dead");
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            int myPosition = 0;// = spawn.findMe(this.gameObject);
            if (myPosition > -1)
            {
                //spawn.remove(myPosition);
                /*if (myPosition == 0)
                {
                    //spawn.GetComponent<SpawnScript>().spin();
                }*/
            }
        }
        showDamageText(damage, blocked, parried);
        if (!parried)
        {
            if (!blocked)
            {
                anim.Play("hit");
            }
            else
            {
                anim.Play("blockHit");
            }
        }
        hit.Invoke();
    }


    /*
    public void inflictHazard()
    {
        hazards.createHazard(moveset[menu.pick].moveEffect.hazard, spawn.findMe(currentTarget));
    }
    public void inflictField()
    {
        fields.setEnemyField(moveset[menu.pick].moveEffect.inflictField);
    }
    public void inflictSelfField()
    {
        fields.setPlayerField(moveset[menu.pick].moveEffect.selfField);
    }*/

    public void Use(move selectedMove, GameObject target, int animationIndex)
    {
        print("Use " + selectedMove.animationNames[0]);
        //set offset, sorting order, animation
        //offset changed; just use pivot point in sprite
        handleOffset(selectedMove, target);
        currentTarget = target;
        print(selectedMove.animationNames.Length + " " + animationIndex);
        if (selectedMove.animationNames.Length > animationIndex)
            changeAnimation(selectedMove.animationNames[animationIndex]);

        currentMove = selectedMove;

        //check for status
        checkStatus(statusTime.MOVESELECTED);


        //get rid of menu
        menu.undisplay();
    }

    private void handleOffset(move selectedMove, GameObject target)
    {
        float offset = -1.36f;
        if (selectedMove.moveTargetType == moveTargets.ALLY || selectedMove.moveTargetType == moveTargets.SELF)
            offset = 0f;
        pos = transform.position;
        print("position saved: " + pos);
        //if (spawn.findMe(this.gameObject) >= 4)
       // {
       //     offset *= -1;
       // }
        if (offset != 0)
            transform.position = new Vector2(target.transform.position.x + offset, target.transform.position.y);

        tempLayer = GetComponent<SpriteRenderer>().sortingOrder;
        GetComponent<SpriteRenderer>().sortingOrder = target.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }





    //inflict functions
    protected void inflictDamage(int HitNumber)
    {
        //check counter
        /*
         * if(targetPlayer.GetComponent<battleScript>().myStatus[i].effectName == "counter")
                {
                    targetPlayer.GetComponent<battleScript>().Use(new battleScript.move(new int[] { 0 }, targetType.UNMOVABLE, attack*2, 0, "Counter", shock, none, moveTargets.SELF, false, actionCommand.NONE)
                        , this.gameObject, 2);
         */

        if (moveset[menu.pick].action.type == actionCommandType.NONE || actionComplete)
        {
            currentTarget.GetComponent<BattleUnit>().takeDamage(currentMove.damageValues[HitNumber] +damageModifier, false, false);
            if (currentMove.moveEffect.inflictStatus.effectName != "none")
            {
                currentTarget.GetComponent<BattleUnit>().AddStatus(applyStatus);
            }
            if (currentMove.moveEffect.inflictStatus.effectName != "none")
            {
                AddStatus(applyStatus);
            }
        }
        else
        {
            currentTarget.GetComponent<BattleUnit>().takeDamage(currentMove.damageValues[HitNumber] + damageModifier / 2, false, false);
        }
        if (HitNumber >= currentMove.damageValues.Length)
            actionComplete = false;

        if (applySelfStatus!= null && currentMove.moveEffect.selfStatus.effectName == "recoil")
        {
            takeDamage(currentMove.damageValues[HitNumber] + damageModifier / 2, false, false);
        }
        for (int i = 0; i < currentTarget.GetComponent<BattleUnit>().myStatus.Count; i++)
        {
            if (currentTarget.GetComponent<BattleUnit>().myStatus[i].effectName == "thorns")
            {
                takeDamage(1, false, false);
                finishAttack(0);
                break;
            }
        }
    }

    

    protected void inflictMultiTargetDamage(int number, int HitNumber)
    {
        /*
        if (spawn.unitList[number].getAlive())
        {
            if (actionComplete)
            {
                spawn.unitList[number].getUnitGO().GetComponent<BattleUnit>().takeDamage(currentMove.damageValues[HitNumber] + damageModifier, false, false);
                if (currentMove.moveEffect.inflictStatus.effectName != "none" && currentMove.moveEffect.inflictStatus.effectName != "swap" && currentMove.moveEffect.inflictStatus.effectName != "reverseSwap")
                {
                    spawn.unitList[number].getUnitGO().GetComponent<BattleUnit>().AddStatus(applyStatus);
                }
            }
            else
            {
                spawn.unitList[number].getUnitGO().GetComponent<BattleUnit>().takeDamage(currentMove.damageValues[HitNumber] + damageModifier / 2, false, false);
            }
            //actionComplete = false;
            if (currentMove.moveEffect.selfStatus.effectName == "recoil")
            {
                takeDamage(currentMove.damageValues[HitNumber] + damageModifier / 2, false, false);
            }
        }*/
    }
    protected void inflictStatus()
    {
        if (moveset[menu.pick].action.type == actionCommandType.NONE || actionComplete)
        {
            currentTarget.GetComponent<BattleUnit>().AddStatus(applyStatus);
        }

        actionComplete = false;
    }
    protected void inflictMultiTargetStatus(int number)
    {/*
        if (spawn.unitList[number].getAlive())
        {
            if (moveset[menu.pick].action.type == actionCommandType.NONE || actionComplete)
            {
                if (currentMove.moveEffect.inflictStatus.effectName != "none" && currentMove.moveEffect.inflictStatus.effectName != "swap" && currentMove.moveEffect.inflictStatus.effectName != "reverseSwap")
                {
                    spawn.unitList[number].getUnitGO().GetComponent<BattleUnit>().AddStatus(applyStatus);
                }
            }
        }*/
    }
    protected void inflictHeal()
    {
        if (moveset[menu.pick].action.type == actionCommandType.NONE || actionComplete)
        {
            currentTarget.GetComponent<BattleUnit>().Heal(-currentMove.damageValues[0]);
            if (currentMove.moveEffect.inflictStatus.effectName != "none")
            {
                currentTarget.GetComponent<BattleUnit>().AddStatus(applyStatus);
            }
        }
        else
        {
            currentTarget.GetComponent<BattleUnit>().Heal(-currentMove.damageValues[0]);
        }

        actionComplete = false;
    }

    public void Heal(int healAmmount)
    {
        health += healAmmount;
        print(health + "/" + maxhealth);
        if (health > maxhealth)
        {
            health = maxhealth;
        }
        showDamageText(-healAmmount, false, false);
    }


    


    
    
    public void AddStatus(statusEffect s)
    {
        print("attempt to add status: " + s.effectName);
        if (s.time != statusTime.INSTANT)
        {

            if (s.animation != null)
            {
                print("got here");
                //if (s.effectName != "poison")
                    //s.animate(transform, spawn.findMe(this.gameObject));
               // else
                  //  s.animate(transform, 0);

            }
            if (s.icon != null)
            {
                if (!myStatus.Contains(s) || s.effectName.StartsWith("perma"))
                {
                    print("new status");
                    s.makeIcon(this.transform);
                    myStatus.Add(s);
                }
                else
                {
                    print("adding to existing status");
                    for (int i = 0; i < GetComponentsInChildren<statusIcon>().Length; i++)
                    {
                        if (GetComponentsInChildren<statusIcon>()[i].status == s)
                        {
                            GetComponentsInChildren<statusIcon>()[i].addDuration(s.duration);
                        }
                    }

                }
            }
        }
        else
        {
            switch (s.type)
            {
                case statusType.DAMAGE:
                    //takeDamage(s.damage, false, false);
                    break;
                case statusType.HEAL:
                    print("healing");
                    //Heal(s.damage);
                    break;
                case statusType.OTHER:
                    if (s.name.Equals("UpForm"))
                    {
                        print("form increased");
                        form++;
                        moveset.Clear();
                        data();
                    }
                    else if (s.name.Equals("DownForm"))
                    {
                        print("form decreased");
                        form--;
                        moveset.Clear();
                        data();
                    }
                    else if (s.name.Equals("spin"))
                    {
                        print("spin");
                        //spawn.spin();
                    }
                    else if (s.name.Equals("reverseSpin"))
                    {
                        print("reverseSpin");
                        //spawn.undoSpin();
                    }
                    else if (s.name.Equals("cleanse"))
                    {
                        print("cleanse");
                        for (int i = 0; i < myStatus.Count; i++)
                        {
                            Destroy(GetComponentsInChildren<statusIcon>()[i].gameObject);
                        }
                        myStatus.Clear();
                    }
                    break;
            }


        }
    }
    public void removeStatus(statusEffect s)
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i] == s)
            {
                myStatus.RemoveAt(i);
                i--;
                if (s == egg)
                {
                    AddStatus(revive);
                    ApplyStatus(0);
                    removeStatus(revive);
                }
            }
        }
    }

    public void checkStartStatus()
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].time == statusTime.STARTTURN)
            {
                ApplyStatus(i);
            }
        }
    }
    public void ApplyStatus(int which)
    {/*
        switch (myStatus[which].effectName)
        {
            case ("poison"):
                int poisonTurns = 0;
                for (int i = 0; i < myStatus.Count; i++)
                {
                    if (GetComponentsInChildren<statusIcon>()[i].status == poison)
                        poisonTurns = GetComponentsInChildren<statusIcon>()[i].poisonTurns;
                }
                //takeDamage(myStatus[which].damage + poisonTurns, false, false);
                myStatus[which].animate(this.transform, 1);
                break;
            case ("shock"):
                for (int i = 0; i < myStatus.Count; i++)
                {
                    if (myStatus[i].effectName != "shock")
                    {
                        switch (spawn.findMe(this.gameObject))
                        {
                            case (0):
                                spawn.unitList[1].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[2].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 0);
                                //      1           5
                                //  3       0   4       7
                                //      2           6
                                break;
                            case (1):
                                spawn.unitList[0].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[3].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 1);
                                break;
                            case (2):
                                spawn.unitList[0].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[3].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 2);
                                break;
                            case (3):
                                spawn.unitList[1].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[2].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 3);
                                break;
                            case (4):
                                spawn.unitList[5].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[6].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 4);
                                break;
                            case (5):
                                spawn.unitList[4].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[7].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 5);
                                break;
                            case (6):
                                spawn.unitList[4].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[7].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 6);
                                break;
                            case (7):
                                spawn.unitList[5].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                spawn.unitList[6].getUnitGO().GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 7);
                                break;
                        }
                    }
                }
                break;
        }*/
    }

    

    protected void checkFlare()
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].effectName == "flare")
            {
                //myStatus[i].animate(this.transform, spawn.findMe(this.gameObject));
                Invoke("flareDamage", 0.1f);
            }

        }
    }
    protected void flareDamage()
    {
        //health -= flare.damage;
        print(health + "/" + maxhealth);
        if (health <= 0)
        {
            //print("dead");
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
           // int myPosition = spawn.findMe(this.gameObject);
          //  if (myPosition > -1)
          //  {
              //  spawn.remove(myPosition);
            //    if (myPosition == 4)
            //    {
                    //spawn.GetComponent<SpawnScript>().spinEnemy();
            //    }
         //   }

        }

        //showDamageText(flare.damage, false, false);
    }

    virtual public void checkStatus(statusTime t)
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].time == t)
            {
                /*
                
                TODO: effect

                switch (myStatus[i].effectName)
                {
                    case "defense+":
                        damage -= 1;
                        break;
                    case "defenseDown":
                        damage += 1;
                        break;



                switch (myStatus[i].effectName)
                {
                    case "attack+":
                        if (currentMove.damageValues[0] >= 0)
                            damageModifier += 2;
                        break;
                    case "attackDown":
                        if (currentMove.damageValues[0] >= 0)
                            damageModifier -= 2;
                        break;
                    case "permaAttackDown":
                        damageModifier -= 1;
                        break;
                }
                }*/
            }
        }
    }

    protected void showDamageText(int damage, bool blocked, bool parried)
    {/*
        GameObject pos;
        GameObject text;
        if(5 >= 4)
        {
            //pos = GameObject.Find("Canvas2/EnemyPositions/Position" + spawn.findMe(this.gameObject));
            //print(pos.transform);
            //text = Instantiate(damageTextE, pos.transform);
        }
        else
        {
            //pos = GameObject.Find("Canvas2/PlayerPositions/Position" + spawn.findMe(this.gameObject));
            //text = Instantiate(damageTextP, pos.transform);
        }
        
        //text.GetComponent<TextMeshPro>().text = Mathf.Abs(damage).ToString();
        if (damage < 0)
        {
            //green
            text.GetComponent<TextMeshPro>().color = new Color(71 / 255f, 150 / 255f, 60 / 255f, 255 / 255f);
        }
        else if (parried)
        {
            //gray
            text.GetComponent<TextMeshPro>().color = new Color(185 / 255f, 185 / 255f, 185 / 255f, 255 / 255f);
        }
        else if (blocked)
        {
            //blue
            print("blue text");
            text.GetComponent<TextMeshPro>().color = new Color(88 / 255f, 125 / 255f, 222 / 255f, 255 / 255f);
        }

        */
    }

    // end attack functions
    protected void finishAttack(int hold)
    {
        if (hold != 1)
            changeAnimation("neutral");
        actionComplete = false;
        transform.position = pos;
        GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
       //turnManager.endTurn();
        //Invoke("delayDisplay", 0.2f);
        //delayDisplay();
        if (currentMove.moveEffect.selfStatus.effectName != "none")
        {
            AddStatus(currentMove.moveEffect.selfStatus);
        }
        if (hold != 1 && formSprites.Length > 1)
        {
            if (form == 1)
                changeAnimation("form1");
            else if (form == 2)
                changeAnimation("form2");
            else if (form == 3)
                changeAnimation("form3");
        }
    }
    virtual protected void finishAttackWithoutEnding()
    {

        changeAnimation("neutral");
        transform.position = pos;
        GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
        //delayDisplay();
        if (currentMove.moveEffect.selfStatus.effectName != "none")
        {
            AddStatus(currentMove.moveEffect.selfStatus);
        }
        if (formSprites.Length > 1)
        {
            if (form == 1)
                changeAnimation("form1");
            else if (form == 2)
                changeAnimation("form2");
            else if (form == 3)
                changeAnimation("form3");
        }
      //  turnManager.checkBattleOver();
    }


    //animation functions
    protected void changeAnimation(string newState)
    {
        if (currentState != newState)
        {
            anim.Play(newState);
            currentState = newState;
        }
    }
    public void resetAnimation(string newState)
    {
        anim.Play(newState);
        currentState = newState;
    }
    protected void changeLayer(int layer)
    {
        GetComponent<SpriteRenderer>().sortingOrder = layer;
    }




    //camera functions

    protected void customCamera()
    {
        zoomCamera.zoomFollow(gameObject.GetComponentsInChildren<Transform>()[1]);
    }
    protected void addScreenShake()
    {
        zoomCamera.addScreenShake();
    }
    protected void stopScreenShake()
    {
        zoomCamera.stopScreenShake();
    }
    protected void zoomOut()
    {
        zoomCamera.zoomOutCamera();
    }

    private void OnDisable()
    {
        SpawnScript.battleSetup -= data;
    }
}
