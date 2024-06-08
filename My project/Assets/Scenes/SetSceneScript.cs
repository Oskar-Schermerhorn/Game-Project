using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesInArea = new List<GameObject>();
    DataRecorderCombat data;
    private void Awake()
    {
        PlayerCollision.RespawnEnemies += Respawn;
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderCombat>();
        DataRecorderObjectPermanence dataObjects = GameObject.Find("DataRecorder").GetComponent<DataRecorderObjectPermanence>();
        if (dataObjects.currentRoomName != null && this.gameObject.transform.IsChildOf(GameObject.Find(dataObjects.currentRoomName).transform))
        {
            StartCoroutine(PlayerPosition(dataObjects));
        }
        if (data.getDefeatedEnemies().Count!= 0)
        {
            for (int i = 0; i < enemiesInArea.Count; i++)
            {
                for (int j = 0; j < data.getDefeatedEnemies().Count - 1; j++)
                {
                    if (enemiesInArea[i].GetComponent<overworldEnemyDestroy>().id == data.getDefeatedEnemies()[j])
                    {
                        enemiesInArea[i].SetActive(false);
                    }
                }
                if (enemiesInArea[i].GetComponent<overworldEnemyDestroy>().id == data.getDefeatedEnemies()[data.getDefeatedEnemies().Count - 1])
                {
                    print("defeated");
                    enemiesInArea[i].GetComponent <overworldEnemyDestroy>().defeated(data.itemDropList);
                }
            }
            data.inCombatWith = "";
        }
        
        
        
    }

    void Respawn()
    {
        if(this.gameObject.transform.parent != null)
        {
            print("respawning from " + this.gameObject.transform.parent.parent.name);
            for (int i = 0; i < enemiesInArea.Count; i++)
            {
                if(enemiesInArea[i].GetComponent<overworldEnemyInformation>() != null && enemiesInArea[i].GetComponent<overworldEnemyInformation>().respawn())
                enemiesInArea[i].SetActive(true);
                if (enemiesInArea[i].GetComponent<overworldEnemyBehavior>() != null)
                {
                    enemiesInArea[i].transform.rotation = Quaternion.identity;
                    enemiesInArea[i].transform.position = enemiesInArea[i].GetComponent<overworldEnemyBehavior>().originalPos;
                }

            }
        }

        data.clearDefeatedEnemies();
    }
    IEnumerator PlayerPosition(DataRecorderObjectPermanence dataObjects)
    {
        yield return new WaitForEndOfFrame();
        yield return null;
        dataObjects.restorePosition();
    }
    private void OnDisable()
    {
        PlayerCollision.RespawnEnemies -= Respawn;
    }
}
