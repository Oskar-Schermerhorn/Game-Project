using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SpawnScript : MonoBehaviour
{
    GameObject battleHandler;
    public GameObject[] unitList = new GameObject[8];

    public DataRecorderParty dataParty;
    public DataRecorderCombat dataCombat;
    MoveCoroutine movement;
    StartBattleCoroutine setup;
    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] GameObject player;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject player3;
    [SerializeField] GameObject player4;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
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
            if (dataParty.getPartyIndex(0) != -1)
            {
                player = Instantiate<GameObject>(playerPrefabs[dataParty.getPartyIndex(0)], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
                print("loading player 1 hp: " + dataParty.getPartyMember(dataParty.getPartyIndex(0)).hp);
                player.GetComponent<BattleUnitHealth>().health = dataParty.getPartyMember(dataParty.getPartyIndex(0)).hp;
                player.GetComponent<BattleUnitHealth>().maxhealth = dataParty.getPartyMember(dataParty.getPartyIndex(0)).maxHp;
                player.name = player.name.Remove(player.name.Length - "(Clone)".Length);
            }
            else
                player = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
            if (dataParty.getPartyIndex(1) != -1)
            {
                player2 = Instantiate<GameObject>(playerPrefabs[dataParty.getPartyIndex(1)], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
                player2.GetComponent<BattleUnitHealth>().health = dataParty.getPartyMember(dataParty.getPartyIndex(1)).hp;
                player2.GetComponent<BattleUnitHealth>().maxhealth = dataParty.getPartyMember(dataParty.getPartyIndex(1)).maxHp;
                player2.name = player2.name.Remove(player2.name.Length - "(Clone)".Length);
            }
            else
                player2 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
            if (dataParty.getPartyIndex(2) != -1)
            {
                player3 = Instantiate<GameObject>(playerPrefabs[dataParty.getPartyIndex(2)], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
                player3.GetComponent<BattleUnitHealth>().health = dataParty.getPartyMember(dataParty.getPartyIndex(2)).hp;
                player3.GetComponent<BattleUnitHealth>().maxhealth = dataParty.getPartyMember(dataParty.getPartyIndex(2)).maxHp;
                player3.name = player3.name.Remove(player3.name.Length - "(Clone)".Length);
            }
            else
                player3 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
            if (dataParty.getPartyIndex(3) != -1)
            {
                player4 = Instantiate<GameObject>(playerPrefabs[dataParty.getPartyIndex(3)], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
                player4.GetComponent<BattleUnitHealth>().health = dataParty.getPartyMember(dataParty.getPartyIndex(3)).hp;
                player4.GetComponent<BattleUnitHealth>().maxhealth = dataParty.getPartyMember(dataParty.getPartyIndex(3)).maxHp;
                player4.name = player4.name.Remove(player4.name.Length - "(Clone)".Length);
            }
            else
                player4 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Players").transform);
            
            for (int i = 0; i< dataCombat.getEnemies().Count; i++)
            {
                switch (i)
                {
                    case 0:
                        enemy1 = Instantiate(dataCombat.getEnemies()[i].enemyPrefab, enemySpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 1:
                        enemy2 = Instantiate(dataCombat.getEnemies()[i].enemyPrefab, enemySpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 2:
                        enemy3 = Instantiate(dataCombat.getEnemies()[i].enemyPrefab, enemySpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 3:
                        enemy4 = Instantiate(dataCombat.getEnemies()[i].enemyPrefab, enemySpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                }
            }
            for (int i = 3; i > dataCombat.getEnemies().Count-1; i--)
            {
                switch (i)
                {
                    case 0:
                        enemy1 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 1:
                        enemy2 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 2:
                        enemy3 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                    case 3:
                        enemy4 = Instantiate<GameObject>(playerPrefabs[playerPrefabs.Length - 1], playerSpawn, Quaternion.identity, GameObject.Find("Enemies").transform);
                        break;
                }
            }
        }
    }
    public void updateLayer()
    {
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
        }
    }
    public void startUp()
    {
        print("start battle");
        
        unitList[0] = player;
        unitList[1] = player2;
        unitList[2] = player3;
        unitList[3] = player4;
        unitList[4] = enemy1;
        unitList[5] = enemy2;
        unitList[6] = enemy3;
        unitList[7] = enemy4;

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
