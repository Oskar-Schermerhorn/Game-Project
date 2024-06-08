using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class targetController : MonoBehaviour
{
    [SerializeField] targetInput input;
    [SerializeField] public List<GameObject> activeTargets;
    public static event Action<List<int>> confirmedTarget;
    private void Awake()
    {
        input = this.gameObject.GetComponent<targetInput>();
        targetCreation.Targeting += setActive;
        targetInput.Aim += MoveTargets;
        targetInput.Confirm += Confirm;
        targetInput.Cancel += Cancel;
    }
    private void MoveTargets(float direction)
    {
        if (direction < 0)
        {
            for (int i = 0; i < activeTargets.Count; i++)
            {
                activeTargets[i].GetComponent<Target>().moveToNext();
            }
        }
        else
        {
            for (int i = 0; i < activeTargets.Count; i++)
            {
                activeTargets[i].GetComponent<Target>().moveToPrev();
            }
        }
        
    }
    private void setActive()
    {
        activeTargets.Clear();
        for(int i = 0; i < GetComponentsInChildren<Target>().Length; i++)
        {
            activeTargets.Add(GetComponentsInChildren<Target>()[i].gameObject);
        }
    }
    private void Confirm()
    {
        List<int> selected = new List<int>();
        for(int i = 0; i < activeTargets.Count; i++)
        {
            selected.Add(activeTargets[i].GetComponent<Target>().position);
        }
        confirmedTarget(selected);
        Cancel();
        
    }
    private void Cancel()
    {
        for(int i = 0; i<activeTargets.Count; i++)
        {
            Destroy(activeTargets[i]);
        }
        activeTargets.Clear();
        input.disableControls();
    }
    private void OnDisable()
    {
        targetInput.Aim -= MoveTargets;
        targetCreation.Targeting -= setActive;
        targetInput.Confirm -= Confirm;
        targetInput.Cancel -= Cancel;
    }
}
