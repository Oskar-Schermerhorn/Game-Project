using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField] bool playCutscene = true;
    [SerializeField] bool repeatable = false;
    [SerializeField] PlayableDirector cutscene;

    private void Awake()
    {
        Rooms.ActiveRoom += playIntro;
    }
    void playIntro(bool active,GameObject room)
    {
        if(this.transform.IsChildOf(room.transform) && active && playCutscene)
        {
            cutscene.Play();
            
            if (!repeatable)
                playCutscene = false;
        }
    }
    private void OnDisable()
    {
        Rooms.ActiveRoom -= playIntro;
    }
}
