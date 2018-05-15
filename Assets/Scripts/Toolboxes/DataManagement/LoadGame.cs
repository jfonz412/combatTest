﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadGame : MonoBehaviour {
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene); //(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
    }

    private void LoadScene()
    {
        string scene = RetrieveSceneFromPlayerData();
        ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene);
        Debug.Log("Loading: " + scene);
    }

    private string RetrieveSceneFromPlayerData()
    {
        PlayerData data = UnpackPlayerData();
        if(data.currentScene != null)
        {
            return data.currentScene;
        }
        else
        {
            Debug.Log("No player data found");
            return "Options";
        }
    }

    private PlayerData UnpackPlayerData()
    {
        PlayerData data = new PlayerData();
        string fileName = "/playerData.dat";

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            data = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogWarning("Player save data not found!");
        }

        return data;
    }
}
