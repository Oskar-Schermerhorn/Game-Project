using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderBattery : MonoBehaviour
{
    [SerializeField] public int bp { get; private set; } = 3;
    [SerializeField] private int maxBp = 3;
    DataRecorderMods dataMods;
    [SerializeField] int modifier;
    private void Awake()
    {
        dataMods = this.gameObject.GetComponent<DataRecorderMods>();
        checkUpgrades();
        DataRecorderMods.ChangedEquippedMods += checkUpgrades;
        EndBattleHandlerScript.RecordBP += recordBP;
        PlayerCollision.RefillBP += fillBP;
        LevelUp.RefillBP += fillBP;
        if (bp > maxBp)
            bp = maxBp;
    }
    public int getMaxBP()
    {
        return maxBp + modifier;
    }
    public void levelUp()
    {
        maxBp++;
    }
    public void checkUpgrades()
    {
        modifier = 0;
        print("checking upgrade mods");
        foreach(mods mod in dataMods.getEquippedMods())
        {
            if(mod.GetType() == typeof(UpgradeMods))
            {
                print("found one");
                UpgradeMods upgrade = (UpgradeMods)mod;
                if (upgrade.type == upgradeType.BP)
                {
                    modifier += upgrade.value;
                }
            }
            
        }
    }
    void recordBP(int amount)
    {
        bp = amount;
        if (bp > maxBp + modifier)
            bp = maxBp + modifier;
    }
    void fillBP()
    {
        bp = maxBp + modifier;
    }
    private void OnDisable()
    {
        DataRecorderMods.ChangedEquippedMods -= checkUpgrades;
        EndBattleHandlerScript.RecordBP -= recordBP;
        PlayerCollision.RefillBP -= fillBP;
        LevelUp.RefillBP -= fillBP;
    }
}
