using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MenuInput : MonoBehaviour
{
    BattleInputs battleControls;
    public static event Action<float> Arrows;
    public static event Action A;
    public static event Action B;
    public static event Action R;
    public static event Action L;
    private void Awake()
    {
        battleControls = new BattleInputs();
        turnManagement.PlayerTurn += enable;
    }
    private void enable(int x)
    {
        print("player turn enable");
        enableControls();
    }
    public void enableControls()
    {
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
                Arrows(battleControls.BattleMenu.Navigate.ReadValue<float>());
            }
            if (battleControls.BattleMenu.Select.triggered)
            {
                A();
            }
            if (battleControls.BattleMenu.Back.triggered)
            {
                B();
            }
            if (battleControls.BattleMenu.Swap.triggered)
            {
                L();
            }
            if (battleControls.BattleMenu.UndoSwap.triggered)
            {
                R();
            }
        }
    }
    private void OnDisable()
    {
        turnManagement.PlayerTurn -= enable;
    }
}
