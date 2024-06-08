using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleTextHandler : MonoBehaviour
{
    ObjectLocator locator;
    protected GameObject damageTextP;
    protected GameObject damageTextE;
    protected GameObject commandText;
    int successLevel = -1;
    List<string> commandTextDegrees = new List<string>();
    List<Color> commandTextColors = new List<Color>();
    static Color reddish = new Color(241 / 255f, 90 / 255f, 96 / 255f, 255 / 255f);
    static Color green = new Color(71 / 255f, 150 / 255f, 60 / 255f, 255 / 255f);
    static Color gray = new Color(185 / 255f, 185 / 255f, 185 / 255f, 255 / 255f);
    static Color blue = new Color(88 / 255f, 125 / 255f, 222 / 255f, 255 / 255f);

    private void Awake()
    {
        locator = this.gameObject.GetComponent<ObjectLocator>();
        damageTextP = Resources.Load<GameObject>("UIPrefabs/DamageText/DamageText(Player)");
        damageTextE = Resources.Load<GameObject>("UIPrefabs/DamageText/DamageText(Enemy)");
        commandText = Resources.Load<GameObject>("UIPrefabs/DamageText/CommandText");
        commandTextDegrees.AddRange(Resources.Load<commandTextDegrees>("UIPrefabs/objectPrefabs/ActionCommands/commandText").names);
        commandTextColors.AddRange(Resources.Load<commandTextDegrees>("UIPrefabs/objectPrefabs/ActionCommands/commandText").colors);
        BattleUnitInflict.Inflict += handleTexts;
        BattleUnitInflict.InflictSecondary += handleDamageText;
        statusAnimation.Inflict += handleTexts;
        ActionCommandHandler.Additional += addScore;
    }
    private void handleTexts(move currentMove, GameObject target, bool successful, bool parried, int damageMod, int hitNum)
    {
        handleDamageText(currentMove, target, successful, parried, damageMod, hitNum);
        handleCommandText(currentMove, target, successful, parried, hitNum);
    }
    private void handleDamageText(move currentMove, GameObject target, bool successful, bool parried, int damageMod, int hitNum)
    {
        int damage = currentMove.damageValues[hitNum];
        if (damage >= 0)
            damage += damageMod;
        else
            damage -= damageMod;

        GameObject type = damageTextP;
        Color color;
        string canvasLocation = "Canvas2";
        if (parried)
        {
            damage = 0;
            color = gray;
            canvasLocation = canvasLocation + "/PlayerPositions/Position" + locator.locateObject(target);
        }
        else
        {
            if (!successful)
            {
                damage /= 2;
            }

            if (checkType(target))
            {
                type = damageTextE;
                color = calcColor(damage, successful, false);
                canvasLocation = canvasLocation + "/EnemyPositions/Position" + locator.locateObject(target);
            }
            else
            {
                color = calcColor(damage, successful, parried);
                canvasLocation = canvasLocation + "/PlayerPositions/Position" + locator.locateObject(target);
            }
        }
        print("target name: "+ target.name);
        print("target location: " + target.transform.position);
        showDamageText(type, target, damage, color);
    }
    protected void showDamageText(GameObject type, GameObject target, int damage, Color color)
    {
        GameObject pos = target.transform.Find("Canvas").gameObject;
        GameObject text;
        //pos = GameObject.Find(targetLocation);
        //print("target is " + pos.name);
        
        text = Instantiate(type, pos.transform);
        //text = Instantiate(type, GameObject.Find("Canvas2").transform);

        //text.GetComponent<RectTransform>().position = target;
        text.GetComponent<TextMeshPro>().text = Mathf.Abs(damage).ToString();
        text.GetComponent<TextMeshPro>().color = color;
    }
    private bool checkType(GameObject target)
    {
        if(locator.locateObject(target) <4)
        {
            return false;
        }
        return true;
    }
    private Color calcColor(int damage, bool successful, bool parried)
    {
        if (damage < 0)
        {
            return green;
        }
        else if (parried)
            return gray;
        else if (!successful)
            return blue;
        else
            return reddish;
    }
    private void handleCommandText(move currentMove, GameObject target, bool successful, bool parried, int hitNum)
    {
        if(currentMove.action.type != actionCommandType.NONE)
        {
            if((currentMove.action.type != actionCommandType.DEFENSE && locator.locateObject(target) >= 4) ||
                currentMove.action.type == actionCommandType.DEFENSE && locator.locateObject(target) < 4)
            {
                if (parried)
                {
                    successLevel += 3;
                }
                else if (currentMove.action.type == actionCommandType.MASH)
                {
                    if (successful)
                        successLevel++;
                }
                else if(currentMove.action.type == actionCommandType.TIMED)
                {
                    if (successful)
                    {
                        successLevel++;
                    }
                    else
                    {
                        successLevel = -1;
                    }
                }
                else if (calcSuccessGood(target) == successful)
                {
                    successLevel += calcAddScore(currentMove);
                }
                if (successLevel >= 0)
                {
                    showCommandText(successLevel, target, (currentMove.action.type == actionCommandType.TIMED && hitNum < currentMove.action.buttons.Length-1));
                }
            }
        }
    }
    private int calcAddScore(move currentMove)
    {
        if(currentMove.action.type == actionCommandType.DEFENSE)
        {
            return 1;
        }
        else
        {
            return currentMove.action.buttons.Length;
        }
    }
    void addScore(int score)
    {
        successLevel += score;
    }
    private bool calcSuccessGood(GameObject target)
    {
        if (locator.locateObject(target) < 4)
        {
            return false;
        }
        return true;
    }
    private void showCommandText(int degree, GameObject target, bool isTimed)
    {
        GameObject pos = target.transform.Find("Canvas").gameObject;
        GameObject text;
        //int position = locator.locateObject(target);
        //if(position >=4)
        //    pos = GameObject.Find("Canvas2/EnemyPositions/Position" + position);
        //else
        //    pos = GameObject.Find("Canvas2/PlayerPositions/Position" + position);
        
        text = Instantiate(commandText, pos.transform);
        text.GetComponent<TextMeshPro>().text = commandTextDegrees[degree];
        text.GetComponent<TextMeshPro>().color = commandTextColors[degree];
        if(!isTimed)
            successLevel = -1;
    }
    private void OnDisable()
    {
        BattleUnitInflict.Inflict -= handleTexts;
        BattleUnitInflict.InflictSecondary -= handleDamageText;
        statusAnimation.Inflict -= handleTexts;
        ActionCommandHandler.Additional -= addScore;
    }
}
