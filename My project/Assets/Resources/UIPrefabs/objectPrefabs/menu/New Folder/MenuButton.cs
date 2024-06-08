using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour, ISelectable
{
    [SerializeField] Sprite[] sprites;
    public int currentSprite { get; private set; } = 0;
    public void changeSprite(int index)
    {
        if(index >=0 && index < sprites.Length)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
            currentSprite = index;
        }
        else
        {
            print("invalid index");
        }
    }
    
}
