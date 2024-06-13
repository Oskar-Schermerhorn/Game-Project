using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum actionCommandType { NONE, TIMED, HOLD, FIGHT, AIM, BALANCE, MASH, COMBO, DEFENSE };
public enum commandButton { NONE, A, B, L, R, A1, A2, A3, A4, A5, A6, A7, A8, A9 }
[Serializable]
public class actionCommand
{ 
    public actionCommandType type;
    public commandButton[] buttons;
    public float time;
    public int minimum;
    public int additional;
    public int maximum;
    public actionCommand()
    {
        type = actionCommandType.NONE;
        buttons = new commandButton[] { };
        time = 0;
        minimum = 0;
        additional = 0;
        maximum = 0;
    }
    public actionCommand(actionCommandType Type)
    {
        type = actionCommandType.DEFENSE;
        buttons = new commandButton[] { };
        time = 0;
        minimum = 0;
        additional = 0;
        maximum = 0;
    }
    public actionCommand(actionCommandType Type, commandButton[] Buttons, float Time, int Minimum, int Additional, int Maximum)
    {
        type = Type;
        buttons = Buttons;
        time = Time;
        minimum = Minimum;
        additional = Additional;
        maximum = Maximum;
    }
    public actionCommand(actionCommand action)
    {
        type = action.type;
        buttons = action.buttons;
        time = action.time;
        minimum = action.minimum;
        additional = action.additional;
        maximum = action.maximum;
    }
}