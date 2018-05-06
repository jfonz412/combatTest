using System;
using UnityEngine;

public class PlayerSaveData : MonoBehaviour {
    private DataManager dataManager;

	// Use this for initialization
	void Start () {
        dataManager = ApplicationManager.GetInstance().GetDataManager();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SavePlayerData();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LoadPlayerData();
        }
    }
	
    public void SavePlayerData()
    {
        Debug.Log("Saving player data");
        PlayerData data = PackagePlayerData();
        dataManager.SavePlayerData(data);
    }

    public void LoadPlayerData()
    {
        Debug.Log("Loading player data");
        PlayerData data = dataManager.LoadPlayerData();
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentHealth = 1000f;
        return data;
    }
}

[Serializable]
public struct PlayerData
{
    public float currentHealth;
}