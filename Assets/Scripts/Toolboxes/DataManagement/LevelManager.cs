using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static List<ExitScene> sceneExits;

    private void Start()
    {
        //clear list from last scene
        sceneExits = null;
        sceneExits = new List<ExitScene>();
    }
	public void LoadScene(string scene) //LoadSceneMidGame
    {
        //SaveData (autosave) if not loading save from title or menu
		SceneManager.LoadScene(scene);
        //LoadData

        //or have DataManager handle saving a loading on scene load? delegate a listener via that class?
    }
	
    public void LoadFromTitleScreen() 
    {
        //deserialize playerData to get scene string
        //LoadScene(sceneString)
    }

	public void QuitGame(){
		Application.Quit();
	}
}
