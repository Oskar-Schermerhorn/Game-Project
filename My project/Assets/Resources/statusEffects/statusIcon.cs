using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusIcon : MonoBehaviour
{
    public statusEffect status;
    public int statusDuration = 0;
    public int poisonTurns = 0;
    [SerializeField] private Sprite[] questionMark;

    private void Awake()
    {
        turnManagement.Status += decreaseStatus;
        BattleUnitStatus.ChangeIcon += updateStatus;
        turnManagement.PlayerTurn += showIcon;
        targetController.confirmedTarget += hideIcon;
    }
    void decreaseStatus()
    {
        if (status != null)
        {
            print("decreasing duration");
            statusDuration--;
            if (status.effectName == "poison")
            {
                poisonTurns++;
                if (statusDuration >= 5)
                    poisonTurns++;
            }
                
            updateStatus();
        }
    }
    void updateStatus()
    {
        if (status != null)
        {
            if (statusDuration <= 0)
            {
                BattleUnitStatus battleUnitStatus = this.gameObject.transform.parent.gameObject.GetComponent<BattleUnitStatus>();
                if (battleUnitStatus != null)
                {
                    battleUnitStatus.removeStatus(status);
                    for (int i = 0; i < battleUnitStatus.myStatus.Count; i++)
                    {
                        GameObject iconGO = GameObject.Find("StatusIcon(" + battleUnitStatus.myStatus[i].effectName + ")");
                        if (battleUnitStatus.myStatus[i].topRow == this.status.topRow && iconGO.transform.position.x >= this.transform.position.x)
                        {
                            iconGO.transform.position =
                                new Vector2(iconGO.transform.position.x - 10 / 64f, iconGO.transform.position.y);
                        }
                    }
                    print("destroying icon");
                    Destroy(this.gameObject);
                }
                if (status.effectName == "poison")
                    poisonTurns = 0;

            }
            setIcon();
        }
    }

    private void setIcon()
    {
        print(status.sprites.Length);
        if(status.sprites.Length >0)
        {
            if (statusDuration < 6 && statusDuration > 0)
                GetComponent<SpriteRenderer>().sprite = status.sprites[statusDuration - 1];
            else if (statusDuration >= 6)
                GetComponent<SpriteRenderer>().sprite = status.sprites[4];
        }
        else
        {
            if (statusDuration < 6 && statusDuration > 0)
                GetComponent<SpriteRenderer>().sprite = questionMark[statusDuration - 1];
            else if (statusDuration >= 6)
                GetComponent<SpriteRenderer>().sprite = questionMark[4];
        }
    }

    public void setStatus(statusEffect s, int duration)
    {

        status = s;
        statusDuration += duration;
    }

    public void addDuration(int duration)
    {
        statusDuration += duration;
    }

    public void showIcon(int i)
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    public void hideIcon(List<int> i)
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnDisable()
    {
        turnManagement.Status -= decreaseStatus;
        BattleUnitStatus.ChangeIcon -= updateStatus;
        turnManagement.PlayerTurn -= showIcon;
        targetController.confirmedTarget -= hideIcon;
    }
}
