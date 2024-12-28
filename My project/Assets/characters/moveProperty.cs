using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public enum moveProperties { SINGLEHIT, MULTIHITPLUS, MULTIHITMINUS, USEBASEATTACK, HEAL, PIERCEDEF, FIXEDDAMAGE, STATECHANGE, NULL }
public enum targetProperties { PLAYERS, ENEMIES, SELF, SINGLETARGET, MULTITARGET, FRONT, FREETARGET, CARDS}
public enum enemyAIProperties { PREFERFRONT, RANDOM, PREFERLOW, PREFERUNEFFECTED}
public enum bashProperties { NORMAL, BASH, SMASH}
[CreateAssetMenu(menuName = "Players/AttackMoves")]
public class moveProperty: ScriptableObject
{
    public int Cost;
    public int Damage;
    public int Selections;
    public List<moveProperties> MoveProperties;
    public List<targetProperties> MoveTargets;
    public List<enemyAIProperties> EnemyMoveAI;
    public List<CardAction> CardAction;
    public List<effect> MoveEffects;
    public actionCommand Action;
    public bashProperties Bash;

    public moveProperty()
    {
        Cost = 0;
        Damage = 0;
        Selections = 1; //only used if targets cards
        MoveProperties = new List<moveProperties>();
        MoveTargets = new List<targetProperties>();
        EnemyMoveAI = new List<enemyAIProperties>();
        CardAction = new List<CardAction>();
        MoveEffects = new List<effect>();
        Action = new actionCommand();
        Bash = bashProperties.NORMAL;

    }
}
