using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    List<commandButton> buttons;
    public bool success { get; protected set; } = false;
    public virtual void Perform(move selectedMove)
    {
        buttons.Clear();
        buttons.AddRange(selectedMove.action.buttons);
    }
}
