using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModMoveEditing : MonoBehaviour
{
    [SerializeField] DataRecorderMods dataMods;
    DataRecorderParty dataParty;
    ObjectLocator locator;
    private void Awake()
    {
        dataMods = GameObject.Find("DataRecorder").GetComponent<DataRecorderMods>();
        dataParty = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        locator = this.gameObject.GetComponent<ObjectLocator>();
        BattleUnitData.movesetUpdated += CheckMovesetMods;
    }
    void CheckMovesetMods(GameObject updatedCharacter)
    {
        print("checking moveset mods");
        foreach(mods mod in dataMods.getEquippedMods())
        {
            if(mod.GetType().IsSubclassOf(typeof(MoveEditMod)))
            {
                MoveEditMod editMod = (MoveEditMod)mod;
                int partyIndex = dataParty.checkCharacterValid(editMod.characterID);
                //print(locator.locateObject(0).name);
                if (partyIndex != -1)
                {
                    StartCoroutine(waitForLocator(partyIndex, updatedCharacter, editMod));
                }
            }
            else if (mod.GetType().IsSubclassOf(typeof(AddMoveMod)))
                {
                AddMoveMod addMod = (AddMoveMod)mod;
                int partyIndex = dataParty.checkCharacterValid(addMod.characterID);
                //print(locator.locateObject(0).name);
                if (partyIndex != -1)
                {
                    StartCoroutine(waitForLocator(partyIndex, updatedCharacter, addMod));
                }
            }
        }
        
    }
    IEnumerator waitForLocator(int partyIndex, GameObject updatedCharacter, MoveEditMod editMod)
    {
        while(locator.locateObject(0) == null)
        {
            yield return null;
        }
        int battleIndex = locator.locateObject(updatedCharacter);
        if (battleIndex == partyIndex)
        {
            move editableMove = FindMove(updatedCharacter.GetComponent<BattleUnitData>().getMoveset(), editMod.animationName);
            if (editableMove != null)
            {
                print("edited move");
                updatedCharacter.GetComponent<BattleUnitData>().getMoveset()[updatedCharacter.GetComponent<BattleUnitData>().getMoveset().IndexOf(editableMove)] =
                    editMod.Modify(editableMove);
            }
        }
    }
    IEnumerator waitForLocator(int partyIndex, GameObject updatedCharacter, AddMoveMod addMod)
    {
        while (locator.locateObject(0) == null)
        {
            yield return null;
        }
        int battleIndex = locator.locateObject(updatedCharacter);
        if (battleIndex == partyIndex)
        {
            move editableMove = FindMove(updatedCharacter.GetComponent<BattleUnitData>().getMoveset(), addMod.newMoveName);
            if (editableMove != null)
            {
                print("upgraded move");
                updatedCharacter.GetComponent<BattleUnitData>().getMoveset()[updatedCharacter.GetComponent<BattleUnitData>().getMoveset().IndexOf(editableMove)] =
                    addMod.UpgradeAddedMove(editableMove);
            }
            else
            {
                print("added move");
                updatedCharacter.GetComponent<BattleUnitData>().getMoveset().Add(addMod.AddNewMove());
            }
        }
    }
    move FindMove(List<move> moves, string firstAnimName)
    {
        foreach(move m in moves)
        {
            if(m.Name == firstAnimName)
            {
                return m;
            }
        }
        return null;
    }
    private void OnDisable()
    {
        BattleUnitData.movesetUpdated -= CheckMovesetMods;
    }
}
