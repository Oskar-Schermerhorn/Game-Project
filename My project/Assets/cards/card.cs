using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum cardProperty { NORMAL, ADD, SUB, MULTIPLY, DIVIDE, STATUS }
public enum cardVFX { NONE, AFTERIMAGE }
[Serializable]
public class card
{
    public string name;
    public Sprite sprite;
    public int damageModifier;
    public cardProperty property;
    public cardVFX VFX;
    
    public card(cardValues values)
    {
        name = values.name;
        sprite = values.sprite;
        damageModifier = values.damageModifier;
        property = values.property;
        VFX = values.VFX;
    }
}
