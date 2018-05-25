using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DataManager : MonoBehaviour {
    [SerializeField]
    private List<DataController> dataControllers; //data collected from the current scene
    private List<string> fileNames = new List<string>(); //to track the files to be deleted if created a new game
    private static string savedFilesList = "/savedFiles.dat";
    private string currentScene;

    private void Start()
    {
        SceneManager.activeSceneChanged += ClearControllers; //should hopefully happen before new controllers get loaded...
    }

    private void ClearControllers(Scene arg0, Scene arg1)
    {
        Debug.Log("Creating new dataControllers list");
        dataControllers = new List<DataController>();
    }

    public void LoadDataController(DataController dataController)
    {
        dataControllers.Add(dataController);
        dataController.LoadData();
    }

    //in the future, do not save if player is in combat
    public void SaveAllData()
    {
        //Debug.Log("Save all data " + dataControllers.Count);
        for (int i = 0; i < dataControllers.Count; i++)
        {
            string returnedFile = dataControllers[i].SaveData();
            Debug.Log(returnedFile);
            fileNames.Add(returnedFile);
            //Debug.Log("Saving " + dataControllers[i]);
        }

        //SaveCurrentScene(); //I don't think it's necessary to call this here because the scene is saved everytime we enter a new scene

        //after data is saved, the file name is copied in our list of saved files
        SerializeFileNames();
    }

    public void LoadAllData()
    {
        Debug.Log("Loading data");

        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].LoadData();
        }
    }

    private void SerializeFileNames()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savedFilesList);
        bf.Serialize(file, fileNames);
        file.Close();
    }

    public static List<string> GetSavedFileNames()
    {
        List<string> savedFiles = new List<string>();

        if (File.Exists(Application.persistentDataPath + savedFilesList))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savedFilesList, FileMode.Open);
            savedFiles = (List<string>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogWarning("Player save data not found!");
        }

        return savedFiles;
    }

    public static void DeleteListOfSavedFiles()
    {
        string filePath = Application.persistentDataPath + savedFilesList;

        Debug.LogFormat("#BeforeDeletion - File at {0} exists: {1}", filePath, File.Exists(filePath));
        File.Delete(filePath);
        Debug.LogFormat("#AfterDeletion - File at {0} exists: {1}", filePath, File.Exists(filePath));
    }

    //also called from ExitScene
    public void SaveCurrentScene()
    {
        //in the future, do not save if player is in combat

        currentScene = SceneManager.GetActiveScene().name;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/currentScene.dat");
        bf.Serialize(file, currentScene);
        file.Close();

        Debug.Log("Saving scene as " + currentScene);
    }
}
