using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObstacle : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] float speed;
    Coroutine lastPartyMember;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.transform.position = destination;
            StartCoroutine(JumpTo(collision.gameObject));
        }
    }
    IEnumerator JumpTo(GameObject player)
    {
        player.GetComponent<MoveScript>().DisableControl();
        player.GetComponent<PlayerCollision>().IFrames();
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        Vector3 originalPosition = player.transform.position;
        while (Vector3.Distance(player.transform.position, originalPosition+destination) > 0.1)
        {
            yield return null;
            
            player.transform.position = Vector3.Lerp(player.transform.position, originalPosition + destination, speed);
            GameObject.Find("Player/Party").transform.position = originalPosition;
            GameObject.Find("Player/Party/Party (1)").transform.position = originalPosition;
            GameObject.Find("Player/Party/Party (1)/Party (2)").transform.position = originalPosition;
        }
        player.transform.position = originalPosition + destination;
        player.GetComponent<PlayerCollision>().EndIFrames();
        StartCoroutine(PartyFollow(GameObject.Find("Player/Party"), originalPosition, player.GetComponent<PlayerAnimator>().currentState));
        StartCoroutine(PartyFollow(GameObject.Find("Player/Party/Party (1)"), originalPosition, player.GetComponent<PlayerAnimator>().currentState));
        yield return StartCoroutine(PartyFollow(GameObject.Find("Player/Party/Party (1)/Party (2)"), originalPosition, player.GetComponent<PlayerAnimator>().currentState));
        print("done");
        player.GetComponent<MoveScript>().EnableControl();
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
    IEnumerator PartyFollow(GameObject party, Vector3 originalPosition, string animation)
    {
        party.transform.position = originalPosition;
        party.GetComponent<PartyMoveScript>().changeAnimation(animation);
        party.GetComponent<PartyMoveScript>().clearMovements();
        while (Vector3.Distance(party.transform.position, originalPosition + destination) > 0.1)
        {
            yield return null;

            party.transform.position = Vector3.Lerp(party.transform.position, originalPosition + destination, speed);
            for(int i =0; i< party.transform.childCount; i++)
            {
                party.transform.GetChild(i).position = originalPosition;
            }
        }
        party.transform.position = originalPosition + destination;
        print("party done");
    }
}
