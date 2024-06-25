using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitAttackOffset : MonoBehaviour
{
    Vector2[] pos = new Vector2[8];
    int tempLayer;
    ObjectLocator locator;
    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
    }
    private void Start()
    {
        tempLayer = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }
    protected void changeLayer(int layer)
    {
        //inputted to change to -1 layer
        if (layer == -1)
        {
            //reset to temp
            GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
        }
        else
        {
            //set temp and change
            tempLayer = GetComponent<SpriteRenderer>().sortingOrder;
            GetComponent<SpriteRenderer>().sortingOrder = layer;
        }
    }
    public void handleOffset(move selectedMove, List<int> targets)
    {
        GameObject target = locator.locateObject(targets[0]);
        //float offset = -1.36f;
        //if (selectedMove.moveTargetType == moveTargets.SELF)
        //    offset = 0f;
        //if (selectedMove.moveTargetType == moveTargets.ALLY)
        //    offset *= -1;
        //pos = transform.position;
        //if (locator.locateObject(this.gameObject) >= 4)
        //{
        //    offset *= -1;
        //}
        //if (offset != 0)
        //{
            //transform.position = new Vector2(target.transform.position.x + offset, target.transform.position.y);
        //}

        //turn off everything
        //set to first frame animations in higher res
        //wait a frame
        //turn on needed 
        //play
        //turn off
        //reset to smol mode
        //reset positions
        //wait a frame
        //turn on all
        

        changeLayer(target.GetComponent<SpriteRenderer>().sortingOrder + 1);
        StartCoroutine(ChangeToAttackMode(selectedMove, targets));
    }

    IEnumerator ChangeToAttackMode(move selectedMove, List<int> targets)
    {
        print("test");
        //this.gameObject.GetComponent<Animator>().Play(selectedMove.animationNames[0]);
        this.gameObject.GetComponent<Animator>().speed = 0;
        for(int i = 0; i< locator.numObjects(); i++)
        {
            pos[i] = locator.locateObject(i).transform.position;
            
            locator.locateObject(i).GetComponent<SpriteRenderer>().enabled = false;
            if(locator.locateObject(i).GetComponent<Animator>() != null)
            {
                locator.locateObject(i).GetComponent<Animator>().enabled = false;
            }
        }
        if(!selectedMove.HasProperty(targetProperties.SELF))
        {
            transform.position = new Vector2(0, -0.58f);
            int set = 0;
            if (locator.locateObject(this.gameObject) < 4)
            {
                set = 4;
            }
            for (int i = 0; i < targets.Count; i++)
            {
                //place the enemies correctly

                locator.locateObject(targets[i]).transform.position = pos[set + i];
                /*if(selectedMove.targetType == targetType.PAIRS)
                {
                    if(i == 1 && targets[1] != targets[0] + 1)
                    {
                        int difference = targets[1] -targets[0]-1;
                        locator.locateObject(targets[i]).transform.position = pos[set + i + difference];
                    }
                }*/
                //locator.locateObject(targets[i]).GetComponent<SpriteRenderer>().enabled = f;
            }
        }
        
        
        yield return null;
        //print(this.name);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<Animator>().enabled = true;
        if (!selectedMove.HasProperty(targetProperties.SELF))
        {
            for (int i = 0; i < targets.Count; i++)
            {
                //print(locator.locateObject(targets[i]).name);
                locator.locateObject(targets[i]).GetComponent<SpriteRenderer>().enabled = true;
                
                if (targets[i] != locator.locateObject(this.gameObject)
                    && locator.locateObject(targets[i]).GetComponent<BattleUnitAnimate>() != null)
                {
                    locator.locateObject(targets[i]).GetComponent<Animator>().enabled = true;
                    locator.locateObject(targets[i]).GetComponent<BattleUnitAnimate>().changeAnimation("neutral64");
                }
                
            }
        }
        //this.gameObject.GetComponent<Animator>().Play(selectedMove.animationNames[0]);
        
        this.gameObject.GetComponent<Animator>().speed = 1;
    }

    public void returnTo()
    {
        //if(transform.position != Vector3.down)
        //{
        //    transform.position = pos;

        //}
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tempLayer;
        for (int i = 0; i < locator.numObjects(); i++)
        {
            locator.locateObject(i).GetComponent<SpriteRenderer>().enabled = true;
            locator.locateObject(i).transform.position = pos[i];
            if(locator.locateObject(i).GetComponent<Animator>() != null)
            {
                locator.locateObject(i).GetComponent<Animator>().enabled = true;
                locator.locateObject(i).GetComponent<BattleUnitAnimate>().changeAnimation("neutral");
            }
            
        }
    }
}
