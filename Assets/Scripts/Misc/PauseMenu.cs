using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
