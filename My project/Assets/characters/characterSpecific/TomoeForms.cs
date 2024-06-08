using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomoeForms : BattleUnitForm
{
    override public void upForm()
    {
        print("new script");
        this.gameObject.GetComponent<BattleUnitStatus>().defense--;
        base.upForm();
        
    }
    override public void downForm()
    {
        this.gameObject.GetComponent<BattleUnitStatus>().defense++;
        base.downForm();
        
    }
}
