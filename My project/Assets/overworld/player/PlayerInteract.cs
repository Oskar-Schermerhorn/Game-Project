using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInteract : MonoBehaviour, PlayerControlInterface
{
    MoveScript moveScript;
    public ContactFilter2D interactFilter;
    Rigidbody2D rb;
    public static event Action Interact;
    public BattleInputs controls;
    private void Awake()
    {
        MoveScript.EnableMovementControls += EnableControl;
        MoveScript.DisableMovementControls += DisableControl;
        print("listening");
        moveScript = this.gameObject.GetComponent<MoveScript>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        controls = new BattleInputs();
        
        MoveScript.Moving += detectInteract;
        
        
        controls.Overworld.Interact.started += context => selectInteract();
    }
    void detectInteract(Vector2 _, Vector2 _2)
    {
        List<RaycastHit2D> interactables = findInteractables();
        //print(interactables.Count);
        if (interactables.Count > 0)
        {
            showIcon();
        }
        else
        {
            hideIcon();
        }
    }

    void showIcon()
    {
        this.transform.Find("InteractIcon").GetComponent<SpriteRenderer>().enabled = true;
    }
    void hideIcon()
    {
        this.transform.Find("InteractIcon").GetComponent<SpriteRenderer>().enabled = false;
    }
    List<RaycastHit2D> findInteractables()
    {
        float offset = Time.deltaTime + 2 * moveScript.collisionOffset;
        List<RaycastHit2D> interactableObjects = new List<RaycastHit2D>();
        List<RaycastHit2D> directionalInteractableObjects = new List<RaycastHit2D>();
        rb.Cast(Vector2.up, interactFilter, directionalInteractableObjects, moveScript.speed * offset);
        interactableObjects.AddRange(directionalInteractableObjects);
        rb.Cast(Vector2.down, interactFilter, directionalInteractableObjects, moveScript.speed * offset);
        interactableObjects.AddRange(directionalInteractableObjects);
        rb.Cast(Vector2.left, interactFilter, directionalInteractableObjects, moveScript.speed * offset);
        interactableObjects.AddRange(directionalInteractableObjects);
        rb.Cast(Vector2.right, interactFilter, directionalInteractableObjects, moveScript.speed * offset);
        interactableObjects.AddRange(directionalInteractableObjects);
        return interactableObjects;
    }
    void selectInteract()
    {
        print("selecting interact");
        List<RaycastHit2D> interactions =  findInteractables();
        
        //interactable layer == 6
        if (interactions.Count > 0)
        {
            if (interactions[0].transform.gameObject.layer == 6 || interactions[0].transform.gameObject.layer == 7)
            {
                GameObject interacting = interactions[0].transform.gameObject;
                interacting.GetComponent<Interactable>().Use();
                if (interacting.GetComponent<Interactable>().dialogue.Count > 0)
                {
                    Interact();
                    DisableControl();
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
        
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
        controls.Overworld.Interact.started -= context => selectInteract();
        MoveScript.Moving -= detectInteract;
        MoveScript.EnableMovementControls -= EnableControl;
        MoveScript.DisableMovementControls -= DisableControl;
        
    }
}
