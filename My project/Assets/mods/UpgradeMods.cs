using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum upgradeType { HP, BP, OTHER };
[CreateAssetMenu(menuName = "Mods/UpgradeMod")]
public class UpgradeMods : mods
{
    public upgradeType type;
    public int value;
}
