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
    public int Selections;
    public List<moveProperties> MoveProperties;
    public List<targetProperties> MoveTargets;
    public List<enemyAIProperties> EnemyMoveAI;
    public List<CardAction> CardAction;
    public List<effect> MoveEffects;
    public actionCommand action;
    public bashProperties Bash;

    public move(moveProperty prop)
    {
        Name = prop.name;
        cost = prop.Cost;
        Damage = prop.Damage;
        Selections = prop.Selections;
        MoveProperties = prop.MoveProperties;
        MoveTargets = prop.MoveTargets;
        EnemyMoveAI = prop.EnemyMoveAI;
        CardAction = prop.CardAction;
        MoveEffects = prop.MoveEffects;
        action = prop.Action;
        Bash = prop.Bash;
    }
    
    public bool HasProperty(moveProperties prop)
    {
        return (MoveProperties.Contains(prop));
    }
    public bool HasProperty(targetProperties prop)
    {
        return (MoveTargets.Contains(prop));
    }
    public bool HasProperty(enemyAIProperties prop)
    {
        return (EnemyMoveAI.Contains(prop));
    }
}
