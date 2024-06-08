using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleUnitForm : MonoBehaviour
{
    public int form = 0;
    public int secondary = 2;
    public List<Sprite> formSprites;
    public List<RuntimeAnimatorController> animators;
    public static event Action FormIcon;
    //change animator
    //change icon
    //change moveset
    public virtual void upForm()
    {
        form++;
        if(form > formSprites.Count-1)
        {
            form = formSprites.Count - 1;
        }
        this.gameObject.GetComponent<BattleUnitIcons>().changeIcons();
        FormIcon();

        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[form];
        this.gameObject.GetComponent<BattleUnitData>().updateData();
    }
    public virtual void downForm()
    {
        form--;
        if(form < 0)
        {
            form = 0;
        }
        this.gameObject.GetComponent<BattleUnitIcons>().changeIcons();
        FormIcon();

        this.gameObject.GetComponent<Animator>().runtimeAnimatorController = animators[form];
        this.gameObject.GetComponent<BattleUnitData>().updateData();
    }
    
}
