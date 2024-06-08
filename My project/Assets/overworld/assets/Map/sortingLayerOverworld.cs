using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingLayerOverworld : MonoBehaviour
{
    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        Rooms.ActiveRoom += checkActiveRoom;
    }

    void checkActiveRoom(bool active, GameObject room)
    {
        if (transform.IsChildOf(room.transform))
        {
            if (active)
            {
                print("trees listening");
                MoveScript.Moving += checkAbove;
            }
            else
            {
                print("trees stopped listening");
                MoveScript.Moving -= checkAbove;
            }
        }
    }

    void checkAbove(Vector2 _, Vector2 _2)
    {
        if (Vertical())
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = player.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 3;
            if (Horizontal())
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 150 / 255f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = -3;
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
    bool Vertical()
    {
        if(player != null)
        {
            if (player.transform.position.y >= this.gameObject.transform.position.y)
                if (player.transform.position.y <= this.gameObject.transform.position.y + this.gameObject.GetComponent<SpriteRenderer>().bounds.size.y)
                    return true;
        }
        
        return false;
    }
    bool Horizontal()
    {
        if(player!= null)
        {
            if (player.position.x >= this.gameObject.transform.position.x - this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                if (player.position.x <= this.gameObject.transform.position.x + this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                    return true;
        }
        
        return false;
    }
    private void OnDisable()
    {
        MoveScript.Moving -= checkAbove;
        Rooms.ActiveRoom -= checkActiveRoom;
    }
}
