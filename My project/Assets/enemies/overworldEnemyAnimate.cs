using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworldEnemyAnimate : MonoBehaviour
{
    string currentState = "neutral";
    Rigidbody2D rb;
    Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void changeAnimation(Vector2 direction)
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
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                state = right;
            }
            else
            {
                state = left;
            }
        }
        else if(direction.y != 0f) 
        {
            if (direction.y > 0)
            {
                state = up;
            }
            else
            {
                state = down;
            }
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
}
