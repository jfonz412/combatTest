using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DataManager : MonoBehaviour {
    [SerializeField]
    private List<DataController> dataControllers; //data collected from the current scene
    private string permDir;
    private string tempDir;
    private string currentScene;

    private void Start()
    {
        SceneManager.activeSceneChanged += ClearControllers; //counting on this happening before new controllers get loaded, put in awake if issues
        permDir = Application.persistentDataPath + "/perm";
        tempDir = Application.persistentDataPath + "/temp";
        Directory.CreateDirectory(permDir);
        Directory.CreateDirectory(tempDir);
    }

    private void ClearControllers(Scene arg0, Scene arg1)
    {
        //Debug.Log("Creating new dataControllers list");
        dataControllers = new List<DataController>();
    }

    public void LoadDataController(DataController dataController)
    {
        dataControllers.Add(dataController);
        dataController.LoadData();
    }

    public void SaveDataToTemp()
    {
        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].SaveData();
        }
    }

    //this works as long as temp and perm are flat files, would need to dig deeper (recursivly) to get into more directories
    public void SaveToPerm()
    {
        //copy files from temp dir into perm dir
        SaveDataToTemp();
  
        if (Directory.Exists(tempDir))
        {
            string[] files = Directory.GetFiles(tempDir);
            string fileName;
            string destFile;

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                fileName = Path.GetFileName(s);
                destFile = Path.Combine(permDir, fileName);
                File.Copy(s, destFile, true);
                File.Delete(s);
            }
        }
        else
        {
            Debug.Log("tempDir not found");
        }
    }

    public void LoadAllData()
    {
        Debug.Log("Loading data");

        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].LoadData();
        }
    }

    //called from ExitScene
    public void SaveCurrentScene()
    {
        //in the future, do not save if player is in combat

        currentScene = SceneManager.GetActiveScene().name;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(tempDir + "/currentScene.dat");
        bf.Serialize(file, currentScene);
        file.Close();

        //Debug.Log("Saving scene as " + currentScene);
    }
}
