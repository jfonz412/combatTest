using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {
    private Button button;
    private string dir;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
        dir = Application.persistentDataPath + "/perm";
    }

    private void LoadScene()
    {
        DeleteTempFiles(); //throw away temp files, revert back to perm files

        if (SavedGameAvailable())
        {
            string scene = RetrieveSavedScene();
            ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene);
            //Debug.Log("Loading: " + scene);
        }
    }

    private string RetrieveSavedScene()
    {
        string scene = GetSceneString();

        if(scene == "")
        {
            //Debug.Log("No saved scene found, loading active scene");
            return SceneManager.GetActiveScene().name;
        }
        else
        {
            //Debug.Log(scene);
            return scene;
        }
    }

    private string GetSceneString()
    {
        string filePath = dir + "/currentScene.dat"; 
        string scene = "";

        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
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
        if (File.Exists(dir + "/playerData.dat"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DeleteTempFiles()
    {
        string tempDir = Application.persistentDataPath + "/temp";

        if (Directory.Exists(tempDir))
        {
            string[] files = Directory.GetFiles(tempDir);

            foreach (string s in files)
            {
                File.Delete(s);
            }
        }
        else
        {
            Debug.Log("tempDir not found");
        }
    }
}
