using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderInteractables : MonoBehaviour
{
    [SerializeField] private List<string> interactableList = new List<string>();
    public void useInteractable(string id)
    {
        interactableList.Add(id);
    }
    public bool checkUsedInteractable(string id)
    {
        for (int i = 0; i < interactableList.Count; i++)
        {
            if (interactableList[i] == id)
            {
                return true;
            }
        }
        return false;
    }
}
