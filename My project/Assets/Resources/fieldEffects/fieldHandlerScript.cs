using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldHandlerScript : MonoBehaviour
{
    fieldEffects fieldPlayer;
    fieldEffects fieldEnemy;

    public void setPlayerField(fieldEffects f)
    {
        fieldPlayer = f;
        GetComponentsInChildren<Animator>()[0].Play(fieldPlayer.effectName);
    }
    public fieldEffects getPlayerField()
    {
        return fieldPlayer;
    }
    public void setEnemyField(fieldEffects f)
    {
        fieldEnemy = f;
        GetComponentsInChildren<Animator>()[1].Play(fieldEnemy.effectName);
    }
    public fieldEffects getEnemyField()
    {
        return fieldEnemy;
    }
}
