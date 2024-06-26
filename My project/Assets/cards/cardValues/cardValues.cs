using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class cardValues : ScriptableObject
{
    public Sprite sprite;
    public int damageModifier;
    public cardProperty property;
    public cardVFX VFX;
}
