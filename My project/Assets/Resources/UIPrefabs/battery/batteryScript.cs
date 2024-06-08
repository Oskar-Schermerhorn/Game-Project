using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryScript : MonoBehaviour
{
    [SerializeField] private int BP;
    [SerializeField] private int BPmax;
    [SerializeField] List<GameObject> segments = new List<GameObject> { };
    [SerializeField] GameObject botSeg;
    [SerializeField] GameObject midSeg;
    [SerializeField] GameObject topSeg;
    DataRecorderBattery dataBattery;
    private void Awake()
    {
        dataBattery = GameObject.Find("DataRecorder").GetComponent<DataRecorderBattery>();
        BPmax = dataBattery.getMaxBP();
        BP = dataBattery.bp;
    }
    private void Start()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((BPmax * 16 + 5f), 22);
        setupSegments(BP);
    }

    private void setupSegments(int numSegments)
    {
        for (int i = 1; i<= numSegments; i++)
        {
            addSegment(i);
        }
    }
    private void addSegment(int BPcount)
    {
        if (BPcount == 1)
        {
            print("putting in bottom");
            segments.Add(Instantiate(botSeg, this.GetComponent<Transform>()));
        }
        else if (BPcount == BPmax)
        {
            print("putting in top");
            segments.Add(Instantiate(topSeg, this.GetComponent<Transform>()));
        }
        else
        {
            print("putting in mid");
            segments.Add(Instantiate(midSeg, this.GetComponent<Transform>()));
        }
    }
    private void removeSegment()
    {
        Destroy(segments[BP]);
        segments.RemoveAt(BP);
    }
    public int getBP()
    {
        return BP;
    }
    public int getBPMax()
    {
        return BPmax;
    }
    public void useBP(int value)
    {
        BP -= value;
        print("useBP");
        for(int i = 0; i< value; i++)
        {
            removeSegment();
        }
    }
    public void regenBP(int value)
    {
        if (BP+value > BPmax)
        {
            value = BPmax - BP;
        }
        for(int i = BP+1; i<= BP+value; i++)
        {
            addSegment(i);
        }
        BP += value;
    }
}
