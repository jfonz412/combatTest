using UnityEngine;

public class PauseController : MonoBehaviour {
    public GameObject pauseMenu;
    private Brain playerBrain;

    private Brain.State stateBeforePause;

    Brain.State[] invalidPauseStates = new Brain.State[]
    {
        Brain.State.Talking,
        Brain.State.Shopping,
        Brain.State.Prompted
    };

    private void Start()
    {
        playerBrain = ScriptToolbox.GetInstance().GetPlayerManager().playerBrain;
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(CanPause())
                TogglePause();
        }	
	}

    private bool CanPause()
    {
        if (!playerBrain.ActiveStates(invalidPauseStates)) //if any of these are active return false
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void TogglePause()
    {
        bool currentlyPaused = pauseMenu.activeSelf;

        if (currentlyPaused)
        {
            Unpause();
            Debug.Log("Unpausing");
        }
        else
        {
            Pause();
            Debug.Log("Pausing");
        }
    }

    private void Pause()
    {
        playerBrain.ToggleState(Brain.State.Paused, true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        playerBrain.ToggleState(Brain.State.Paused, false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
