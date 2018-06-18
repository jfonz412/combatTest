using UnityEngine;

public class PauseController : MonoBehaviour {
    public GameObject pauseMenu;
    private PlayerState.PlayerStates stateBeforePause;

    PlayerState.PlayerStates[] invalidPauseStates = new PlayerState.PlayerStates[]
    {
        PlayerState.PlayerStates.Speaking,
        PlayerState.PlayerStates.Shopping,
        PlayerState.PlayerStates.Prompt
    };


    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!PlayerState.CheckPlayerState(invalidPauseStates))
                TogglePause();
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
        stateBeforePause = PlayerState.GetPlayerState();
        PlayerState.SetPlayerState(PlayerState.PlayerStates.Paused); 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        PlayerState.SetPlayerState(stateBeforePause);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
