using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TalkTextBox : MonoBehaviour, TextBoxes
{
    public List<Sprite> portraits;
    public GameObject talkTextBoxPrefabLeft;
    public GameObject talkTextBoxPrefabRight;
    public int recallIndex = 0;
    public List<string> strings;
    public List<GameObject> openBoxes;
    public static event Action Reset;
    bool doneScrolling = false;

    [SerializeField]int index = 0;
    public void show(List<string> s)
    {
        index = 0;
        strings = s;
        doneScrolling = false;
        transform.GetChild(0).gameObject.SetActive(true);
        createPortrait(nameToIndex(strings[0].Split('~')[0]), strings[0].Split('~')[0], strings[0].Split('~')[1]);
    }
    public void hide()
    {
        index = 0;
        for (int i = 0; i < openBoxes.Count; i++)
        {
            Destroy(openBoxes[i]);
            //openBoxes.RemoveAt(0);
        }
        openBoxes.Clear();
        transform.GetChild(0).gameObject.SetActive(false);
        Reset();
        GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
    }
    private int nameToIndex(string name)
    {
        switch (name)
        {
            case "Aeneas":
                return 1;
            case "Woodsworth":
                return 2;
            case "Bennu":
                return 3;
            case "Tomoe":
                return 4;
            case "Hyperion":
                return 5;
            case "Edward":
                return 6;
            case "Tonttu":
                return 7;
            case "Lenore":
                return 8;
        }
        return 0;
    }
    private void createPortrait(int index, string n, string t)
    {
        GameObject newTextBox;
        if (index == 0)
        {
            newTextBox = Instantiate(talkTextBoxPrefabRight, this.transform.GetChild(0));
        }
        else
        {
            newTextBox = Instantiate(talkTextBoxPrefabLeft, this.transform.GetChild(0));
        }
        openBoxes.Add(newTextBox);
        newTextBox.GetComponentsInChildren<Image>()[2].GetComponentInChildren<TextMeshProUGUI>().text = n;
        newTextBox.GetComponentsInChildren<Image>()[3].sprite = portraits[index];
        newTextBox.GetComponentInChildren<TextMeshProUGUI>().text = t;
        StartCoroutine(ScrollText(t, newTextBox, 0.05f));
    }
    IEnumerator ScrollText(string fullText, GameObject textBox, float speed)
    {
        doneScrolling = false;
        for (int i = 0; i < fullText.Length; i++)
        {
            if (fullText[i].Equals('<'))
            {
                while (!fullText[i].Equals('>'))
                {
                    i++;
                }
            }
            textBox.GetComponentInChildren<TextMeshProUGUI>().text = fullText.Substring(0, i+1);
            
            yield return new WaitForSeconds(speed);
        }
        print("done");
        doneScrolling = true;
    }
    public void next()
    {
        if (doneScrolling)
        {
            print("next talk");
            index++;
            if (index >= strings.Count)
            {
                StopAllCoroutines();
                hide();
                
            }
            else
            {
                if (recallIndex > 0)
                {
                    for (int i = 0; i < openBoxes.Count; i++)
                    {
                        openBoxes[i].transform.position = new Vector2(openBoxes[i].transform.position.x, openBoxes[i].transform.position.y + (60 / 64f) * recallIndex);
                    }
                }
                createPortrait(nameToIndex(strings[index].Split('~')[0]), strings[index].Split('~')[0], strings[index].Split('~')[1]);
                StartCoroutine(moveOpenBoxes());
            }
            for (int i = 0; i < openBoxes.Count - 1; i++)
            {
                openBoxes[i].GetComponent<Image>().color = new Color(openBoxes[i].GetComponent<Image>().color.r / 255f, openBoxes[i].GetComponent<Image>().color.b / 255f, openBoxes[i].GetComponent<Image>().color.g / 255f, 0.5f);
                openBoxes[i].GetComponentsInChildren<Image>()[2].color = new Color(openBoxes[i].GetComponent<Image>().color.r, openBoxes[i].GetComponent<Image>().color.b, openBoxes[i].GetComponent<Image>().color.g, 0.5f);
                openBoxes[i].GetComponentsInChildren<Image>()[3].color = new Color(openBoxes[i].GetComponent<Image>().color.r, openBoxes[i].GetComponent<Image>().color.b, openBoxes[i].GetComponent<Image>().color.g, 0.5f);
                openBoxes[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }

            recallIndex = 0;
        }
        else
        {
            StopAllCoroutines();
            print(openBoxes.Count - 1);
            print(strings[openBoxes.Count - 1]);
            openBoxes[openBoxes.Count-1].GetComponentInChildren<TextMeshProUGUI>().text = strings[openBoxes.Count - 1].Split('~')[1];
            doneScrolling = true;

        }
        
    }
    IEnumerator moveOpenBoxes()
    {
        print("move text boxes");
        while (openBoxes[0].transform.position.y - (openBoxes[openBoxes.Count - 1].transform.position.y + (openBoxes.Count - 1) * (60 / 64f)) <= -0.01f)
        {
            for (int i = openBoxes.Count - 1; i >= 0; i--)
            {
                //print("moving");
                openBoxes[i].transform.position = Vector3.Lerp(new Vector3(openBoxes[i].transform.position.x, openBoxes[i].transform.position.y, openBoxes[i].transform.position.z),
                    new Vector3(openBoxes[i].transform.position.x, openBoxes[openBoxes.Count - 1].transform.position.y + (openBoxes.Count - i - 1) * (60f / 64f), openBoxes[i].transform.position.z), 0.1f);
                //openBoxes[i].transform.position = new Vector3(openBoxes[i].transform.position.x, openBoxes[i].transform.position.y + 60/64f, openBoxes[i].transform.position.z);
            }
            yield return null;
        }
        openBoxes[0].transform.position = new Vector3(openBoxes[0].transform.position.x,
            openBoxes[1].transform.position.y + (60 / 64f), openBoxes[0].transform.position.z);

    }
    public void recallText(bool back)
    {
        if (back && recallIndex < openBoxes.Count - 1)
        {
            //recallTextBoxesBack = true;
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color =
            new Color(openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.r, openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.b,
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.g, 0.5f);
            recallIndex++;
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color =
            new Color(openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.r, openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.b,
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.g, 1f);
            for (int i = openBoxes.Count - 2; i >= 0; i--)
            {
                openBoxes[i].transform.position = new Vector3(openBoxes[i].transform.position.x, openBoxes[openBoxes.Count - 1].transform.position.y + (openBoxes.Count - i - 2) * (60f / 64f));
            }
            openBoxes[openBoxes.Count-1].transform.position = new Vector3(openBoxes[openBoxes.Count - 1].transform.position.x, openBoxes[openBoxes.Count - 1].transform.position.y - (60f / 64f));
        }
        else if (!back && recallIndex > 0)
        {
            //recallTextBoxesForward = true;
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color =
            new Color(openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.r, openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.b,
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.g, 0.5f);
            recallIndex--;
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color =
            new Color(openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.r, openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.b,
            openBoxes[openBoxes.Count - recallIndex - 1].GetComponent<Image>().color.g, 1f);
            for (int i = openBoxes.Count - 2; i >= 0; i--)
            {
                openBoxes[i].transform.position = new Vector3(openBoxes[i].transform.position.x, openBoxes[openBoxes.Count - 1].transform.position.y + (openBoxes.Count - i) * (60f / 64f));
            }
            openBoxes[openBoxes.Count - 1].transform.position = new Vector3(openBoxes[openBoxes.Count - 1].transform.position.x, openBoxes[openBoxes.Count - 1].transform.position.y + (60f / 64f));
        }
    }
}
