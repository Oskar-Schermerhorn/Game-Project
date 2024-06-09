using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnScript : MonoBehaviour
{
    GameObject battleHandler;
    public List<GameObject> unitList = new List<GameObject>();

    public DataRecorderParty dataParty;
    public DataRecorderCombat dataCombat;
    MoveCoroutine movement;
    StartBattleCoroutine setup;
    [SerializeField] GameObject[] playerPrefabs;

    List<GameObject> enemyList = new List<GameObject>();
    Vector2[] positions = new Vector2[8];
    public float speed;
    private bool undo = false;
    public bool allowInput = false;

    

    public static event Action battleSetup;



    public void Awake()
    {
        dataParty = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        dataCombat = GameObject.Find("DataRecorder").GetComponent<DataRecorderCombat>();
        battleHandler = GameObject.Find("BattleHandler");
        movement = battleHandler.GetComponent<MoveCoroutine>();
        setup = battleHandler.GetComponent<StartBattleCoroutine>();
        Vector2 playerSpawn = GameObject.Find(battleHandler.name + "/SpawnPositions/SpawnPlayer").transform.position;
        Vector2 enemySpawn = GameObject.Find(battleHandler.name + "/SpawnPositions/SpawnEnemy").transform.position;
        if (dataParty != null)
        {
            //GameObject junk = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1]);
            unitList.Clear();
            for(int i = 0; i<3; i++)
            {
                if (dataParty.getPartyIndex(i) != -1)
                {
                    GameObject player = Instantiate<GameObject>(playerPrefabs[dataParty.getPartyIndex(i)], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
                    print("loading player " + i + " hp: " + dataParty.getPartyMember(dataParty.getPartyIndex(i)).hp);
                    player.GetComponent<BattleUnitHealth>().health = dataParty.getPartyMember(dataParty.getPartyIndex(i)).hp;
                    player.GetComponent<BattleUnitHealth>().maxhealth = dataParty.getPartyMember(dataParty.getPartyIndex(i)).maxHp;
                    player.name = player.name.Remove(player.name.Length - "(Clone)".Length);
                    player.GetComponent<BattleUnitID>().UnitSide = side.PLAYER;
                    unitList.Add(player);
                }
            }
            
            for (int i = 0; i< dataCombat.getEnemies().Count; i++)
            {
                GameObject enemy = Instantiate(dataCombat.getEnemies()[i].enemyPrefab, enemySpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                enemy.GetComponent<BattleUnitID>().UnitSide = side.ENEMY;
                enemyList.Add(enemy);
            }
            unitList.AddRange(enemyList);
        }
    }
    public void updateLayer()
    {
        /*
         fix this with different unit numbers

         for(int i = 0; i<8; i++)
        {
            if(i == 0 || i == 3 || i == 4 || i == 7)
            {
                if(unitList[i].GetComponent<SpriteRenderer>() != null && unitList[i].GetComponent<BattleUnitHealth>() != null && unitList[i].GetComponent<BattleUnitHealth>().health >0)
                    unitList[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else if(i == 1 || i == 5)
            {
                if (unitList[i].GetComponent<SpriteRenderer>() != null && unitList[i].GetComponent<BattleUnitHealth>() != null && unitList[i].GetComponent<BattleUnitHealth>().health > 0)
                    unitList[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            else if (i == 2 || i == 6)
            {
                if (unitList[i].GetComponent<SpriteRenderer>() != null && unitList[i].GetComponent<BattleUnitHealth>() != null && unitList[i].GetComponent<BattleUnitHealth>().health > 0)
                    unitList[i].GetComponent<SpriteRenderer>().sortingOrder = 4;
            }
        }*/
    }
    public void startUp()
    {
        print("start battle");
        


        for (int i = 0; i < 8; i++)
        {
            if(i < 4)
            {
                positions[i] = GameObject.Find(battleHandler.name + "/Positions/PlayerPositions/position" + i).transform.position;
            }
            else
            {
                positions[i] = GameObject.Find(battleHandler.name + "/Positions/EnemyPositions/position" + i).transform.position;
            }
            
        }
        GameObject[] units = new GameObject[unitList.Count];
        setup.StartBattle(unitList, positions);
        print("after coroutine call");
        updateLayer();
    }
    public void StartBattle()
    {
        print("start called");
        battleSetup();
    }
}
