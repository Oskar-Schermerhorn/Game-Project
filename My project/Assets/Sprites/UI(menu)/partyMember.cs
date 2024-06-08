using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Players/partyMember")]
public class partyMember : ScriptableObject
{
    public string characterName;
    public string description;
    public int hp;
    public int maxHp;
    public int atk;
    public int def;
    public Sprite overSprite;
    public Sprite battleSprite;

}
