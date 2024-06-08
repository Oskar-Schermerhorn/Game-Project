using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Players/playerCharacter")]
public class playerCharacters : ScriptableObject
{
    public string characterName;
    public Sprite art;
    public int hp;
    public int attack;
    public int defence;

}
