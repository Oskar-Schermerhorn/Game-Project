using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class move
{
    public string Name;
    public AnimationClip animation;
    public int cost;
    public int Damage;
    public List<moveProperties> MoveProperties;
    public List<targetProperties> MoveTargets;
    public List<enemyAIProperties> EnemyMoveAI;
    public List<effect> MoveEffects;
    public actionCommand action;


    public move()
    {

    }
    public move(string[] AnimationNames, int Cost, int[] DamageValues, moveTargets MoveTargetType, effect MoveEffect, int[] TargetPos, targetType TargetType, actionCommand Action)
    {

    }

    public move(moveProperty prop)
    {
        Name = prop.name;
        animation = prop.animation;
        cost = prop.Cost;
        Damage = prop.Damage;
        MoveProperties = prop.MoveProperties;
        MoveTargets = prop.MoveTargets;
        EnemyMoveAI = prop.EnemyMoveAI;
        MoveEffects = prop.MoveEffects;
        action = prop.Action;
    }
    
    public bool HasProperty(moveProperties prop)
    {
        return (MoveProperties.Contains(prop));
    }
    public bool HasProperty(targetProperties prop)
    {
        return (MoveTargets.Contains(prop));
    }
}
