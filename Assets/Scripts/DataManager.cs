using UnityEngine;

public class DataManager : MonoBehaviour {
    public PlayerSaveData ps;

    //DEBUGGING
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Attempting to save data");
            ps.SaveData();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Attempting to load data");
            ps.LoadData();
        }
    }

    //this class will call all saved objects to save and load on demand
}
