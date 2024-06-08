using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class optionsInput : MonoBehaviour
{
    BattleInputs battleControls;
    public static event Action<float> Cycle;
    public static event Action Confirm;
    public static event Action Cancel;

    private void Awake()
    {
        battleControls = new BattleInputs();
        menuExecute.Options += enableControls;
    }
    public void enableControls()
    {
        print("options listenting");
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
                Cycle(battleControls.BattleMenu.Navigate.ReadValue<float>());
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
        menuExecute.Options -= enableControls;
    }
}