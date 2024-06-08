using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class overworldEnemyInformation : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private int minVariance;
    [SerializeField] private int maxAdditional;
    [SerializeField] private bool DoNotRespawn;
    [SerializeField] private List<overworldEnemy> enemyContainer = new List<overworldEnemy> { };
    [SerializeField] private List<overworldEnemy> output = new List<overworldEnemy> { };
    [SerializeField] private int itemDropChance;
    [SerializeField] private List<item> itemOutput;


    public List<overworldEnemy> getList()
    {
        output.Clear();
        int roll = Random.Range(0, 2);
        print("roll if variance: " + roll);
        if ((maxAdditional != 0 || minVariance != numEnemies)&& roll == 1)
        {
            roll = Random.Range(minVariance, maxAdditional + 1 + numEnemies);
            print("number of enemies:" + roll);
            for (int i = 0; i < roll; i++)
            {
                output.Add(enemyContainer[i]);
            }
        }
        else
        {
            for (int i = 0; i < numEnemies; i++)
            {
                output.Add(enemyContainer[i]);
            }
        }
        return output;


    }

    public List<item> getItems()
    {
        itemOutput.Clear();
        for(int i = 0; i<output.Count; i++)
        {
            for(int j=0; j<output[i].potentialItems.Count; j++)
            {
                int roll = Random.Range(0, 100);
                if (roll <= output[i].itemDropChance[j])
                {
                    itemOutput.Add(output[i].potentialItems[j]);
                    j = output[i].potentialItems.Count;
                }
            }
        }
        
        return itemOutput;
    }

    public bool respawn()
    {
        return !DoNotRespawn;
    }
}
