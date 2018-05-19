using System;
using System.IO;
using UnityEngine;

public class PlayerSaveData : DataController {
    private Health health;
    private EquipmentManager equipmentManager;
    private Loadout loadOut;
    //private Inventory inventory;

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
            Debug.Log("Loading Player");
            PlayerData data = (PlayerData)LoadDataFromFile(fileName);
            ApplyDataToPlayer(data);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
        }
    }

    protected override void GatherComponents()
    {
        base.GatherComponents();
        health = GetComponent<Health>();
        equipmentManager = GetComponent<EquipmentManager>();
        loadOut = GetComponent<Loadout>();
        //inventory = GetComponent<Inventory>();
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; 

        data.currentHealth = health.GetCurrentHealth();
        data.currentEquipment = equipmentManager.GetEquipmentNames();
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        health.ApplyCurrentHealth(data.currentHealth);
        loadOut.LoadSavedEquipment(data.currentEquipment);
    }
}

[Serializable]
public class PlayerData : Data
{
    public float currentHealth;
    public string currentScene;
    public string[] currentEquipment;
    public Inventory[] currentInventory;
}