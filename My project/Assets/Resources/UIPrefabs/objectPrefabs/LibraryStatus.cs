using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryStatus : MonoBehaviour
{
    public Dictionary<string, statusEffect> statusDictionary = new Dictionary<string, statusEffect> { };
    public statusEffect[] allStatuses;
    private void Awake()
    {
        allStatuses = Resources.LoadAll<statusEffect>("statusEffects");
        for (int i = 0; i < allStatuses.Length; i++)
        {
            statusDictionary.Add(allStatuses[i].effectName, allStatuses[i]);
        }
    }
}
