using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitAttackOffset : MonoBehaviour
{
    Vector2 mypos = new Vector2();
    int tempLayer;
    ObjectLocator locator;
    MoveCoroutine movement;
    SpawnScript spawn;

    int playercount = 0;
    int enemycount = 0;
    List<GameObject> objects;
    List<Vector2> positions;
    List<int> savedTargets;

    private void Awake()
    {
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        movement = GameObject.Find("BattleHandler").GetComponent<MoveCoroutine>();
        spawn = GameObject.Find("BattleHandler").GetComponent<SpawnScript>();
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
        this.gameObject.GetComponent<Animator>().speed = 0;
        for (int i = 0; i < locator.numObjects(); i++)
        {

            locator.locateObject(i).GetComponent<SpriteRenderer>().enabled = false;
            if (locator.locateObject(i).GetComponent<Animator>() != null)
            {
                locator.locateObject(i).GetComponent<Animator>().enabled = false;
            }
        }


        savedTargets = targets;
        createAttackMovementLists(targets, ref playercount, ref enemycount, out objects, out positions);

        yield return null;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<Animator>().enabled = true;
        for (int i = 0; i < targets.Count; i++)
        {
            locator.locateObject(targets[i]).GetComponent<SpriteRenderer>().enabled = true;

            if (targets[i] != locator.locateObject(this.gameObject)
                && locator.locateObject(targets[i]).GetComponent<BattleUnitAnimate>() != null)
            {
                locator.locateObject(targets[i]).GetComponent<Animator>().enabled = true;
            }

        }

        // stall until movement is finished
        movement.Move(objects, positions, 3);
        yield return movement.coroutine;

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].GetComponent<BattleUnitAnimate>() != null && objects[i] != this.gameObject)
            {
                objects[i].GetComponent<BattleUnitAnimate>().changeAnimation("neutral64");
            }

        }
        yield return null;

        this.gameObject.GetComponent<Animator>().speed = 1;
        this.transform.position = new Vector2(0, -0.58f);
        this.gameObject.GetComponent<BattleUnitAnimate>().changeAnimation(selectedMove.Name);
    }

    private void createAttackMovementLists(List<int> targets, ref int playercount, ref int enemycount, out List<GameObject> objects, out List<Vector2> positions)
    {
        objects = new List<GameObject>();
        positions = new List<Vector2>();
        playercount = 0;
        enemycount = 0;

        objects.Add(this.gameObject);
        if (this.gameObject.GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
        {
            positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position0").transform.position);
            playercount++;
        }
        else if (this.gameObject.GetComponent<BattleUnitID>().UnitSide == side.ENEMY)
        {
            positions.Add(GameObject.Find("BattleHandler/Positions/EnemyPositions/position").transform.position);
            enemycount++;
        }


        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != locator.locateObject(this.gameObject))
            {
                if (locator.locateObject(targets[i]).GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
                {
                    objects.Add(locator.locateObject(targets[i]));
                    positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position0").transform.position - (spawn.enemyOffset * playercount));
                    playercount++;
                }
                else if (locator.locateObject(targets[i]).GetComponent<BattleUnitID>().UnitSide == side.ENEMY)
                {
                    objects.Add(locator.locateObject(targets[i]));
                    positions.Add(GameObject.Find("BattleHandler/Positions/EnemyPositions/position").transform.position + (spawn.enemyOffset * enemycount));
                    enemycount++;
                }
            }
        }
    }

    private void createReturnMovementLists(List<int> targets, ref int playercount, ref int enemycount, out List<GameObject> objects, out List<Vector2> positions)
    {
        objects = new List<GameObject>();
        positions = new List<Vector2>();

        objects.Add(this.gameObject);
        int index = locator.locateObject(this.gameObject);
        if (this.gameObject.GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
        {
            positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + index).transform.position);
        }
        else if (this.gameObject.GetComponent<BattleUnitID>().UnitSide == side.ENEMY)
        {
            index -= locator.getAll(true).Count;
            positions.Add(GameObject.Find("BattleHandler/Positions/EnemyPositions/position").transform.position + (spawn.enemyOffset * index));
        }


        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != locator.locateObject(this.gameObject))
            {
                if (locator.locateObject(targets[i]).GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
                {
                    objects.Add(locator.locateObject(targets[i]));
                    positions.Add(GameObject.Find("BattleHandler/Positions/PlayerPositions/position" + targets[i]).transform.position);
                }
                else if (locator.locateObject(targets[i]).GetComponent<BattleUnitID>().UnitSide == side.ENEMY)
                {
                    objects.Add(locator.locateObject(targets[i]));
                    positions.Add(GameObject.Find("BattleHandler/Positions/EnemyPositions/position").transform.position + (spawn.enemyOffset * (targets[i] - locator.getAll(true).Count)));
                }
            }
        }
    }

    public void returnTo(bool end)
    {
        StartCoroutine(ReturnCoroutine(end));
    }

    IEnumerator ReturnCoroutine(bool end)
    {
        for (int i = 0; i < locator.numObjects(); i++)
        {

            locator.locateObject(i).GetComponent<SpriteRenderer>().enabled = true;

            if (locator.locateObject(i).GetComponent<BattleUnitAnimate>() != null)
            {
                locator.locateObject(i).GetComponent<Animator>().enabled = true;
                locator.locateObject(i).GetComponent<BattleUnitAnimate>().changeAnimation("neutral");
            }
        }

        createReturnMovementLists(savedTargets, ref playercount, ref enemycount, out objects, out positions);
        movement.Move(objects, positions, 3);
        yield return movement.coroutine;
        this.gameObject.GetComponent<BattleUnitFinish>().endOfAction(end);
    }

}
