using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class overworldEnemyBehavior : MonoBehaviour
{
    public enum behavior { STAND, ATTACK, PATROL, JUMP, LUNGE, RETURN };
    [SerializeField] private behavior enemyBehavior;
    [SerializeField] public behavior currentBehavior { get; private set; }
    private GameObject player;
    [SerializeField] private float attentionDistance;
    [SerializeField] private float detectRange;
    [SerializeField] private float speed;
    [SerializeField] private bool activeBehavior;
    [SerializeField] private bool detected;
    overworldEnemyAnimate anim;
    [SerializeField] public Vector3 originalPos { get; private set; }
    Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions { get; private set; } = new List<RaycastHit2D>();
    public float collisionOffset { get; private set; } = 0.05f;
    private void Awake()
    {
        Rooms.ActiveRoom += checkActiveRoom;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        originalPos = new Vector2(Mathf.RoundToInt(rb.position.x * 64) / 64f, Mathf.RoundToInt(rb.position.y * 64) / 64f);
    }
    private void OnEnable()
    {
        Rooms.ActiveRoom += checkActiveRoom;
    }
    void Start()
    {
        player = GameObject.Find("Player");
        
        anim = this.gameObject.GetComponent<overworldEnemyAnimate>();
        currentBehavior = enemyBehavior;
    }
    public void ActivateBehavior()
    {
        activeBehavior = true;
    }
    public void SetBehaviorAttack()
    {
        enemyBehavior = behavior.ATTACK;
        currentBehavior = behavior.ATTACK;
    }
    void checkActiveRoom(bool active, GameObject room)
    {
        if (transform.IsChildOf(room.transform))
        {
            activeBehavior = active;
            if (!active && currentBehavior != behavior.PATROL && gameObject.activeSelf)
            {
                StartCoroutine(stopPersuit());
            }
        }
    }

    //change this to Action based on player move
    void FixedUpdate()
    {
        if (activeBehavior)
        {
            switch (currentBehavior)
            {
                case behavior.ATTACK:
                    if (Vector2.Distance(player.transform.position, this.transform.position) < detectRange 
                        && allowMovement((player.transform.position - this.transform.position).normalized)
                        && Vector2.Distance(this.transform.position, originalPos) < attentionDistance)
                    {
                        detected = true;
                        rb.velocity = speed * (new Vector2(player.transform.position.x, player.transform.position.y) 
                            -new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
                        anim.changeAnimation(rb.velocity.normalized);
                        rb.position = new Vector2(Mathf.RoundToInt(rb.position.x * 64) / 64f, Mathf.RoundToInt(rb.position.y * 64) / 64f);
                    }
                    else if(detected)
                    {
                        detected = false;
                        //print("stop");
                        StartCoroutine(stopPersuit());
                    }
                    break;
                case behavior.PATROL:
                    if(detected == false)
                    {
                        StartCoroutine(FollowPath(speed));
                        detected = true;
                    }
                    break;
                case behavior.JUMP:
                    if(this.gameObject.GetComponent<LineRenderer>() != null)
                    {
                        if(Vector2.Distance(player.transform.position, this.gameObject.GetComponent<LineRenderer>().GetPosition(this.gameObject.GetComponent<LineRenderer>().positionCount-1) + originalPos) < detectRange
                            && detected == false)
                        {
                            detected = true;
                            StartCoroutine(FollowPath(speed * 1.5f));
                        }
                    }
                    break;
            }
        }
    }

    IEnumerator FollowPath(float PathSpeed)
    {
        LineRenderer line = this.gameObject.GetComponent<LineRenderer>();
        for (int i = 0; i < line.positionCount-1; i++)
        {
            yield return null;
            while(Vector2.Distance(rb.position, line.GetPosition(i+1) + originalPos) > 0.1f)
            {
                rb.velocity = (line.GetPosition(i + 1) - line.GetPosition(i)).normalized * PathSpeed;
                anim.changeAnimation(rb.velocity.normalized);
                yield return null;
            }
        }
        rb.velocity = Vector2.zero;
        anim.changeAnimation(Vector2.zero);
        if (currentBehavior == behavior.JUMP)
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(stopPersuit());
        }
        else if(currentBehavior == behavior.PATROL)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 temp;
            //reverse the line
            for (int i = 0; i < line.positionCount/2; i++) 
            {
                temp = line.GetPosition(i);
                line.SetPosition(i, line.GetPosition(line.positionCount-i-1));
                line.SetPosition(line.positionCount-i-1, temp);
            }
            detected = false;
        }
        
    }
    IEnumerator stopPersuit()
    {
        rb.velocity = Vector2.zero;
        anim.changeAnimation(Vector2.zero);
        yield return new WaitForSeconds(0.5f);
        //print("returning");
        currentBehavior = behavior.RETURN;
        while(Vector2.Distance(this.transform.position, originalPos) > 0.02f)
        {
            if (allowMovement((originalPos - this.transform.position).normalized))
            {
                rb.velocity = speed * (originalPos - this.transform.position).normalized;
                anim.changeAnimation(rb.velocity.normalized);
                rb.position = new Vector2(Mathf.RoundToInt(rb.position.x * 64) / 64f, Mathf.RoundToInt(rb.position.y * 64) / 64f);
            }
            else
            {
                break;
            }
            yield return null;
        }
        rb.velocity = Vector2.zero;
        anim.changeAnimation(Vector2.zero);
        currentBehavior = enemyBehavior;
        detected = false;
    }

    bool allowMovement(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.deltaTime + collisionOffset);
        if (count > 0)
        {
            return false;
        }
        return true;
    }
    private void OnDisable()
    {
        Rooms.ActiveRoom -= checkActiveRoom;
    }
    private void OnDestroy()
    {
        Rooms.ActiveRoom -= checkActiveRoom;
    }
}
