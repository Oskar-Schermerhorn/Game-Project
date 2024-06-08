using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitAnimate : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected string currentState = "neutral";
    protected turnManagement turn;
    protected ObjectLocator locator;
    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        ActionCommandHandler.ResumeAnimation += resumeAnimation;
        BattleUnitHealth.PlayHitAnim += PlayHit;
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
    }
    public void changeAnimation(string newState)
    {
        if (currentState != newState)
        {
            anim.Play(newState);
            currentState = newState;
        }
    }
    public void resetAnimation(string newState)
    {
        anim.Play(newState);
        currentState = newState;
    }
    protected void pauseForActionCommandEvent()
    {
        anim.speed = 0;
    }
    public void resumeAnimation()
    {
        anim.speed = 1;
    }
    public void PlayHit(GameObject hit)
    {
        if(hit == this.gameObject && (turn.turnNum != locator.locateObject(hit)))
        {
            anim.Play("Hit");
        }
    }
    private void OnDisable()
    {
        ActionCommandHandler.ResumeAnimation -= resumeAnimation;
        BattleUnitHealth.PlayHitAnim -= PlayHit;
    }
}
