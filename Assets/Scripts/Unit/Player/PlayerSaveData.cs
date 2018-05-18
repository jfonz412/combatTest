using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        data.currentScene = SceneManager.GetActiveScene().name; //maybe make this listen for scene change and make it a class var?
        //inventory
        //equipment (loadout)
        //position

        data.currentHealth = health.GetCurrentHealth();
        data.currentEquipment = GetEquipmentIDs();
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        health.ApplyCurrentHealth(data.currentHealth);
        loadOut.LoadEquipment(data.currentEquipment);
    }

    private int[] GetEquipmentIDs()
    {
        Equipment[] equipment = equipmentManager.GetCurrentEquipment();

        //should be a dictionary so I can store the ID and condition...or a struct that can store all kinds of data
        int[] equipmentIDs = new int[6];

        for (int i = 0; i < equipment.Length; i++)
        {
            equipmentIDs[i] = equipment[i].equipmentID;
        }

        return equipmentIDs;
    }
}

[Serializable]
public class PlayerData : Data
{
    public float currentHealth;
    public string currentScene;
    public int[] currentEquipment;
    public Inventory[] currentInventory;
}