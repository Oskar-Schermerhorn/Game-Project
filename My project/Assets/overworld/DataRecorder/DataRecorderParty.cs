using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderParty : MonoBehaviour
{
    public void Awake()
    {
        print("singleton moment");
        for (int i = 0; i < Object.FindObjectsOfType<DataRecorderParty>().Length; i++)
        {
            if (Object.FindObjectsOfType<DataRecorderParty>()[i] != this)
            {
                if (Object.FindObjectsOfType<DataRecorderParty>()[i].name == gameObject.name)
                {
                    print("deleting clone");
                    Destroy(gameObject);
                }
            }

        }
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    [SerializeField] int[] party = new int[4];
    [SerializeField] private partyMember[] possiblePartyMembers;
    private void Start()
    {
        for(int i = 0; i<possiblePartyMembers.Length; i++)
        {
            partyMember copy = Instantiate(possiblePartyMembers[i]);
            possiblePartyMembers[i] = copy;
        }
    }
    public void addCharacter(int index, int character)
    {
        party[index] = character;

    }
    public int checkCharacterValid(int newCharacter)
    {
        for (int i = 0; i < 4; i++)
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
    public void levelUp()
    {
        for(int i = 0; i<8; i++)
        {
            getPartyMember(i).maxHp++;
        }
    }
    public int getPartyIndex(int index)
    {
        return party[index];
    }
    public partyMember getPartyMember(int index)
    {
        return possiblePartyMembers[index];
    }
}
