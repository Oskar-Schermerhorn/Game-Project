using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveCoroutine : MonoBehaviour
{
    public Coroutine coroutine;
    public static event Action<bool> spinComplete;
    public static event Action StartedSpin;
    ObjectLocator locator;
    SpinHandler spin;
    private void Awake()
    {
        locator = gameObject.GetComponent<ObjectLocator>();
        spin = gameObject.GetComponent<SpinHandler>();
    }
    public void Move(List<GameObject> objects, Vector2[] destinations)
    {
        List<Vector2> destinationList = new List<Vector2>();
        destinationList.AddRange(destinations);
        coroutine = StartCoroutine(MoveToTarget(objects, destinationList, true));

    }
    public void Move(List<GameObject> objects, List<Vector2> destinations, bool left)
    {
        coroutine = StartCoroutine(MoveToTarget(objects, destinations, left));
    }

    IEnumerator MoveToTarget(List<GameObject> objects, List<Vector2> destinations, bool left)
    {
        StartedSpin();
        float elapsedTime = 0;
        List<Vector2> startingPositions = new List<Vector2>();
        for(int i = 0; i< objects.Count; i++)
        {
            startingPositions.Add(objects[i].transform.position);
        }
        while (elapsedTime < 1)
        {
            elapsedTime += 3*Time.deltaTime;
            if (elapsedTime > 1)
                elapsedTime = 1;
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = Vector2.Lerp(startingPositions[i], destinations[i], elapsedTime);
            }
            yield return null;

        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = destinations[i];
        }
        print("set");
        
        if (spin.checkSpin())
        {
            print("spin again");
            if(left)
                spin.spin(objects[0]);
            else
                spin.reverseSpin(objects[0]);
        }
        else
        {
            spinComplete(true);
            print(true);
            yield return null;
            spinComplete(false);
            print("complete");
        }
    }
}
