using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBehindBackground : Transition
{
    [SerializeField] GameObject cover;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && allowCollision)
        {
            if (cover != null)
            {
                cover.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        base.OnTriggerEnter2D(collision);
        
            
    }
    protected override void EndTransition(GameObject player)
    {
        base.EndTransition(player);
        if (cover != null)
        {
            cover.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
