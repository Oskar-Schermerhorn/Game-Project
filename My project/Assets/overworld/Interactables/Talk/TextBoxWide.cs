using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TextBoxWide : MonoBehaviour, TextBoxes
{
    public List<string> strings;
    public List<GameObject> openBoxes;
    public static event Action Reset;
    [SerializeField] int index = 0;
    public void show(List<string> s)
    {
        index = 0;
        strings = s;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = strings[0];
        Time.timeScale = 0f;
    }
    public void hide()
    {
        index = 0;
        transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
        Reset();
        GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
    }
    public void next()
    {
        index++;
        print(index);
        if (index > strings.Count - 1)
        {
            hide();
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = strings[index];
        }
    }
}
