using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class mods : ScriptableObject
{
    public int id;
    public string modName;
    public int characterID;
    public int cost;
    public string effectDescription;
    public Sprite art;
}
