using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSet : MonoBehaviour
{
    public void ChangeValue()
    {
        SetResolution((int)this.gameObject.GetComponent<Slider>().value);
    }
    public void SetResolution(int multiplier)
    {
        Screen.SetResolution(320 * multiplier, 180 * multiplier, Screen.fullScreen);
        print("setting resolution to " + (320 * multiplier) + "x" + (180 * multiplier));
    }
}
