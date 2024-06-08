using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mods/MoveEditMods/BigSpender")]
public class BigSpender : MoveEditMod
{
    override public move Modify(move currentMove)
    {
        //increase cost by 1 but allow 1 more damage (if successful)
        move replacement = new move(currentMove.animationNames,
            currentMove.cost + 1,
                currentMove.damageValues, currentMove.moveTargetType, currentMove.moveEffect, currentMove.targetPos, currentMove.targetType,
                new actionCommand(currentMove.action.type, currentMove.action.buttons, currentMove.action.time, currentMove.action.minimum, currentMove.action.additional,
            currentMove.action.maximum + 1));
        return replacement;
    }
}
