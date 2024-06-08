using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeUse : MonoBehaviour
{
    DataRecorderProgress data;
    private void Awake()
    {
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
    }
    private void OnEnable()
    {
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        bool found = false;
        for (int i = 0; i < data.GetProgressTracker().Count; i++)
        {
            if (data.GetProgressTracker()[i].GO == this.gameObject)
            {
                //print("found progress" + this.gameObject.name);
                if (data.GetProgressTracker()[i].spawned)
                    this.gameObject.SetActive(!data.GetProgressTracker()[i].used);
                else
                    this.gameObject.SetActive(!data.GetProgressTracker()[i].spawned);
                found = true;
                break;
            }
        }
        if (!found)
        {
            //print("create progress tracker" + this.gameObject.name);
            data.SetProgressTracker(this.gameObject, true, false);
        }
    }
    public void Use()
    {
        //print("one time used");
        if(data == null)
        {
            data = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
        }
        data.SetProgressTracker(this.gameObject, true, true);
    }
}
