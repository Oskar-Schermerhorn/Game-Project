using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class hazard: ScriptableObject
{
    public string hazardName;
    public int damage;
    public statusEffect status;
    public int turns;
    public string anim;
}
