using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statusType { DAMAGE, HEAL, STUN, DAMAGEMOD, DEFENSEMOD, OTHER}
public enum statusTime { STARTTURN, INSTANT, MOVESELECTED, ONHIT}
[CreateAssetMenu]
public class statusEffect : ScriptableObject
{
    public string effectName;
    public string description;
    public GameObject icon;
    public int amount;
    public int additional;
    public int duration;
    public statusType type;
    public statusTime time;
    public GameObject animation;
    //public GameObject iconGO;
    public bool topRow;
    public Sprite[] sprites;

    public void animate(Transform t, int position)
    {
        GameObject newStatus = Instantiate<GameObject>(animation, t);
        newStatus.name = "StatusAnimation(" + effectName + ")";
        newStatus.GetComponent<statusAnimation>().status = this;
        newStatus.GetComponent<statusAnimation>().target = t.gameObject;
        int shockIndex = position;
        if(effectName == "shock")
        {
            if (shockIndex < 4)
                shockIndex += 4;
            newStatus.GetComponent<Animator>().Play(effectName + shockIndex);
        }
        else
        {
            newStatus.GetComponent<Animator>().Play(effectName);
        }
    }

    public void makeIcon(Transform t)
    {
        GameObject iconGO = Instantiate<GameObject>(icon, t);
        iconGO.GetComponent<statusIcon>().setStatus(this, duration);
        iconGO.name = "StatusIcon(" + effectName + ")";
        if (topRow)
            iconGO.transform.position = new Vector2(iconGO.transform.position.x - 30 / 64f, iconGO.transform.position.y);
        else
            iconGO.transform.position = new Vector2(iconGO.transform.position.x - 30 / 64f, iconGO.transform.position.y - 12 / 64f);

        for (int i = 0; i < t.gameObject.GetComponentsInChildren<statusIcon>().Length; i++)
        {
            if (t.GetComponentsInChildren<statusIcon>()[i].status.topRow == topRow)
            {
                iconGO.transform.position = new Vector2(iconGO.transform.position.x + 10 / 64f, iconGO.transform.position.y);
            }
        }
    }
}