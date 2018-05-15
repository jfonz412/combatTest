using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveData : DataController {
    private string fileName = "/playerData.dat";

    public override string SaveData()
    {
        base.SaveData();
        PlayerData data = PackagePlayerData();
        SaveDataToFile(data, fileName);
        return fileName;
    }

    public override void LoadData()
    {
        base.LoadData();
        if(File.Exists(Application.persistentDataPath + fileName))
        {
            PlayerData data = (PlayerData)LoadDataFromFile(fileName);
            ApplyDataToPlayer(data);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
        }
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentHealth = 1000f;
        data.currentScene = SceneManager.GetActiveScene().name; //maybe make this listen for scene change and make it a class var?
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
    }
}

[Serializable]
public class PlayerData : Data
{
    public float currentHealth;
    public string currentScene;
}