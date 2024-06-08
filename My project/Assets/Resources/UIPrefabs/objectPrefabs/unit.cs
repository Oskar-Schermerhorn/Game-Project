using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit
{
    private GameObject unitGO;
    private Vector2 gamePosition;
    private int range;
    private List<int> rangeList = new List<int> { };
    private bool alive { get; set; }
    private bool playerTeam;

    public GameObject getUnitGO()
    {
        return unitGO;
    }
    public void setUnitGO(GameObject g)
    {
        unitGO = g;
    }
    public Vector2 getPosition()
    {
        return gamePosition;
    }
    public void setPosition(Vector2 newPos)
    {
        gamePosition = newPos;
    }
    public void setRange(int r)
    {
        range = r;
        rangeList.Clear();
        switch (range)
        {
            case 0:
                rangeList.Add(4);
                rangeList.Add(5);
                rangeList.Add(6);
                break;
            case 1:
                rangeList.Add(4);
                rangeList.Add(5);
                break;
            case 2:
                rangeList.Add(4);
                rangeList.Add(6);
                break;
            case 3:
                rangeList.Add(4);
                break;
            case 4:
                rangeList.Add(0);
                rangeList.Add(1);
                rangeList.Add(2);
                break;
            case 5:
                rangeList.Add(0);
                rangeList.Add(1);
                break;
            case 6:
                rangeList.Add(0);
                rangeList.Add(2);
                break;
            case 7:
                rangeList.Add(0);
                break;
        }
    }
    public List<int> getRangeList()
    {
        return rangeList;
    }
    public bool getAlive()
    {
        return alive;
    }
    public void setAlive(bool isAlive)
    {
        alive = isAlive;
    }
    public unit()
    {
        alive = false;
    }
    public unit(GameObject g)
    {
        if (g != null)
        {
            setUnitGO(g);
            alive = !g.tag.Equals("dontUse");
        }
        else
        {
            alive = false;
        }
    }
}
