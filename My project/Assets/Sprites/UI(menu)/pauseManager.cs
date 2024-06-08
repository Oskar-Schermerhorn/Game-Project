using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { PAUSED, UNPAUSED};
public class pauseManager : MonoBehaviour
{
    MoveScript player;
    pauseMenuNav pausePanel;
    public BattleInputs controls;
    
    private void Awake()
    {
        controls = new BattleInputs();
        PlayerPause.PauseGame += Pause;
        controls.Overworld.Cancel.started +=_ => { Cancel(); };
        controls.Overworld.Pause.started +=_ => { Unpause(); };
        controls.Overworld.NavigateMenuPages.started +=context => { Shift(context.ReadValue<float>()); };
        pausePanel = GameObject.FindObjectOfType<pauseMenuNav>(true);
        player = GameObject.Find("Player").GetComponent<MoveScript>();
    }
    public void Pause()
    {
        controls.Enable();
        pausePanel.gameObject.SetActive(true);
        pausePanel.resetTabs();
        //pausePanel.GetComponent<pauseMenuNav>().setupMod();
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        controls.Disable();
        pausePanel.resetModDisplay();
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        player.EnableControl();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }
    void Shift(float direction)
    {
        if (direction < 0)
        {
            pausePanel.prevTab();
        }
        else
            pausePanel.nextTab();
    }
    void Cancel()
    {
        pausePanel.cancel();
    }
    private void OnDisable()
    {
        PlayerPause.PauseGame -= Pause;
        controls.Overworld.Cancel.started -= _ => { Cancel(); };
        controls.Overworld.Pause.started -= _ => { Unpause(); };
        controls.Overworld.NavigateMenuPages.started -= context => { Shift(context.ReadValue<float>()); };
    }
}
