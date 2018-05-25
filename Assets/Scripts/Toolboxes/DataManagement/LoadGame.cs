using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
    }

    private void LoadScene()
    {
        if (SavedGameAvailable())
        {
            string scene = RetrieveSavedScene();
            Time.timeScale = 1; //in case we are coming from the pause screen
            ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene);
            Debug.Log("Loading: " + scene);
        }
    }

    private string RetrieveSavedScene()
    {
        string scene = GetSceneString();

        if(scene == "")
        {
            Debug.Log("No saved scene found, loading active scene");
            return SceneManager.GetActiveScene().name;
        }
        else
        {
            return scene;
        }
    }

    private string GetSceneString()
    {
        string fileName = "/currentScene.dat";
        string scene = "";

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            scene = (string)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogWarning("Save Scene file not found!");
        }

        return scene;
    }

    private bool SavedGameAvailable()
    {
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
