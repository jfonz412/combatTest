using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class NewGame : MonoBehaviour {
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DeleteSaveData); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
    }

    private void DeleteSaveData()
    {
        List<string> savedFileNames = DataManager.GetSavedFileNames();
     
        for(int i = 0; i < savedFileNames.Count; i++)
        {
            Delete(savedFileNames[i]);
        }

        DataManager.DeleteListOfSavedFiles();
        ApplicationManager.GetInstance().GetLevelManager().LoadScene("Demoburgh");
    }

    private void Delete(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;

        Debug.LogFormat("#BeforeDeletion - File at {0} exists: {1}", filePath, File.Exists(filePath));
        File.Delete(filePath);
        Debug.LogFormat("#AfterDeletion - File at {0} exists: {1}", filePath, File.Exists(filePath));
    }
}
