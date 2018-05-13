using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerSaveData : DataController {
    private string fileName = "/playerData.dat";

    public override void SaveData()
    {
        base.SaveData();
        PlayerData data = PackagePlayerData();
        SaveDataToFile(data, fileName);
    }

    public override void LoadData()
    {
        base.LoadData();
        PlayerData data = LoadDataFromFile(fileName);
        ApplyDataToPlayer(data);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentHealth = 1000f;
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
    }

    #region Serialization
    private void SaveDataToFile(PlayerData data, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, data);
        file.Close();
    }

    private PlayerData LoadDataFromFile(string fileName)
    {
        PlayerData data = new PlayerData();

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
    #endregion
}

[Serializable]
public struct PlayerData
{
    public float currentHealth;
    public string currentScene;
}