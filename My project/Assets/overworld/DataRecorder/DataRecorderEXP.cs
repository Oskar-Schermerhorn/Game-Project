using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderEXP : MonoBehaviour
{
    [SerializeField] int exp = 0;
    public bool getEXP(int amount)
    {
        exp += amount;
        if (exp >= 100)
        {
            print("Level up");
            exp -= 100;
            return true;
        }
        return false;
    }
}
