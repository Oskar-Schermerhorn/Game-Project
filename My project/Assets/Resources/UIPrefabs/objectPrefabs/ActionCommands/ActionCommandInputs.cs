using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class ActionCommandInputs : MonoBehaviour
{
    BattleInputs actionCommands;
    public static event Action<commandButton> Button;

    public void Open()
    {
        actionCommands = new BattleInputs();
        actionCommands.Enable();
        actionCommands.ActionCommands.Direction.performed += Direction;
        actionCommands.ActionCommands.Direction.canceled += Direction;
    }
    public void Close()
    {
        actionCommands.Disable();
        actionCommands.ActionCommands.Direction.performed -= Direction;
        actionCommands.ActionCommands.Direction.canceled -= Direction;
    }

    private void Update()
    {
        if (actionCommands != null)
        {
            if (actionCommands.ActionCommands.A.triggered)
            {
                Button(commandButton.A);
            }
            if (actionCommands.ActionCommands.B.triggered)
            {
                Button(commandButton.B);
            }
            if (actionCommands.ActionCommands.R.triggered)
            {
                Button(commandButton.R);
            }
            if (actionCommands.ActionCommands.L.triggered)
            {
                Button(commandButton.L);
            }
        }

        
    }
    void Direction(InputAction.CallbackContext context)
    {
        translateDirectionToButton(actionCommands.ActionCommands.Direction.ReadValue<Vector2>());
    }
    private void translateDirectionToButton(Vector2 direction)
    {
        if (direction.x >= 0.5f)
        {
            if (direction.y >= 0.5f)
                Button(commandButton.A9);
            else if (direction.y <= -0.5f)
                Button(commandButton.A3);
            else
                Button(commandButton.A6);
        }
        else if (direction.x <= -0.5f)
        {
            if (direction.y >= 0.5f)
                Button(commandButton.A7);
            else if (direction.y <= -0.5f)
                Button(commandButton.A1);
            else
                Button(commandButton.A4);
        }
        else
        {
            if (direction.y >= 0.5f)
                Button(commandButton.A8);
            else if (direction.y <= -0.5f)
                Button(commandButton.A2);
            else
                Button(commandButton.A5);
                
        }
    }
    private void OnDisable()
    {
        if(actionCommands != null)
            Close();
    }
}
