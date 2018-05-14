using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    [SerializeField]
    private List<DataController> dataControllers; //data collected from the current scene

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

    public void AddToDataManager(DataController dataController)
    {
        dataControllers.Add(dataController);
    }

    //will eventually make these public
    private void SaveData()
    {
        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].SaveData();
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < dataControllers.Count; i++)
        {
            dataControllers[i].LoadData();
        }
    }
}
