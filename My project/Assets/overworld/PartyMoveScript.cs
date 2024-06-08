using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;

public class PartyMoveScript : MonoBehaviour
{
    class movementData
    {
        public Vector2 pos {get; private set;}
        public Vector2 vel { get; private set; }
        public movementData(Vector2 velocity, Vector2 position)
        {
            pos = position;
            vel = velocity;
        }
    }
    private DataRecorderParty data;
    public Rigidbody2D follow;
    private Rigidbody2D rb;
    private Queue<movementData> movements;
    [SerializeField] private int order;
    [SerializeField] private RuntimeAnimatorController[] controllers;
    private Animator anim;
    private string currentState = "";
    void Start()
    {
        data = GameObject.Find("DataRecorder").GetComponent<DataRecorderParty>();
        follow = transform.parent.GetComponentInParent<Rigidbody2D>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        movements = new Queue<movementData>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (follow.velocity != Vector2.zero)
            movements.Enqueue(new movementData(follow.velocity, follow.gameObject.transform.position));
        else
            rb.velocity = Vector2.zero;
        if (movements.Count >= 10)
            move();
        updateAnimation();
        if(rb.position.y > follow.position.y)
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder -1;
        else if (rb.position.y < follow.position.y)
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder +1;
        else
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder-1;
        checkWhom();
    }
    void move()
    {
        if(follow.velocity != Vector2.zero)
        {
            movementData nextMove = movements.Dequeue();
            rb.velocity = nextMove.vel;
            rb.position = nextMove.pos;
            rb.position = new Vector2(Mathf.RoundToInt(rb.position.x * 64) / 64f, Mathf.RoundToInt(rb.position.y * 64) / 64f);
        }
            
    }
    public void clearMovements()
    {
        movements.Clear();
        rb.position = follow.position;
    }
    private void updateAnimation()
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
        if (rb.velocity.x > 0.5)
        {
            state = right;
            
        }
        else if (rb.velocity.x < -0.5)
        {
            state = left;
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
        else if (rb.velocity.y > 0.5)
        {
            state = up;
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else if (rb.velocity.y < -0.5)
        {
            state = down;
            GetComponent<SpriteRenderer>().sortingOrder = follow.gameObject.GetComponent<SpriteRenderer>().sortingOrder -1;
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

    public void changeAnimation(string newState)
    {
        if (currentState != newState)
        {
            anim.Play(newState);
            currentState = newState;
        }
    }

    private void checkWhom()
    {
        int[] sequence = { 0, 1, 3, 2 };
        int locatePrince = data.checkCharacterValid(0);
        int temp = (order + sequence[locatePrince]) % 4;
        if(data.getPartyIndex(sequence[temp]) != -1)
        {
            anim.runtimeAnimatorController = controllers[data.getPartyIndex(sequence[temp])];
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
