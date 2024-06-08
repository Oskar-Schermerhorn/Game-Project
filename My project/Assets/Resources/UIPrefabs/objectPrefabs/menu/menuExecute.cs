using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class menuExecute : MonoBehaviour
{
    ObjectLocator locator;
    turnManagement turn;
    menuMoveHolder moveHolder;
    batteryScript battery;
    DataRecorderItems dataItem;
    [SerializeField] GameObject MenuBoxPrefab;
    [SerializeField] GameObject ItemBoxPrefab;
    [SerializeField] MenuInput input;
    [SerializeField] menuState state;
    [SerializeField] SpinHandler spin;
    public static event Action<optionsType> OptionsType;
    public static event Action Options;
    [SerializeField] Color red;
    [SerializeField]bool isItem = false;
    [SerializeField] public int numSpins { get; private set; } = 0;
    public bool left = true;
    public bool manual = false;
    public int spinCost = 1;
    private void Awake()
    {
        turn = GameObject.Find("BattleHandler").GetComponent<turnManagement>();
        locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        spin = GameObject.Find("BattleHandler").GetComponent<SpinHandler>();
        dataItem = GameObject.Find("DataRecorder").GetComponent<DataRecorderItems>();
        moveHolder = this.gameObject.GetComponent<menuMoveHolder>();
        input = this.gameObject.GetComponent<MenuInput>();
        state = this.gameObject.GetComponent<menuState>();
        battery = GameObject.Find("Canvas/Battery").GetComponent<batteryScript>();
        targetController.confirmedTarget += Use;
        EnemyUnitMoveSelect.targetSelected += UseMove;
    }
    public void resetIsItem()
    {
        isItem = false;
    }
    public void createMoveList()
    {
        isItem = false;
        //check battery
        for(int i=1; i< locator.locateObject(turn.turnNum).GetComponent<BattleUnitData>().moveset.Count; i++)
        {
            move moveAtI = locator.locateObject(turn.turnNum).GetComponent<BattleUnitData>().moveset[i];
            GameObject option = Instantiate<GameObject>(MenuBoxPrefab, GameObject.Find("Canvas/Options").transform);
            option.name = "Move" + i;
            GameObject.Find("Canvas/Options/Move" + i + "/MoveName").GetComponent<TextMeshProUGUI>().text = moveAtI.animationNames[0];
            GameObject.Find("Canvas/Options/Move" + i + "/Cost").GetComponent<TextMeshProUGUI>().text = moveAtI.cost.ToString();
            if(battery.getBP() < moveAtI.cost)
            {
                GameObject.Find("Canvas/Options/Move" + i + "/MoveName").GetComponent<TextMeshProUGUI>().color = red;
                GameObject.Find("Canvas/Options/Move" + i + "/Cost").GetComponent<TextMeshProUGUI>().color = red;
            }
        }
        Options();
        OptionsType(optionsType.MOVES);
    }
    public void createItemList()
    {
        if (dataItem.getOpenItemSlots().Count<5)
        {
            for(int i = 0; i<dataItem.getOpenItemSlots().Count; i++)
            {
                print(dataItem.getOpenItemSlots()[i]);
            }
            isItem = true;
            //maybe change layout to include pictures
            for (int i = 0; i < dataItem.items.Length; i++)
            {
                if (dataItem.items[i] != 5)
                {
                    item itemAtI = dataItem.getItem(dataItem.items[i]);
                    GameObject option = Instantiate<GameObject>(ItemBoxPrefab, GameObject.Find("Canvas/Options").transform);
                    option.name = "Item" + i;

                    GameObject.Find("Canvas/Options/Item" + i + "/ItemName").GetComponent<TextMeshProUGUI>().text = itemAtI.itemName;
                    GameObject.Find("Canvas/Options/Item" + i + "/Icon").GetComponent<Image>().sprite = itemAtI.art16Bit;
                }
            }
            Options();
            OptionsType(optionsType.ITEMS);
        }
        else
        {
            GameObject option = Instantiate<GameObject>(ItemBoxPrefab, GameObject.Find("Canvas/Options").transform);
            option.name = "Item0";
            GameObject.Find("Canvas/Options/Item0/ItemName").GetComponent<TextMeshProUGUI>().text = "No Items";
            Destroy(GameObject.Find("Canvas/Options/Item0/Icon").GetComponent<Image>());
            StartCoroutine(forceBack());
        }
    }
    IEnumerator forceBack()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(GameObject.Find("Canvas/Options/Item0"));
        state.retreat();
    }
    public void Use(List<int> targetPositions)
    {
        if (!isItem)
        {
            UseMove(targetPositions);
        }
        else
        {
            UseItem(targetPositions);
        }
    }
    public void UseMove(List<int> targetPositions)
    {

        if(turn.turnNum < 4)
        {
            //use bp
            if (moveHolder.currentMove.cost > 0)
            {
                battery.useBP(moveHolder.currentMove.cost);

            }
            else if (moveHolder.currentMove.cost < 0)
            {
                battery.regenBP(moveHolder.currentMove.cost * -1);
            }
        }

        //hide hp bars
        //fix switches
        numSpins = 0;

        locator.locateObject(turn.turnNum).GetComponent<BattleUnitUse>().Use( moveHolder.currentMove, targetPositions);
    }
    public void UseItem(List<int> targetPositions)
    {
        if (moveHolder.currentMove.cost < 0)
        {
            battery.regenBP(moveHolder.currentMove.cost * -1);
        }
        numSpins = 0;
        locator.locateObject(turn.turnNum).GetComponent<BattleUnitUse>().Use(moveHolder.currentMove, targetPositions);
        dataItem.usedItem(moveHolder.moveIndex);
    }
    private bool checkSpin()
    {
        if(turn.turnNum == 0)
        {
            spinCost = 0;
        }
        else
        {
            spinCost = 1;
        }
        return battery.getBP() >= spinCost;
    }
    public void Spin()
    {
        if (checkSpin())
        {
            battery.useBP(spinCost);
            numSpins++;
            spin.spin(locator.locateObject(turn.turnNum));
            input.disableControls();
            left = true;
            manual = true;
            StartCoroutine(ensureTurnCatcher(true));
        }
    }
    IEnumerator ensureTurnCatcher(bool left)
    {
        bool spinDone = false;
        MoveCoroutine.spinComplete += a;
        void a(bool _)
        {
            print("spining done");
            spinDone = true;
        }
        yield return new WaitUntil(() =>spinDone);
        
        MoveCoroutine.spinComplete -= a;
    }
    public void undoSpin()
    {
        if (numSpins>0)
        {
            battery.regenBP(spinCost);
            numSpins--;
            spin.reverseSpin(locator.locateObject(turn.turnNum));
            input.disableControls();
            left = false;
            manual = true;
            StartCoroutine(ensureTurnCatcher(false));
        }
    }
    private void OnDisable()
    {
        targetController.confirmedTarget -= Use;
        EnemyUnitMoveSelect.targetSelected -= UseMove;
    }
}
