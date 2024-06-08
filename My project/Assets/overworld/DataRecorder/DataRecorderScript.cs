using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum characters { AENEAS, WOODSWORTH, BENNU, TOMOE, HYPERION, EDWARD, TONTTU, LENORE, NONE};
public class DataRecorderScript : MonoBehaviour
{
    public int[] party = new int[4];
    [SerializeField] private partyMember[] possiblePartyMembers;
    public int[] items = new int[5];
    [SerializeField] private item[] possibleItems;
    private int bp =3;
    private int maxBp =3;
    private int maxMp = 3;
    private int mp;
    [SerializeField] private mods[] possibleMods;
    public List<mods> foundMods= new List<mods>();
    [SerializeField] public List<mods> equippedMods= new List<mods>();
    public int exp = 0;
    public string inCombatWith;
    [SerializeField] private List<overworldEnemy> enemyList = new List<overworldEnemy> { };
    [SerializeField] private List<string> defeatedEnemiesInArea = new List<string>();
    [SerializeField] public List<item> itemDropList = new List<item> { };
    [SerializeField] private List<string> interactableList = new List<string>();

    public void Start()
    {
        for(int i = 0; i< Object.FindObjectsOfType<DataRecorderScript>().Length; i++)
        {
            if(Object.FindObjectsOfType<DataRecorderScript>()[i] != this)
            {
                if (Object.FindObjectsOfType<DataRecorderScript>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        mp = maxMp;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public void addCharacter(int index, int character)
    {
        party[index] = character;
    }
    public int checkCharacterValid(int newCharacter)
    {
        for(int i =0; i<4; i++)
        {
            if (party[i] == newCharacter)
            {
                return i;
            }
        }
        return -1;
    }

    public void spinCharList()
    {
        int char0 = party[0];
        int char1 = party[1];
        int char2 = party[2];
        int char3 = party[3];
        party[0] = char2;
        party[1] = char0;
        party[2] = char3;
        party[3] = char1;
    }
    public int readIndex(int index)
    {
        return party[index];
    }

    public void addItem(int newItem)
    {
        int openSlot = -1;
        for(int i = 4; i>=0; i--)
        {
            if (items[i] == 5)
            {
                openSlot = i;
            }
        }
        if (openSlot != -1)
            items[openSlot] = newItem;
    }
    public void replaceItem(int newItem, int index)
    {
        items[index] = newItem;
    }

    public void usedItem(int index)
    {
        print("usedItemFunction");
        for(int i = index; i<(items.Length-1); i++)
        {
            items[i] = items[i+1];
        }
        items[items.Length-1] = 5;
    }
    public int getCurrentParty(int index)
    {
        return party[index];
    }
    public partyMember getParty(int index)
    {
        return possiblePartyMembers[index];
    }
    public item getItem(int index)
    {
        return possibleItems[index];
    }
    public void setCombat(string enemy)
    {
        inCombatWith = enemy;
    }
    public void endCombat()
    {
        defeatedEnemiesInArea.Add(inCombatWith);
    }
    public List<string> getDefeatedEnemies()
    {
        return defeatedEnemiesInArea;
    }
    public void writeEnemy(List<overworldEnemy> input)
    {
        enemyList.Clear();
        enemyList.AddRange(input);
    }
    public void writeItems(List<item> input)
    {
        itemDropList.Clear();
        itemDropList.AddRange(input);
    }
    public List<overworldEnemy> getEnemies()
    {
        return enemyList;
    }
    public void getEXP(int amount)
    {
        exp += amount;
        if(exp >= 100)
        {
            print("Level up");
            exp -= 100;
        }
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
        if(foundMods == null)
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
            for(int i = 0; i<foundMods.Count; i++)
            {
                if(foundMods[i] == mod)
                {
                    mp -= mod.cost;
                    equippedMods.Add(mod);
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
            }
        }
    }
    public void sortFoundMods()
    {
        List<mods> sorted = new List<mods>();
        mods temp = null;
        for(int i = 0; i < foundMods.Count; i++)
        {
            
            sorted.Add(foundMods[i]);
            temp = sorted[i];
            int j = i - 1;
            while(j >= 0 && sorted[j].id > temp.id)
            {
                sorted[j+1] = sorted[j];
                j = j - 1;
            }
            sorted[j+1] = temp;
        }
        foundMods = sorted;
    }
    public void useInteractable(string id)
    {
        interactableList.Add(id);
    }
    public bool checkUsedInteractable(string id)
    {
        for(int i = 0; i< interactableList.Count; i++)
        {
            if(interactableList[i] == id)
            {
                return true;
            }
        }
        return false;
    }
}
