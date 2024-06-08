using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnvironment : MonoBehaviour
{
    [SerializeField] Sprite NewEnvironment;
    [SerializeField] GameObject firstSet;
    [SerializeField] GameObject secondSet;
    DataRecorderProgress data;
    private void Awake()
    {
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        CheckData();
    }
    void CheckData()
    {
        for(int i = 0; i< data.GetProgressTracker().Count; i++)
        {
            if(data.GetProgressTracker()[i].GO == this.gameObject)
            {
                Change();
            }
        }
    }
    public void Change()
    {
        GetComponent<SpriteRenderer>().sprite = NewEnvironment;
        firstSet.SetActive(false);
        secondSet.SetActive(true);
        data.SetProgressTracker(this.gameObject, true, true);
    }
}
