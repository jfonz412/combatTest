using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static List<ExitScene> sceneExits; // = new List<ExitScene>();

    private void Start()
    {
        sceneExits = new List<ExitScene>();

        ExitScene[] exits = FindObjectsOfType<ExitScene>();
        for (int i = 0; i < exits.Length; i++)
        {
            sceneExits.Add(exits[i]);
        }
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
