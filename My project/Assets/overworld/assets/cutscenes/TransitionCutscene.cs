using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCutscene : Transition
{

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 collisionPosition = collision.transform.position;
            StartCoroutine(MoveTowards(collision.gameObject, collisionPosition));
        }
    }
    protected override IEnumerator MoveTowards(GameObject player, Vector2 collisionPosition)
    {
        yield return null;
        for (int i = 0; i < this.gameObject.GetComponent<LineRenderer>().positionCount - 1; i++)
        {
            Vector2 originalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i) + this.transform.position;
            Vector2 finalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i + 1) + this.transform.position;
            player.transform.position = originalLocation;
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
        player.GetComponent<MoveScript>().move(Vector2.zero);
    }
    public void endCutscene()
    {
        EndTransition();
    }
}
