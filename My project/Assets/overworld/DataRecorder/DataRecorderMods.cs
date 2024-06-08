using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataRecorderMods : MonoBehaviour
{
    private int maxMp = 3;
    private int mp;
    [SerializeField] private mods[] possibleMods;
    public List<mods> foundMods = new List<mods>();
    [SerializeField] public List<mods> equippedMods = new List<mods>();
    public static event Action ChangedEquippedMods;
    private void Start()
    {
        mp = maxMp;
    }
    public List<mods> getFoundMods()
    {
        return foundMods;
    }
    public List<mods> getEquippedMods()
    {
        return equippedMods;
    }
    public int getMaxMP()
    {
        return maxMp;
    }
    public void levelUp()
    {
        maxMp++;
    }
    public int getCurrentMP()
    {
        return mp;
    }
    public void foundNewMod(int index)
    {
        /*
        mods mod = null;
        for(int i= 0; i<possibleMods.Length; i++)
        {
            if(possibleMods[i].id == index)
            {
                mod = possibleMods[i];
            }
        }
        */
        if (foundMods == null)
        {
            foundMods = new List<mods>();
        }
        foundMods.Add(possibleMods[index]);
        sortFoundMods();
    }
    public bool equipMod(mods mod)
    {
        if (mp - mod.cost >= 0)
        {
            for (int i = 0; i < foundMods.Count; i++)
            {
                if (foundMods[i] == mod)
                {
                    mp -= mod.cost;
                    equippedMods.Add(mod);
                    ChangedEquippedMods();
                    return true;
                }
            }
        }
        return false;
    }
    public void unequipMod(mods mod)
    {

        for (int i = 0; i < equippedMods.Count; i++)
        {
            if (equippedMods[i] == mod)
            {
                mp += mod.cost;
                equippedMods.RemoveAt(i);
                i += equippedMods.Count;
                ChangedEquippedMods();
            }
        }
    }
    public void sortFoundMods()
    {
        List<mods> sorted = new List<mods>();
        mods temp = null;
        for (int i = 0; i < foundMods.Count; i++)
        {

            sorted.Add(foundMods[i]);
            temp = sorted[i];
            int j = i - 1;
            while (j >= 0 && sorted[j].id > temp.id)
            {
                sorted[j + 1] = sorted[j];
                j = j - 1;
            }
            sorted[j + 1] = temp;
        }
        foundMods = sorted;
    }
}
