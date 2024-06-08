using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Players/overworldEnemy")]
public class overworldEnemy : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab;
    public List<item> potentialItems;
    public List<int> itemDropChance;
}
