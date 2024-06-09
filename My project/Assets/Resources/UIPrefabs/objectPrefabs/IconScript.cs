using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    SpawnScript spawn;
    List<Sprite> displayIcon = new List<Sprite> { };

    private void Start()
    {
        spawn = GameObject.FindWithTag("BattleHandler").GetComponent<SpawnScript>();
        MoveCoroutine.spinComplete += setIcons;
        SpawnScript.battleSetup +=setIcons;
        BattleUnitForm.FormIcon += setIcons;
        BattleUnitHealth.Hit += updateHP;
    }
    void setIcons(bool _)
    {
        setIcons();
    }
    private void setIcons()
    {
        switch (this.gameObject.name)
        {
            case "Icon1":
                setPlayer(0);
                break;
            case "Icon2":
                setPlayer(1);
                break;
            case "Icon3":
                setPlayer(2);
                break;


        }
    }
    private void setPlayer(int p)
    {
        if(p > spawn.unitList.Count)
        {
            player = spawn.unitList[p];
            if (player.GetComponent<BattleUnitID>().UnitSide == side.PLAYER)
            {
                displayIcon.Clear();
                displayIcon.AddRange(player.GetComponent<BattleUnitIcons>().GetSprites());
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                //player.GetComponent<BattleUnitHealth>().hit.AddListener(updateHP);
            }
            updateHP();
        }
        
    }
    private void updateHP()
    {
        if(player != null && GetComponent<SpriteRenderer>().enabled && player.GetComponent<BattleUnitHealth>() != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = player.GetComponent<BattleUnitHealth>().health + "/" + player.GetComponent<BattleUnitHealth>().maxhealth;
            if (player.GetComponent<BattleUnitHealth>().health > 0)
            {
                GetComponent<SpriteRenderer>().sprite = displayIcon[0];
                GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = displayIcon[1];
                GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }

    private void OnDisable()
    {
        MoveCoroutine.spinComplete -= setIcons;
        SpawnScript.battleSetup -= setIcons;
        BattleUnitForm.FormIcon -= setIcons;
        BattleUnitHealth.Hit -= updateHP;
    }
}
