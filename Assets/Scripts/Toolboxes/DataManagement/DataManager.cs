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

    //DEBUGGING
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Attempting to save data");
            SaveAllData();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Attempting to load data");
            LoadAllData();
        }
    }

    public void Load(DataController dataController)
    {
        dataControllers.Add(dataController);
        dataController.LoadData();
    }

    //will eventually make these public
    private void SaveAllData()
    {
        for (int i = 0; i < dataControllers.Count; i++)
        {
            string returnedFile = dataControllers[i].SaveData();
            Debug.Log(returnedFile);
            fileNames.Add(returnedFile);
        }

        //after data is saved, the file name is copied in our list of saved files
        SerializeFileNames();
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

    private void LoadAllData()
    {
        Debug.Log("Loading data");

        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].LoadData();
        }
    }
}
