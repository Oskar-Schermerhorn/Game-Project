using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnitUse : MonoBehaviour
{
    public void Use(move currentMove, List<int> target)
    {
        this.gameObject.GetComponent<BattleUnitAttackOffset>().handleOffset(currentMove, target);
        this.gameObject.GetComponent<BattleUnitAnimate>().changeAnimation(currentMove.Name);
    }
    private int determineWhichAnimation(move currentMove, List<int> target)
    {
        /*if(currentMove.targetType == targetType.PAIRS)
        {
            if (target.Count > 1)
            {
                if(target[1] == 5)
                {
                    return 1;
                }
            }
        }*/
        return 0;
    }
}
