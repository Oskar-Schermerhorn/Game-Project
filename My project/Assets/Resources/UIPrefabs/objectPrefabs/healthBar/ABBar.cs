using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABBar : MonoBehaviour
{
    BashHandler enemy;
    [SerializeField] GameObject segmentPrefab;
    List<GameObject> segments;
    private void Awake()
    {
        if(this.transform.parent.parent.gameObject.GetComponent<BashHandler>() != null)
            enemy = this.transform.parent.parent.gameObject.GetComponent<BashHandler>();
        turnManagement.PlayerTurn += show;
        targetInput.Confirm += hide;
        segments = new List<GameObject>();
        setSize();
    }

    private void setSize()
    {
        if (enemy != null)
        {
            this.gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(this.gameObject.GetComponent<RectTransform>().rect.width /enemy.maxbashes, 0.05f);
        }
    }

    private bool updateValue()
    {
        if (enemy.GetComponent<BattleUnitHealth>() == null || enemy.GetComponent<BattleUnitHealth>().health <= 0 || enemy.GetComponent<BashHandler>() == null)
            return false;

        while(segments.Count < enemy.bashes)
        {
            print("adding segment");
            GameObject newSegment = Instantiate(segmentPrefab, this.transform);
            segments.Add(newSegment);
        }
        while(segments.Count > enemy.bashes)
        {
            GameObject removeSeg = segments[segments.Count - 1];

            segments.RemoveAt(segments.Count - 1);
            Destroy(removeSeg);
        }
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
        turnManagement.PlayerTurn -= show;
        targetInput.Confirm -= hide;
    }
}
