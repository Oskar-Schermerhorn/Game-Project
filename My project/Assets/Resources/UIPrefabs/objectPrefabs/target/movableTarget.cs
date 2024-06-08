using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableTarget : Target
{
    [SerializeField] int positionIndex;
    [SerializeField] public List<int> possiblePositions;
    public override void moveToNext()
    {
        positionIndex++;
        if (positionIndex >= possiblePositions.Count)
        {
            positionIndex = 0;
        }
        position = possiblePositions[positionIndex];
        this.gameObject.transform.position = locator.locateObject(possiblePositions[positionIndex]).transform.position;
    }
    public override void moveToPrev()
    {
        positionIndex--;
        if (positionIndex <0 )
        {
            positionIndex = possiblePositions.Count-1;
        }
        position = possiblePositions[positionIndex];
        this.gameObject.transform.position = locator.locateObject(possiblePositions[positionIndex]).transform.position;
    }
}
