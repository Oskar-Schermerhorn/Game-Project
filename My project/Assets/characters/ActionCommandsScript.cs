using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCommandsScript : MonoBehaviour
{
    private GameObject prompt;
    [SerializeField] private GameObject additionalPrompt;
    private BattleInputs battleControls;
    private BattleInputs actionCommands;
    private turnManagement turn;
    private SpawnScript spawn;
    private menuScript menu;
    private bool allowAction = true;
    public bool pressed = false;
    public bool activated = false;
    private Queue<int> inputRecord;
    int currentInput;
    int previouslyEntered = 0;
    int fromInputQueue = 0;
    int inputIndex = 0;
    List<GameObject> openPrompts = new List<GameObject>();
    float timer = 0;
    float currentCharge = 0;
    public string currentState;
    public commandButton currentButton;

    private void Awake()
    {
        prompt = GameObject.Find("ActionCommandPrompt");
        battleControls = new BattleInputs();
        battleControls.Enable();
        actionCommands = new BattleInputs();
        turn = GameObject.FindWithTag("BattleHandler").GetComponent<turnManagement>();
        spawn = GameObject.FindWithTag("BattleHandler").GetComponent<SpawnScript>();
        menu = GameObject.Find("Menu").GetComponent<menuScript>();
        inputRecord = new Queue<int>();
    }
    void Start()
    {
        
    }
    //handle different inputs
    
    //enable controls and start
    
    //disable controls and clear
    
    
    
    private void blockTimer()
    {
        allowAction = true;
    }
    
}
