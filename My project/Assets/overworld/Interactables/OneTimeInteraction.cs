using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeInteraction : MonoBehaviour
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
                print("found progress" + this.gameObject.name);
                if (data.GetProgressTracker()[i].spawned)
                    this.gameObject.GetComponent<Interactable>().enabled = !data.GetProgressTracker()[i].used;
                else
                    this.gameObject.GetComponent<Interactable>().enabled = !data.GetProgressTracker()[i].spawned;
                found = true;
                break;
            }
        }
        if (!found)
        {
            print("create progress tracker" + this.gameObject.name);
            data.SetProgressTracker(this.gameObject, true, false);
        }
    }
}
