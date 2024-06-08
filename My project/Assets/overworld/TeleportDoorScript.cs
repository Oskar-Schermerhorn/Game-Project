using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoorScript : MonoBehaviour
{
    [SerializeField] Vector2 destination;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = destination;
        }
    }
}
