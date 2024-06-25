using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mods/MoveEditMods/BigSpender")]
public class BigSpender : MoveEditMod
{
    override public move Modify(move currentMove)
    {
        //increase cost by 1 but allow 1 more damage (if successful)
        move replacement = currentMove;
        replacement.cost++;
        replacement.action = new actionCommand(replacement.action.type, replacement.action.buttons, replacement.action.time, replacement.action.minimum, replacement.action.additional,
            replacement.action.maximum + 1);

        return replacement;
    }
}
