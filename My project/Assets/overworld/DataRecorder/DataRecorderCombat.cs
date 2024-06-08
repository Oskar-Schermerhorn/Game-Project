using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderCombat : MonoBehaviour
{
    public string inCombatWith;
    [SerializeField] Vector2 savedPosition;
    [SerializeField] private List<overworldEnemy> enemyList = new List<overworldEnemy> { };
    [SerializeField] private List<string> defeatedEnemiesInArea = new List<string>();
    [SerializeField] public List<item> itemDropList = new List<item> { };
    public void setCombat(string enemy)
    {
        inCombatWith = enemy;
    }
    public void endCombat()
    {
        defeatedEnemiesInArea.Add(inCombatWith);
    }
    public List<string> getDefeatedEnemies()
    {
        return defeatedEnemiesInArea;
    }
    public void writeEnemy(List<overworldEnemy> input)
    {
        enemyList.Clear();
        enemyList.AddRange(input);
    }
    public void writeItems(List<item> input)
    {
        itemDropList.Clear();
        itemDropList.AddRange(input);
    }
    public List<overworldEnemy> getEnemies()
    {
        return enemyList;
    }
    public void clearDefeatedEnemies()
    {
        defeatedEnemiesInArea.Clear();
    }
}
