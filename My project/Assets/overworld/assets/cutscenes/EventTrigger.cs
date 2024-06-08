using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] TriggerCutscene cutscene;
    [SerializeField] bool activateEvent = false;
    private void Awake()
    {
        Interactable.TriggerEvent += ActivateEvent;
        TalkTextBox.Reset += Event;
    }
    void ActivateEvent(string Name)
    {
        if (Name == this.name)
            activateEvent = true;
        else
            activateEvent = false;
    }
    public void ResetActivationTrigger()
    {
        activateEvent = false;
    }
    void Event()
    {
        if (activateEvent)
        {
            print("resume cutscene");
            if (cutscene == null)
            {
                cutscene = this.gameObject.GetComponent<TriggerCutscene>();
            }
            cutscene.ResumeCutscene();
        }
    }
    private void OnDisable()
    {
        Interactable.TriggerEvent -= ActivateEvent;
        TalkTextBox.Reset -= Event;
    }
}
