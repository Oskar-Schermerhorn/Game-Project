using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnitHPBar : MonoBehaviour
{
    ObjectLocator locator;

    public void showHP()
    {
        GameObject HpBar = this.transform.Find("Canvas/HPBar").gameObject;
        for (int i = 0; i < 3; i++)
        {
            HpBar.GetComponentsInChildren<Image>()[i].enabled = true;
        }
        HpBar.GetComponent<Slider>().maxValue = this.gameObject.GetComponent<BattleUnitHealth>().maxhealth;
        HpBar.GetComponent<Slider>().value = this.gameObject.GetComponent<BattleUnitHealth>().health;
    }
    public void hideHP()
    {
        GameObject HpBar = this.transform.Find("Canvas/HPBar").gameObject;
        for (int i = 0; i < 3; i++)
        {
            HpBar.GetComponentsInChildren<Image>()[i].enabled = false;
        }
    }
}
