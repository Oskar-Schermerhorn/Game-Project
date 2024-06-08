using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected ObjectLocator locator;
    [SerializeField] public int position = 4;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
    }
    public virtual void moveToNext()
    {
        this.gameObject.transform.position = locator.locateObject(position).transform.position;
    }
    public virtual void moveToPrev()
    {

    }
}
