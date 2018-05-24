using UnityEngine;

public class PauseController : MonoBehaviour {
    public GameObject pauseMenu;

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            ToggleTime();
        }	
	}

    private void ToggleTime()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
