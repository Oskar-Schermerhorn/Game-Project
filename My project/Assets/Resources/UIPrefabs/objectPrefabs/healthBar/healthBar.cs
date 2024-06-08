using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    //ObjectLocator locator;
    GameObject enemy;
    //[SerializeField] int position = -1;
    private void Awake()
    {
        enemy = this.transform.parent.parent.gameObject;
        //locator = GameObject.Find("BattleHandler").GetComponent<ObjectLocator>();
        //print(this.transform.parent.name);
        //position = int.Parse(this.transform.parent.name.Substring("Position".Length));
        //targetCreation.Targeting += show;
        turnManagement.PlayerTurn += show;
        targetInput.Confirm += hide;
        //targetInput.Cancel += hide;
    }
    private bool updateValue()
    {
        //GameObject battleUnit = locator.locateObject(position);
        if (enemy.GetComponent<BattleUnitHealth>() == null || enemy.GetComponent<BattleUnitHealth>().health <= 0)
            return false;
        this.gameObject.GetComponent<Slider>().maxValue = enemy.GetComponent<BattleUnitHealth>().maxhealth;
        this.gameObject.GetComponent<Slider>().value = enemy.GetComponent<BattleUnitHealth>().health;
        /*
        if(this.gameObject.GetComponent<BossUnitData>() != null)
        {
            this.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 64f);
        }*/
        return true;
    }
    public void show(int _)
    {
       
        if (updateValue())
        {
            print("showing hp bar");
            for (int i = 0; i < GetComponentsInChildren<Image>().Length; i++)
            {
                GetComponentsInChildren<Image>()[i].enabled = true;
            }
        }
    }
    public void hide()
    {
        for (int i = 0; i < GetComponentsInChildren<Image>().Length; i++)
        {
            GetComponentsInChildren<Image>()[i].enabled = false;
        }
    }
    private void OnDisable()
    {
        //targetCreation.Targeting -= show;
        turnManagement.PlayerTurn -= show;
        targetInput.Confirm -= hide;
        //targetInput.Cancel -= hide;
    }
}
