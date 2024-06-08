using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveEditMod : mods
{
    //public int characterID;
    public string animationName;
    abstract public move Modify(move currentMoveData);
}
