using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashHandler : MonoBehaviour
{
    public int bashes = 0;
    public int maxbashes = 5;

    public int BashModifier(int damage, bool bash)
    {
        if (bash)
        {
            return (Bash(damage));
        }
        else
        {
            return (Smash(damage));
        }
    }

    private int Bash(int damage)
    {
        int returnvalue = damage - bashes;
        if(returnvalue < 0)
        {
            returnvalue = 0;
        }
        bashes++;
        
        print("BASH");
        print("num bashes " + bashes);
        return returnvalue;
    }
    private int Smash(int damage)
    {
        if (bashes > maxbashes)
        {
            bashes = maxbashes;
        }
        int returnvalue = damage * bashes;
        print("num bashes " + bashes);
        bashes = 0;
        print("SMASH");
        
        return returnvalue;

    }
}
