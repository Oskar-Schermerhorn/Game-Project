using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public enum optionsType { MOVES, ITEMS}
public class optionsController : MonoBehaviour
{
    [SerializeField] optionsInput input;
    [SerializeField] public List<GameObject> activeOptions;
    public static event Action<int> confirmedMove;
    public static event Action<int> confirmedItem;
    int index = 0;
    batteryScript battery;
    optionsType type;
    private void Awake()
    {
        input = this.gameObject.GetComponent<optionsInput>();
        battery = GameObject.Find("Canvas/Battery").GetComponent<batteryScript>();
        menuExecute.OptionsType += setActive;
        optionsInput.Cycle += Cycle;
        optionsInput.Confirm += Confirm;
        optionsInput.Cancel += Cancel;
    }
    private void Cycle(float direction)
    {
        activeOptions[index].GetComponent<MenuBox>().Deselect();
        if (direction < 0)
        {
            index++;
            if (index >= activeOptions.Count)
                index = 0;
        }
        else
        {
            index--;
            if (index < 0)
                index = activeOptions.Count-1;
        }
        activeOptions[index].GetComponent<MenuBox>().Select();
    }
    private void setActive(optionsType setType)
    {
        type = setType;
        index = 0;
        activeOptions.Clear();
        for (int i = 0; i < GetComponentsInChildren<MenuBox>().Length; i++)
        {
            activeOptions.Add(GetComponentsInChildren<MenuBox>()[i].gameObject);
        }
        activeOptions[0].GetComponent<MenuBox>().Select();
    }
    private void Confirm()
    {
        if(type == optionsType.MOVES)
        {
            if (Validate(index + 1))
            {
                confirmedMove(index + 1);
                Cancel();
            }
        }
        else if(type == optionsType.ITEMS)
        {
            confirmedItem(index);
            Cancel();
        }
    }
    private void Cancel()
    {
        for (int i = 0; i < activeOptions.Count; i++)
        {
            Destroy(activeOptions[i]);
        }
        activeOptions.Clear();
        input.disableControls();
    }
    private bool Validate(int i)
    {
        return (int.Parse(GameObject.Find("Canvas/Options/Move" + i + "/Cost").GetComponent<TextMeshProUGUI>().text) <= battery.getBP());
    }
    private void OnDisable()
    {
        menuExecute.OptionsType -= setActive;
        optionsInput.Cycle -= Cycle;
        optionsInput.Confirm -= Confirm;
        optionsInput.Cancel -= Cancel;
    }
}
