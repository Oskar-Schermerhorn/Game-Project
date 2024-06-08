using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MoveScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float buffer = 0.3f;
    public float speed { get; private set; } = 5 / 3f;
    [SerializeField] private GameObject PauseMenu;
    public BattleInputs controls;
    bool controlsActive;
    public ContactFilter2D movementFilter;

    public List<RaycastHit2D> castCollisions { get; private set; } = new List<RaycastHit2D>();
    public float collisionOffset { get; private set; } = 0.05f;
    Vector2 direction;
    private GameObject data;
    public List<GameObject> itemDisplaysOpen = new List<GameObject> { };
    [SerializeField] private GameObject overfillmenu;
    [SerializeField] private GameObject ItemDisplayPrefab;
    public static event Action<Vector2, Vector2> Moving;
    public static event Action Stop;
    public static event Action EnableMovementControls;
    public static event Action DisableMovementControls;
    private void Awake()
    {
        controls = new BattleInputs();
        PlayerInteract.Interact += DisableControl;
        PlayerPause.PauseGame += DisableControl;
        TextBoxController.ReturnControl += EnableControl;
        controls.Overworld.Move.performed += context => findDirection(context.ReadValue<Vector2>());
        controls.Overworld.Move.canceled += context => findDirection(context.ReadValue<Vector2>());
    }
    void Start()
    {
        EnableControl();
        data = GameObject.Find("DataRecorder");
        overfillmenu = GameObject.Find("Canvas/ItemsHolder");
    }
    void Update()
    {
        if(controlsActive)
        {
            if (allowMovement())
            {
                move(direction);

            }
            else
            {
                Stop();
            }
        }
    }

    void findDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) - buffer < 0)
        {
            input = new Vector2(0, input.y);
        }
        if (Mathf.Abs(input.y) - buffer < 0)
        {
            input = new Vector2(input.x, 0);
        }
        if(input.normalized != Vector2.down && input.normalized != Vector2.up && input.normalized != Vector2.right && input.normalized != Vector2.left)
        {
            input = input / 1.4f;
        }
        direction = input;
        
    }
    bool allowMovement()
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, speed * Time.deltaTime + collisionOffset);
        if(count > 0)
        {
            rb.velocity = Vector2.zero;
            return false;
        }
        return true;
    }
    public void move(Vector2 MoveDirection)
    {
        rb.velocity = MoveDirection * speed;
        rb.position = new Vector2(Mathf.RoundToInt(rb.position.x * 64) / 64f, Mathf.RoundToInt(rb.position.y * 64) / 64f);
        Moving(MoveDirection, rb.velocity);

    }
    public void EnableControl()
    {
        controls.Enable();
        print("enablemovement");
        controlsActive = true;
        EnableMovementControls();
    }
    public void DisableControl()
    {
        move(Vector2.zero);
        controls.Disable();
        controlsActive = false;
        DisableMovementControls();
    }
    private void OnDisable()
    {
        controls.Overworld.Move.performed -= context => findDirection(context.ReadValue<Vector2>());
        controls.Overworld.Move.canceled -= context => findDirection(context.ReadValue<Vector2>());
        PlayerInteract.Interact -= DisableControl;
        PlayerPause.PauseGame -= DisableControl;
        TextBoxController.ReturnControl -= EnableControl;
    }
}
