using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompt : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    public void Select()
    {
        //print("selected");
        GetComponent<SpriteRenderer>().sprite = sprites[1];
    }
    public void Select(int index)
    {
        print("selected");
        if(index < sprites.Count)
            GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
    public void Deselect()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
