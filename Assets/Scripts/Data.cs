using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Data : MonoBehaviour {

    public virtual void SaveData()
    {
        Debug.Log("Saving player data");
    }

    public virtual void LoadData()
    {
        Debug.Log("Loading player data");
    }

    public virtual void SaveDataToFile(PlayerData data, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, data);
        file.Close();
    }

    public virtual PlayerData LoadDataFromFile(string fileName)
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
}

