using System;
using System.IO;
using UnityEngine;

public class PlayerSaveData : DataController {
    private Health health;
    private EquipmentManager equipmentManager;
    private Loadout loadOut;
    private Inventory inventory;
    private PlayerWallet wallet;
    private SavedItem[] myInventory;

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

    public SavedItem[] GetSavedInventory()
    {
        return myInventory;
    }

    protected override void GatherComponents()
    {
        base.GatherComponents();
        health = GetComponent<Health>();
        equipmentManager = GetComponent<EquipmentManager>();
        loadOut = GetComponent<Loadout>();
        inventory = InventoryManager.GetInstance().GetInventory();
        wallet = ScriptToolbox.GetInstance().GetPlayerWallet();
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        Vector3 pos = transform.position;
        data.currentPosition = new SavedPosition { x = pos.x, y = pos.y, z = pos.z };

        data.currentHealth = health.GetCurrentHealth();
        data.currentEquipment = equipmentManager.GetEquipmentNames();
        data.currentInventory = inventory.GetItemInfo();
        data.currentGold = wallet.GetCurrentBalance();
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);
        health.ApplyCurrentHealth(data.currentHealth);
        loadOut.LoadSavedEquipment(data.currentEquipment);
        myInventory = data.currentInventory; //temporarily cache this so InventoryUI can grab it when it is ready
        wallet.LoadSavedBalance(data.currentGold);
    }
}

[Serializable]
public class PlayerData : Data
{
    public float currentHealth;
    public string currentScene;
    public string[] currentEquipment;
    public float currentGold;
    public SavedItem[] currentInventory;
    public SavedPosition currentPosition;
}

[Serializable]
public struct SavedItem
{
    public string fileName;
    public int quantity;
}

[Serializable]
public struct SavedPosition
{
    public float x, y, z;
}