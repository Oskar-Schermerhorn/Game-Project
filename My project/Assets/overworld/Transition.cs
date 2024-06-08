using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Transition : MonoBehaviour
{
    public static event Action<bool> TransitionInProgress;
    protected bool allowCollision = true;
    private void Awake()
    {
        Transition.TransitionInProgress += SetAllowCollision;
        for (int i = 0; i < this.gameObject.GetComponent<LineRenderer>().positionCount; i++)
        {
            this.gameObject.GetComponent<LineRenderer>().SetPosition(i, new Vector2(Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(i).x * 64) / 64f, Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(i).y * 64) / 64f));
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && allowCollision)
        {
            Vector2 collisionPosition = collision.transform.position;
            StartCoroutine(MoveTowards(collision.gameObject, collisionPosition));
        }
    }
    protected virtual IEnumerator MoveTowards(GameObject player, Vector2 collisionPosition)
    {
        this.gameObject.GetComponent<LineRenderer>().SetPosition(0, collisionPosition - new Vector2(this.transform.position.x, this.transform.position.y));
        this.gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector2(Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(0).x * 64) / 64f, Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(0).y * 64) / 64f));
        player.GetComponent<MoveScript>().DisableControl();
        TransitionInProgress(true);
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return null;

        for (int i = 0; i < this.gameObject.GetComponent<LineRenderer>().positionCount - 1; i++)
        {
            Vector2 originalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i) + this.transform.position;
            Vector2 finalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i+1) + this.transform.position;
            player.transform.position = originalLocation;
            //block vision if traveling behind background;
            while (Vector2.Distance(player.transform.position, finalLocation) > 0.05f)
            {
                Vector2 direction = (finalLocation - originalLocation).normalized;
                if (direction.normalized != Vector2.down && direction.normalized != Vector2.up && direction.normalized != Vector2.right && direction.normalized != Vector2.left)
                {
                    direction = direction / 1.4f;
                }
                player.GetComponent<MoveScript>().move(direction * 1.5f);
                originalLocation = player.transform.position;
                yield return null;
            }
            player.transform.position = finalLocation;
        }
        EndTransition(player);
        
    }

    protected virtual void EndTransition(GameObject player)
    {
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        player.GetComponent<MoveScript>().EnableControl();
        TransitionInProgress(false);
    }
    protected void EndTransition()
    {
        TransitionInProgress(false);
    }
    void SetAllowCollision(bool allow)
    {
        allowCollision = !allow;
    }
    private void OnDisable()
    {
        Transition.TransitionInProgress -= SetAllowCollision;
    }
}
