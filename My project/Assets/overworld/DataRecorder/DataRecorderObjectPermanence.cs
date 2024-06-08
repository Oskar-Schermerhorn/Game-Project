using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderObjectPermanence : MonoBehaviour
{
    [SerializeField] Vector2 savedPosition;
    [SerializeField] GameObject currentRoom;
    [SerializeField] public string currentRoomName { get; private set; }
    public List<enemyLocationInformation> enemyPositions { get; private set; } = new List<enemyLocationInformation>();
    public class enemyLocationInformation
    {
        public Vector2 pos { get; private set; }
        public Vector2 originalPos { get; private set; }
        public string enemyID { get; private set; }
        public enemyLocationInformation(string EnemyID, Vector2 Position, Vector2 OriginalPosition)
        {
            pos = Position;
            originalPos = OriginalPosition;
            enemyID = EnemyID;
        }
    }
    private void Awake()
    {
        Rooms.ActiveRoom += setCurrentRoom;
    }
    void setCurrentRoom(bool active, GameObject room)
    {
        if (active)
        {
            currentRoom = room;
            currentRoomName = currentRoom.name;
        }
    }
    public void savePosition(Vector2 overworldPosition)
    {
        savedPosition = overworldPosition;
        enemyPositions.Clear();
        if(currentRoom.transform.GetComponentInChildren<overworldEnemyBehavior>() != null)
        {
            for (int i = 0; i < currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>().Length; i++)
            {
                if (Vector2.Distance(currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].transform.gameObject.transform.position,
                    currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].gameObject.GetComponent<overworldEnemyBehavior>().originalPos) > 0.5f
                    && currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].gameObject.GetComponent<overworldEnemyBehavior>().currentBehavior != overworldEnemyBehavior.behavior.PATROL
                    )
                {
                    enemyLocationInformation info =
                        new enemyLocationInformation(currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].gameObject.GetComponent<overworldEnemyDestroy>().id,
                        currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].gameObject.transform.position,
                        currentRoom.transform.GetComponentsInChildren<overworldEnemyBehavior>()[i].gameObject.GetComponent<overworldEnemyBehavior>().originalPos);
                    enemyPositions.Add(info);
                }
            }
        }
        
    }
    public void restorePosition()
    {
        if (savedPosition != Vector2.zero)
        {
            print("restoring position");
            GameObject.Find("Player").transform.position = savedPosition;
        }
        if (currentRoom == null)
        {
            setCurrentRoom(true, GameObject.Find(currentRoomName));
        }
        StartCoroutine(restoreEnemies());
    }
    IEnumerator restoreEnemies()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.1f);
        print("restoring the position of " + enemyPositions.Count + " enemies");
        print(currentRoom.GetComponentsInChildren<overworldEnemyBehavior>(true).Length);
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            for (int j = 0; j < currentRoom.GetComponentsInChildren<overworldEnemyBehavior>(true).Length; j++)
            {
                if (currentRoom.GetComponentsInChildren<overworldEnemyBehavior>(true)[j].GetComponent<overworldEnemyDestroy>().id == enemyPositions[i].enemyID)
                {
                    currentRoom.GetComponentsInChildren<overworldEnemyBehavior>(true)[j].gameObject.transform.position = enemyPositions[i].pos;
                    break;
                }
            }
        }
        enemyPositions.Clear();
    }
    private void OnDisable()
    {
        Rooms.ActiveRoom -= setCurrentRoom;
    }
}
