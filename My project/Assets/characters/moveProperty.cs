using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum moveProperties { SINGLEHIT, MULTIHITPLUS, MULTIHITMINUS, USEBASEATTACK, HEAL, PIERCEDEF, FIXEDDAMAGE, STATECHANGE, NULL }
public enum targetProperties { PLAYERS, ENEMIES, SELF, SINGLETARGET, MULTITARGET, FRONT, FREETARGET, TARGETALL}
public enum enemyAIProperties { PREFERFRONT, RANDOM, PREFERLOW, PREFERUNEFFECTED}
[CreateAssetMenu(menuName = "Players/AttackMoves")]
public class moveProperty: ScriptableObject
{
    public AnimationClip animation;
    public int Cost;
    public int Damage;
    public List<moveProperties> MoveProperties;
    public List<targetProperties> MoveTargets;
    public List<enemyAIProperties> EnemyMoveAI;
    public List<effect> MoveEffects;
    public actionCommand Action;
}
