using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMoveHandler : MonoBehaviour
{
    [SerializeField] GameObject menu;
    Animator anim;

    const string special1 = "PrinceBatSpecial1";
    const string special2 = "PrinceBatSpecial2";
    const string tornado = "PrinceBatTornado";
    const string fireball = "PrinceBatFire";
    const string buff = "PrinceBatBuff";
    const string focus = "PrinceBatFocus";
    const string heal1 = "PrinceBatHeal1";
    const string heal2 = "PrinceBatHeal2";
    public string[] moveList = { special1, special2, tornado, fireball, buff, focus, heal1, heal2 };

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    public void setMenu(int selection)
    {
        anim.Play(moveList[selection]);
    }

    public int specialRange(int selection)
    {
        switch (selection)
        {
            default:
                return 0;
            case 3:
                return 4;
            case 4:
                return -1;
            case 5:
                return -1;
            case 6:
                return -2;
            case 7:
                return -2;

        }
    }

    public bool doIMove(int selection)
    {
        switch (selection)
        {
            default:
                return true;
            case 3:
                return false;
            case 4:
                return false;
            case 5:
                return false;
            case 6:
                return false;
            case 7:
                return false;

        }
    }

    public string activate(int selection)
    {
        switch(selection)
        {
            case 0:
                return "Special1";
            case 1:
                return "Special2";
            case 2:
                return "Tornado";
            case 3:
                return "Fireball";
            case 4:
                return "Buff";
            case 5:
                return "Focus";
            case 6:
                return "Heal1";
            case 7:
                return "Heal2";

        }
        return (moveList[selection]);
    }
}
