using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleUnitStatus : MonoBehaviour
{
    
    public List<statusEffect> myStatus = new List<statusEffect> { };
    ObjectLocator locator;
    SpinHandler spin;
    LibraryStatus LibraryStatus;
    public static event Action ChangeIcon;
    public static event Action StatusDamage;
    int Duration = 0;
    public int defense = 0;

    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        LibraryStatus = GameObject.Find("BattleHandler").GetComponent<LibraryStatus>();
        spin = GameObject.Find("BattleHandler").GetComponent<SpinHandler>();
    }
    public void AddStatus(statusEffect status)
    {
        print("attempt to add status: " + status.effectName);
        if (status.time != statusTime.INSTANT)
        {
            Duration = status.duration;
            if (status.effectName.StartsWith("heavy"))
            {
                status = LibraryStatus.statusDictionary[status.effectName.Remove(0, 6)];
            }
            if (status.animation != null)
            {
                print("got here");
                if (status != LibraryStatus.statusDictionary["poison"] && status != LibraryStatus.statusDictionary["flare"])
                    status.animate(transform, locator.locateObject(this.gameObject));

            }
            if (status.icon != null)
            {
                if (!myStatus.Contains(status) || status.effectName.StartsWith("perma"))
                {
                    print("new status");
                    status.makeIcon(this.transform);
                    myStatus.Add(status);
                    for (int i = 0; i < GetComponentsInChildren<statusIcon>().Length; i++)
                    {
                        if (GetComponentsInChildren<statusIcon>()[i].status == status)
                        {
                            GetComponentsInChildren<statusIcon>()[i].addDuration(Duration - status.duration);
                        }
                    }
                }
                else
                {
                    print("adding to existing status");
                    for (int i = 0; i < GetComponentsInChildren<statusIcon>().Length; i++)
                    {
                        if (GetComponentsInChildren<statusIcon>()[i].status == status)
                        {
                            GetComponentsInChildren<statusIcon>()[i].addDuration(Duration);
                        }
                    }

                }
                ChangeIcon();
            }

        }
        else
        {
            switch (status.type)
            {
                case statusType.DAMAGE:
                    this.gameObject.GetComponent<BattleUnitHealth>().takeDamage(status.amount, false, false);
                    break;
                case statusType.HEAL:
                    print("healing");
                    this.gameObject.GetComponent<BattleUnitHealth>().Heal(status.amount);
                    break;
                case statusType.OTHER:
                    if (status.name.Equals("UpForm"))
                    {
                        print("form increased");
                        this.gameObject.GetComponent<BattleUnitForm>()?.upForm();
                    }
                    else if (status.name.Equals("DownForm"))
                    {
                        print("form decreased");
                        this.gameObject.GetComponent<BattleUnitForm>()?.downForm();
                        
                    }
                    else if (status.name.Equals("spin"))
                    {
                        print("spin");
                        spin.spin(this.gameObject);
                    }
                    else if (status.name.Equals("reverseSpin"))
                    {
                        print("reverseSpin");
                        spin.reverseSpin(this.gameObject);
                    }
                    else if (status.name.Equals("cleanse"))
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
                if (s == LibraryStatus.statusDictionary["egg"])
                {
                    AddStatus(LibraryStatus.statusDictionary["revive"]);
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
    {
        switch (myStatus[which].effectName)
        {
            case ("poison"):
                int poisonTurns = 0;
                for (int i = 0; i < myStatus.Count; i++)
                {
                    if (GetComponentsInChildren<statusIcon>()[i].status == LibraryStatus.statusDictionary["poison"])
                        poisonTurns = GetComponentsInChildren<statusIcon>()[i].poisonTurns;
                }
                this.gameObject.GetComponent<BattleUnitHealth>().takeDamage(myStatus[which].amount + poisonTurns, false, false);
                myStatus[which].animate(this.transform, 1);
                break;
            case ("shock"):
                for (int i = 0; i < myStatus.Count; i++)
                {
                    if (myStatus[i].effectName != "shock")
                    {
                        switch (locator.locateObject(this.gameObject))
                        {
                            case (0):
                                locator.locateObject(1).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(2).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 0);
                                //      1           5
                                //  3       0   4       7
                                //      2           6
                                break;
                            case (1):
                                locator.locateObject(0).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(3).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 1);
                                break;
                            case (2):
                                locator.locateObject(0).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(3).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 2);
                                break;
                            case (3):
                                locator.locateObject(1).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(2).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 3);
                                break;
                            case (4):
                                locator.locateObject(5).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(6).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 4);
                                break;
                            case (5):
                                locator.locateObject(4).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(7).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 5);
                                break;
                            case (6):
                                locator.locateObject(4).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(7).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 6);
                                break;
                            case (7):
                                locator.locateObject(5).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                locator.locateObject(6).GetComponent<BattleUnit>().AddStatus(myStatus[i]);
                                print("giving " + myStatus[i].effectName + " effect");
                                myStatus[which].animate(this.transform, 7);
                                break;
                        }
                    }
                }
                break;
        }
    }



    public void checkOnhit()
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if(myStatus[i].animation != null && myStatus[i].time == statusTime.ONHIT)
            {
                myStatus[i].animate(this.transform, locator.locateObject(this.gameObject));
            }
        }
    }
    protected void flareDamage()
    {
        this.gameObject.GetComponent<BattleUnitHealth>().takeDamage(LibraryStatus.statusDictionary["flare"].amount, false, false);
    }
    public int calcDamageMod()
    {
        int damageModifier = 0;
        for(int i =0; i< myStatus.Count; i++)
        {
            if(myStatus[i].type == statusType.DAMAGEMOD)
            {
                damageModifier += myStatus[i].amount;
                if (transform.Find("StatusIcon(" + myStatus[i].effectName+")").GetComponent<statusIcon>().statusDuration >= 5)
                {
                    damageModifier += myStatus[i].additional;
                }
            }
        }
        return damageModifier;
    }
    public int calcDefenseMod()
    {
        int defenseModifier = defense;
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].type == statusType.DEFENSEMOD)
            {
                defenseModifier += myStatus[i].amount;
                if (transform.Find("StatusIcon(" + myStatus[i].effectName + ")").GetComponent<statusIcon>().statusDuration >= 5)
                {
                    defenseModifier += myStatus[i].additional;
                }
            }
        }
        return defenseModifier;
    }
    virtual public statusEffect checkStatus(statusTime t)
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].time == t)
            {
                return myStatus[i];
            }
        }
        return (LibraryStatus.statusDictionary["none"]);
    }
    virtual public statusEffect checkStatus(statusType t)
    {
        for (int i = 0; i < myStatus.Count; i++)
        {
            if (myStatus[i].type == t)
            {
                return myStatus[i];
            }
        }
        return (LibraryStatus.statusDictionary["none"]);
    }
}
