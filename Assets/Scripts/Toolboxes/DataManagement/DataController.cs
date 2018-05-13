using UnityEngine;

public class DataController : MonoBehaviour {

    void Start()
    {
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
}
