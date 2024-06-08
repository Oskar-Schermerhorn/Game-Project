using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] bool playCutscene = true;
    [SerializeField] bool repeatable = false;
    [SerializeField] PlayableDirector cutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && playCutscene)
        {
            Cutscene();
        }
    }
    void Cutscene()
    {
        cutscene.Play();
        if (!repeatable)
            playCutscene = false;
    }
    public void PauseCutscene()
    {
        print("root playable count: " +cutscene.playableGraph.GetRootPlayableCount());
        cutscene.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void ResumeCutscene()
    {
        
        if (cutscene.playableGraph.IsValid())
        {
            cutscene.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
        else
        {
            cutscene.Play();
        }
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
