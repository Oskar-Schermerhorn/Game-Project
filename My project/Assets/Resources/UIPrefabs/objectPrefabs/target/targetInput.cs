using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class targetInput : MonoBehaviour
{
    BattleInputs battleControls;
    public static event Action<float> Aim;
    public static event Action Confirm;
    public static event Action Cancel;

    private void Awake()
    {
        targetCreation.Targeting += enableControls;
        battleControls = new BattleInputs();
    }
    public void enableControls()
    {
        print("target listenting");
        battleControls.Enable();
    }
    public void disableControls()
    {
        battleControls.Disable();
    }
    private void Update()
    {
        if (battleControls != null)
        {
            if (battleControls.BattleMenu.Navigate.triggered)
            {
                Aim(battleControls.BattleMenu.Navigate.ReadValue<float>());
            }
            if (battleControls.BattleMenu.Select.triggered)
            {
                Confirm();
            }
            if (battleControls.BattleMenu.Back.triggered)
            {
                Cancel();
            }
        }
        
    }
    private void OnDisable()
    {
        targetCreation.Targeting -= enableControls;
    }
}
