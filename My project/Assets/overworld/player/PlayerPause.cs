using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPause : MonoBehaviour, PlayerControlInterface
{
    public static event Action PauseGame;
    public BattleInputs controls;
    private void Awake()
    {
        MoveScript.EnableMovementControls += EnableControl;
        MoveScript.DisableMovementControls += DisableControl;
        
        print(this.name + "listening");
        controls = new BattleInputs();
        controls.Overworld.Pause.started += context => PauseGame();
    }
    public void EnableControl()
    {
        controls.Enable();
    }
    public void DisableControl()
    {
        controls.Disable();
    }
    private void OnDisable()
    {
        DisableControl();
        controls.Overworld.Pause.started -= context => PauseGame();
        MoveScript.EnableMovementControls -= EnableControl;
        MoveScript.DisableMovementControls -= DisableControl;

    }
}
