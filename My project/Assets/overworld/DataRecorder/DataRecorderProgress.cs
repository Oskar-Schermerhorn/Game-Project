using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class DataRecorderProgress : MonoBehaviour
{
    [Serializable]
    public class DataObject
    {
        public string GOname;
        public GameObject GO;
        public bool spawned;
        public bool used;
        public DataObject(GameObject GameObj, bool Spawned, bool Used)
        {
            GO = GameObj;
            GOname = GO.name;
            spawned = Spawned;
            used = Used;
        }
    }
    public int coins { get; private set; } = 100;
    public Vector2 recentSave { get; private set; }
    DataRecorderObjectPermanence data;
    [SerializeField] List<DataObject> progressTracker = new List<DataObject>();
    private void Awake()
    {
        data = this.gameObject.GetComponent<DataRecorderObjectPermanence>();
    }
    public void SetSavePosition(GameObject save)
    {
        recentSave = save.transform.position;
    }
    public List<DataObject> GetProgressTracker()
    {
        foreach ( DataObject dataO in progressTracker){
            if(dataO.GO == null)
            {
                if (GameObject.Find(dataO.GOname) != null)
                {
                    dataO.GO = GameObject.Find(dataO.GOname);
                    if (!dataO.GO.activeSelf && !dataO.used)
                    {
                        dataO.GO.SetActive(dataO.spawned);
                    }
                }
                else if(GameObject.FindObjectsOfType<OneTimeUse>(true).Length > 0) 
                { 
                    for(int i =0; i< GameObject.FindObjectsOfType<OneTimeUse>(true).Length; i++)
                    {
                        GameObject trackerHolder = GameObject.FindObjectsOfType<OneTimeUse>(true)[i].gameObject;
                        print(trackerHolder.name);
                        if (trackerHolder.name == dataO.GOname)
                        {
                            dataO.GO = trackerHolder;
                            if (!dataO.GO.activeSelf && !dataO.used)
                            {
                                dataO.GO.SetActive(dataO.spawned);
                            }
                        }
                    }
                }
                    
            }
        }
        return progressTracker;
    }
    public void SetProgressTracker(GameObject GO, bool Spawned, bool Used)
    {
        bool found = false;
        for(int i = 0; i<progressTracker.Count; i++)
        {
            if(progressTracker[i].GO == GO)
            {
                progressTracker[i].used = Used;
                progressTracker[i].spawned = Spawned;
                found = true;
                break;
            }
        }
        if (!found)
        {
            progressTracker.Add(new DataObject(GO, Spawned, Used));
        }
    }
    public void AquireCoins(int amount)
    {
        coins += amount;
    }
    public void SpendCoins(int amount)
    {
        coins -= amount;
    }
    public void Reset()
    {
        data.savePosition(recentSave);
    }
}
