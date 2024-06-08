using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum fieldTime{SPIN, STARTTURN, NONE};
[CreateAssetMenu]
public class fieldEffects : ScriptableObject
{
    public string effectName;
    public statusEffect effect;
    public int damage;
    public fieldTime time;
}
