using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardHandler : MonoBehaviour
{
    SpawnScript spawn;
    turnManagement turn;
    [SerializeField] GameObject hazardIndicator;
    public List<GameObject> activeHazards = new List<GameObject>();
    hazardIndicatorScript currentHazard;
    bool animating = false;
    private void Start()
    {
        spawn = GameObject.FindWithTag("BattleHandler").GetComponent<SpawnScript>();
        turn = GameObject.FindWithTag("BattleHandler").GetComponent<turnManagement>();
    }

    public void createHazard(hazard newHazard, int position)
    {
        print(newHazard);
        GameObject h = Instantiate<GameObject>(hazardIndicator, this.transform);
        activeHazards.Add(h);
        //h.transform.position = spawn.getPosition(position);
        h.GetComponent<hazardIndicatorScript>().haz = newHazard;
        h.GetComponent<hazardIndicatorScript>().turns = newHazard.turns;
        h.GetComponent<hazardIndicatorScript>().position = position;
        h.GetComponent<hazardIndicatorScript>().updateColor();
    }
    public void updateHazards()
    {
        animating = false;
        for(int i = 0; i<GetComponentsInChildren<hazardIndicatorScript>().Length; i++)
        {
            currentHazard = GetComponentsInChildren<hazardIndicatorScript>()[i];
            currentHazard.incrementTurns();
            if (currentHazard.turns <= 0)
            {
                detonateHazard();
                animating = true;
            }
        }
        if (!animating)
        {
            turn.finishedHazards();
        }
    }
    private void detonateHazard()
    {
        currentHazard.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        currentHazard.gameObject.transform.localScale = Vector3.one;
        //currentHazard.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spawn.unitList[currentHazard.position].getUnitGO().GetComponent<SpriteRenderer>().sortingOrder + 1;
        currentHazard.gameObject.GetComponent<Animator>().Play(currentHazard.haz.anim);
    }
    public void inflictDamage()
    {
        if(currentHazard.position < 4)
        {
            //spawn.unitList[currentHazard.position].getUnitGO().GetComponent<battleScript>().takeDamage(currentHazard.haz.damage, false, false);
        }
        else
        {
            //spawn.unitList[currentHazard.position].getUnitGO().GetComponent<enemyTurnScript>().takeDamage(currentHazard.haz.damage, false, false);
        }
    }
    public void hazardHandled()
    {
        if(animating)
            Destroy(currentHazard.gameObject);
        turn.finishedHazards();
    }
}
