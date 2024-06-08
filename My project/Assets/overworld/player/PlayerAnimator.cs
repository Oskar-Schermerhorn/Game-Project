using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]public string currentState { get; private set; }
    Animator anim;
    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        MoveScript.Moving += updateAnimation;
        MoveScript.Stop += Stop;
    }
    private void updateAnimation(Vector2 direction, Vector2 velocity)
    {
        const string faceR = "StandRight";
        const string faceL = "StandLeft";
        const string faceU = "StandUp";
        const string faceD = "StandDown";
        const string right = "Right";
        const string left = "Left";
        const string up = "Up";
        const string down = "Down";
        string state = "";
        if (direction.x > 0.5 && velocity.x > 0)
        {
            state = right;
        }
        else if (direction.x < -0.5 && velocity.x < 0)
        {
            state = left;
        }
        else if (direction.y > 0.5 && velocity.y > 0)
        {
            state = up;
        }
        else if (direction.y < -0.5 && velocity.y < 0)
        {
            state = down;
        }
        else
        {
            if (currentState == right)
            {
                state = faceR;
            }
            else if (currentState == left)
            {
                state = faceL;
            }
            else if (currentState == up)
            {
                state = faceU;
            }
            else if (currentState == down)
            {
                state = faceD;
            }

        }
        changeAnimation(state);
    }

    private void changeAnimation(string newState)
    {
        if (currentState != newState && newState != "")
        {
            anim.Play(newState);
            currentState = newState;
        }
    }
    void Stop()
    {
        updateAnimation(Vector2.zero, Vector2.zero);
    }
    private void OnDisable()
    {
        MoveScript.Moving -= updateAnimation;
        MoveScript.Stop -= Stop;
    }
}
