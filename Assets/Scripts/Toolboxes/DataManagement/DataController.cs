using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour {

    void Start()
    {
        Debug.Log(this);
        Debug.Log(ApplicationManager.GetInstance());
        Debug.Log(ApplicationManager.GetInstance().GetDataManager());
        ApplicationManager.GetInstance().GetDataManager().AddToDataManager(this);
    }

    public virtual void SaveData()
    {
        Debug.Log("Saving player data");
    }

    public virtual void LoadData()
    {
        Debug.Log("Loading player data");
    }

    protected virtual void ApplyData(Data data) 
    {
        Debug.Log("Applying data to " + gameObject.name);
    }

    //move this back to DataController base class after figuring out PlayerData : Data inheritance
    #region Serialization
    protected void SaveDataToFile(Data data, string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, data);
        file.Close();
    }

    protected Data LoadDataFromFile(string fileName)
    {
        Data data = new Data();

        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            data = (Data)bf.Deserialize(file);
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
