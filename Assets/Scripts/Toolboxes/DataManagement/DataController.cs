using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataController : MonoBehaviour {
    protected static string fileDirectory = "/GameSave";

    void Start()
    {
        GatherComponents();
        ApplicationManager.GetInstance().GetDataManager().LoadDataController(this);
    }

    public virtual string SaveData()
    {
        string fileName = "";
        //Debug.Log("Saving player data");
        return fileName;
    }

    public virtual void LoadData()
    {
        //Debug.Log("Loading " + gameObject.name);
    }

    protected virtual void GatherComponents()
    {
        //Debug.Log("Gathering components");
    }

    protected virtual void ApplyData(Data data) 
    {
        //Debug.Log("Applying data to " + gameObject.name);
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

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
        data = (Data)bf.Deserialize(file);
        file.Close();
        
        return data;
    }
    #endregion
    
}
