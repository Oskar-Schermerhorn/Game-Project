using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorderItems : MonoBehaviour
{
    public int[] items = new int[5];
    [SerializeField] private item[] possibleItems;
    public void addItem(int newItem)
    {
        int openSlot = -1;
        for (int i = 4; i >= 0; i--)
        {
            if (items[i] == 5)
            {
                openSlot = i;
            }
        }
        if (openSlot != -1)
            items[openSlot] = newItem;
    }
    public void replaceItem(int newItem, int index)
    {
        items[index] = newItem;
    }

    public void usedItem(int index)
    {
        print("usedItemFunction");
        for (int i = index; i < (items.Length - 1); i++)
        {
            items[i] = items[i + 1];
        }
        items[items.Length - 1] = 5;
    }
    public item getItem(int index)
    {
        return possibleItems[index];
    }
    public List<int> getOpenItemSlots()
    {
        List<int> openItemSlot = new List<int>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == 5)
                openItemSlot.Add(i);
        }
        return openItemSlot;
    }
}
