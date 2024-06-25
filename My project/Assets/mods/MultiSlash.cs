using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mods/AddMoveMods/MultiSlash")]
public class MultiSlash : AddMoveMod
{
    move MoveMod;
    override public move AddNewMove()
    {
        //adds specail 2 as a new move
        return MoveMod;
        //return (new move(new string[] { "Multi Slash" }, 2, new int[] { 2, 2, 2, 2, 5 }, moveTargets.ENEMY, new effect("none", "none"), new int[] { 4 }, targetType.SINGLE, new actionCommand(actionCommandType.TIMED, new commandButton[] { commandButton.A, commandButton.A, commandButton.A, commandButton.A, commandButton.A }, 0, 0, 0, 0)));
    }
    public override move UpgradeAddedMove(move currentMoveData)
    {
        //no upgrade for equipping multiple
        //yet?
        return currentMoveData;
    }

}
