using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompletionBar : MonoBehaviour
{
    [SerializeField] Gradient gradient;
    private void Awake()
    {
        ActionCommandHandler.updateBar += Fill;
    }
    public void CreateGradient(move currentMove)
    {
        gradient = new Gradient();
        GradientColorKey[] colorKey;
        colorKey = new GradientColorKey[currentMove.action.maximum+2];
        print("gradient set" + colorKey.Length);
        colorKey[0].color = Color.white;
        colorKey[0].time = Mathf.Round(((float)(currentMove.action.minimum) / (float)(currentMove.action.minimum + (currentMove.action.additional * currentMove.action.maximum)) * 100f) - 1f) / 100f;
        for (int i= 1; i<colorKey.Length-1; i++)
        {
            colorKey[i].color = Resources.Load<commandTextDegrees>("UIPrefabs/objectPrefabs/ActionCommands/commandText").colors[i-1];
            colorKey[i].time = Mathf.Round(((float)(currentMove.action.minimum + (currentMove.action.additional * i)) / (float)(currentMove.action.minimum + (currentMove.action.additional * currentMove.action.maximum)) * 100f) - 1f) / 100f;
        }
        colorKey[colorKey.Length - 1].color = Resources.Load<commandTextDegrees>("UIPrefabs/objectPrefabs/ActionCommands/commandText").colors[colorKey.Length - 2];
        colorKey[colorKey.Length - 1].time = Mathf.Round((float)(currentMove.action.minimum + (currentMove.action.additional * (colorKey.Length - 2))) / (float)(currentMove.action.minimum + (currentMove.action.additional * currentMove.action.maximum)) * 100f) / 100f;

        GradientAlphaKey[] alphaKey;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1;
        gradient.SetKeys(colorKey, alphaKey);
        gradient.mode = GradientMode.Fixed;
        
        this.gameObject.GetComponent<Slider>().maxValue = currentMove.action.minimum + (currentMove.action.additional * (currentMove.action.maximum));
        this.gameObject.GetComponent<Slider>().value = 0;
    }
    public void Fill(int amount)
    {
        this.gameObject.GetComponent<Slider>().value = amount;
        GameObject.Find("Canvas2/ButtonPrompt/Bar/fill").GetComponent<Image>().color = gradient.Evaluate(this.gameObject.GetComponent<Slider>().normalizedValue);
        this.gameObject.GetComponent<Slider>().value = amount + 1;
    }
    private void OnDisable()
    {
        ActionCommandHandler.updateBar -= Fill;
    }
}
