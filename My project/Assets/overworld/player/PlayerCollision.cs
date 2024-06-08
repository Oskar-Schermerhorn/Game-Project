using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollision : MonoBehaviour
{
    DataRecorderCombat dataCombat;
    DataRecorderObjectPermanence dataObject;
    DataRecorderParty dataParty;
    DataRecorderMods dataMod;
    DataRecorderBattleBackground dataBackground;
    DataRecorderProgress dataProgress;
    public static event Action<int> ShowItems;
    public static event Action RespawnEnemies;
    public static event Action RefillBP;
    public bool allowCollision { get; private set; } = false;
    private void Awake()
    {
        dataCombat = GameObject.Find("DataRecorder").GetComponent<DataRecorderCombat>();
        dataObject = GameObject.Find("DataRecorder").GetComponent<DataRecorderObjectPermanence>();
        dataParty = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        dataMod = GameObject.Find("DataRecorder").GetComponent<DataRecorderMods>();
        dataBackground = GameObject.Find("DataRecorder").GetComponent<DataRecorderBattleBackground>();
        dataProgress = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        StartCoroutine(StopIFrames());
    }
    public void IFrames()
    {
        allowCollision = false;
    }
    public void EndIFrames()
    {
        StartCoroutine(StopIFrames());
    }
    IEnumerator StopIFrames()
    {
        yield return new WaitForSeconds(0.3f);
        allowCollision = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowCollision)
        {
            //print("collision");
            switch (collision.gameObject.tag)
            {

                case "enemyTeam":
                    dataCombat.setCombat(collision.GetComponent<overworldEnemyDestroy>().id);
                    dataObject.savePosition(this.gameObject.transform.position);
                    dataCombat.writeEnemy(collision.GetComponent<overworldEnemyInformation>().getList());
                    dataCombat.writeItems(collision.GetComponent<overworldEnemyInformation>().getItems());
                    dataBackground.setBackground();
                    SceneManager.LoadScene(sceneName: "BattleScene");
                    break;
                case "healStation":
                    for (int i = 0; i < 8; i++)
                    {
                        dataParty.getPartyMember(i).hp = dataParty.getPartyMember(i).maxHp;
                    }
                    RefillBP();
                    RespawnEnemies();
                    dataProgress.SetSavePosition(collision.gameObject);
                    break;
                case "itemDrops":

                    string index = collision.gameObject.name.Split(':')[1];
                    int id = int.Parse(index);
                    ShowItems(id);
                    if(collision.gameObject.GetComponent<OneTimeUse>() != null)
                    {
                        collision.gameObject.GetComponent<OneTimeUse>().Use();
                    }
                    Destroy(collision.gameObject);
                    break;
                case "mod":
                    index = collision.gameObject.name.Split(':')[1];
                    id = int.Parse(index);
                    dataMod.foundNewMod(id);
                    if (collision.gameObject.GetComponent<OneTimeUse>() != null)
                    {
                        collision.gameObject.GetComponent<OneTimeUse>().Use();
                    }
                    Destroy(collision.gameObject);
                    break;
            }
        }
        

    }

}
