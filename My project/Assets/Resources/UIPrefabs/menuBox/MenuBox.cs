using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBox : MonoBehaviour
{
    [SerializeField] Color normalColor;
    [SerializeField] Color selectedColor;
    public void Select()
    {
        GetComponent<Image>().color = selectedColor;
    }
    public void Deselect()
    {
        GetComponent<Image>().color = normalColor;
    }
}
