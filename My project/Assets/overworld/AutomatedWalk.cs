using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedWalk : MonoBehaviour
{
    [SerializeField] Vector2 destination;
    [SerializeField] float time;
    private void Awake()
    {
        for(int i =0; i< this.gameObject.GetComponent<LineRenderer>().positionCount; i++)
        {
            this.gameObject.GetComponent<LineRenderer>().SetPosition(i, new Vector2(Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(i).x * 64) / 64f, Mathf.RoundToInt(this.gameObject.GetComponent<LineRenderer>().GetPosition(i).y * 64) / 64f));
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTowards(collision.gameObject));
        }
    }
    IEnumerator MoveTowards(GameObject player)
    {
        player.GetComponent<MoveScript>().DisableControl();
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        yield return null;
        
        for(int i= 0; i< this.gameObject.GetComponent<LineRenderer>().positionCount -1; i++)
        {
            Vector2 originalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i);
            Vector2 finalLocation = this.gameObject.GetComponent<LineRenderer>().GetPosition(i+1);
            player.transform.position = originalLocation;
            //block vision if traveling behind background;
            while (Vector2.Distance(player.transform.position, finalLocation) > 0.1f)
            {
                Vector2 direction = (finalLocation - originalLocation).normalized;
                if (direction.normalized != Vector2.down && direction.normalized != Vector2.up && direction.normalized != Vector2.right && direction.normalized != Vector2.left)
                {
                    direction = direction / 1.4f;
                }
                player.GetComponent<MoveScript>().move(direction*1.5f);
                originalLocation = player.transform.position;
                yield return null;
            }
            player.transform.position = finalLocation;
        }
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        player.GetComponent<MoveScript>().EnableControl();
    }
}
