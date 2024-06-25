using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum statusTarget { INFLICT, SELF}
public enum effectCondition { SUCCESSFUL, ALWAYS, RANDOM}
[Serializable]
public class effect
{
    public statusEffect MoveStatus;
    public statusTarget Target;
    public effectCondition Condition;
    public int amount;
    public int additional;
    public int duration;
    public effect(statusEffect Effect, statusTarget target, effectCondition condition)
    {
        MoveStatus = Effect;
        Target = target;
        Condition = condition;
    }
    public effect(statusEffect Effect, statusTarget target, effectCondition condition, int Amount, int Additional, int Duration)
    {
        MoveStatus = Effect;
        Target = target;
        Condition = condition;
        amount = Amount;
        additional = Additional;
        duration = Duration;
    }
}