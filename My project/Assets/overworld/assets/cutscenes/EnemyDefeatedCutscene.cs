using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.SceneManagement;

public class EnemyDefeatedCutscene : MonoBehaviour
{
    [SerializeField] bool playCutscene = true;
    [SerializeField] bool repeatable = false;
    [SerializeField] PlayableDirector cutscene;
    [SerializeField] List<overworldEnemyDestroy> enemies = new List<overworldEnemyDestroy>();
    DataRecorderObjectPermanence dataObject;
    DataRecorderCombat dataCombat;
    DataRecorderProgress dataProgress;
    private void Awake()
    {
        dataObject = GameObject.Find("DataRecorder").GetComponent<DataRecorderObjectPermanence>();
        dataCombat = GameObject.Find("DataRecorder").GetComponent<DataRecorderCombat>();
        dataProgress = GameObject.Find("DataRecorder").GetComponent<DataRecorderProgress>();
    }
    private void Start()
    {
        if(dataObject.currentRoomName == this.transform.parent.name)
        {
            if(dataProgress!= null)
            CheckEnemies();
        }
        
    }
    void CheckEnemies()
    {
        print("checking condition");
        bool conditionMet = true;
        for(int i =0; i<enemies.Count; i++)
        {
            if (conditionMet)
            {
                conditionMet = false;
                for (int j = 0; j < dataCombat.getDefeatedEnemies().Count; j++)
                {
                    if (dataCombat.getDefeatedEnemies()[j] == enemies[i].id)
                    {
                        conditionMet = true;
                    }
                }
            }
        }
        if (conditionMet)
        {
            Cutscene();
        }
    }
    void Cutscene()
    {
        print("condition met");
        cutscene.Play();

        if (!repeatable)
            playCutscene = false;
    }
    public void PauseCutscene()
    {
        cutscene.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void ResumeCutscene()
    {
        cutscene.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
    public void AddScreenShake(CinemachineVirtualCamera camera)
    {
        print("add");
        camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 5;
    }
    public void StopScreenShake(CinemachineVirtualCamera camera)
    {
        print("stop");
        camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
