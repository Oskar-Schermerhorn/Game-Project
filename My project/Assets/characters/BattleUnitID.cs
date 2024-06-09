using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum side { PLAYER, ENEMY, OTHER}
public class BattleUnitID : MonoBehaviour
{
    public int ID;
    public side UnitSide;
}
