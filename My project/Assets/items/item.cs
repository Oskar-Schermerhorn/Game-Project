using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class item : ScriptableObject
{
    public int index;
    public string itemName;
    public string flavor;
    public string effectDescription;

    public Sprite art;
    public Sprite art16Bit;

    public bool useOnPlayer;
    public bool useOnEnemy;
    public bool consume;
    public int hpRestore;
    public int bpRestore;
    public statusEffect effect;

}
