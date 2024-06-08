using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AddMoveMod : mods
{
    public string newMoveName;
    abstract public move AddNewMove();
    abstract public move UpgradeAddedMove(move currentMoveData);
}
