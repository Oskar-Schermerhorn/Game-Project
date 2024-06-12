using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class move
{
    public string[] animationNames;
    public int cost;
    public int[] damageValues;
    public moveTargets moveTargetType;
    public effect moveEffect;
    public int[] targetPos = { 4 };
    public targetType targetType = targetType.SINGLE;
    public actionCommand action;

    public move(string[] AnimationNames, int Cost, int[] DamageValues, moveTargets MoveTargetType, effect MoveEffect, int[] TargetPos, targetType TargetType, actionCommand Action)
    {
        animationNames = AnimationNames;
        cost = Cost;
        damageValues = DamageValues;
        moveTargetType = MoveTargetType;
        moveEffect = MoveEffect;
        targetPos = TargetPos;
        targetType = TargetType;
        action = Action;
    }

    
}
