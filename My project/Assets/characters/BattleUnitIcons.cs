using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitIcons : MonoBehaviour
{
    public Sprite[] icons;
    public Sprite[] secondaryIcons;
    public Sprite[] thirdIcons;
    public int form = 0;
    public void changeIcons()
    {
        //primaryActive = !primaryActive;
        form = this.gameObject.GetComponent<BattleUnitForm>().form;
    }
    public Sprite[] GetSprites()
    {
        Sprite[] currentFormSprites = new Sprite[2];
        currentFormSprites[0] = icons[form*2];
        currentFormSprites[1] = icons[form*2+1];
        return currentFormSprites;
    }
}
