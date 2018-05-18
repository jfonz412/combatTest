using System;
using System.IO;
using UnityEngine;

public class NPCSaveData : DataController {
    private Health health;
    //private EquipmentManager equipmentManager;
    //private Loadout loadOut;

    public override string SaveData()
    {
        base.SaveData();
        NPCData data = PackageNPCData();
        SaveDataToFile(data, GetFileName());
        return GetFileName();
    }

    public override void LoadData()
    {
        base.LoadData();
        if (File.Exists(Application.persistentDataPath + GetFileName()))
        {
            NPCData data = (NPCData)LoadDataFromFile(GetFileName());
            ApplyDataToNPC(data);
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
        //equipmentManager = GetComponent<EquipmentManager>();
        //loadOut = GetComponent<Loadout>();
        //inventory = GetComponent<Inventory>();
    }

    //MAKE THIS OVERRIDE?
    private NPCData PackageNPCData()
    {
        NPCData data = new NPCData();
        data.currentHealth = health.GetCurrentHealth();
        //data.currentEquipment = equipmentManager.GetCurrentEquipment();
        return data;
    }

    //MAKE THIS OVERRIDE?
    private void ApplyDataToNPC(NPCData data)
    {
        health.ApplyCurrentHealth(data.currentHealth);
        //loadOut.LoadEquipment(data.currentEquipment);
    }

    private string GetFileName()
    {
        string filePath = "/NPC_" + gameObject.name + ".dat";
        //UnityEngine.Debug.Log(filePath); //should probably add an ID to this as well
        return filePath;
    }
}

[Serializable]
public class NPCData : Data
{
    public float currentHealth;
    //public Equipment[] currentEquipment;
}