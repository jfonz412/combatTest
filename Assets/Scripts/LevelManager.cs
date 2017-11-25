using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadScene(string scene){
		Application.LoadLevel(scene);
	}
	
	public void QuitGame(){
		Application.Quit();
	}
}
