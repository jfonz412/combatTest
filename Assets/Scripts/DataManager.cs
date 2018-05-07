using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    [SerializeField]
    private List<Data> saveData; //data collected from the current scene

    //DEBUGGING
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Attempting to save data");
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Attempting to load data");
            LoadData();
        }
    }

    public void AddToDataManager(Data data)
    {
        saveData.Add(data);
    }

    //will eventually make these public
    private void SaveData()
    {
        for (int i = 0; i < saveData.Count; i++)
        {
            saveData[i].SaveData();
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < saveData.Count; i++)
        {
            saveData[i].LoadData();
        }
    }
}
