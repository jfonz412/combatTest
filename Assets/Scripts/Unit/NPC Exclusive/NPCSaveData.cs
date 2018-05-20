using System;
using System.IO;
using UnityEngine;

public class NPCSaveData : DataController {
    private Health health;
    private Loadout loadOut;

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

        loadOut.EquipLoadout();
    }

    protected override void GatherComponents()
    {
        base.GatherComponents();
        health = GetComponent<Health>();
        loadOut = GetComponent<Loadout>();
    }

    //MAKE THIS OVERRIDE?
    private NPCData PackageNPCData()
    {
        NPCData data = new NPCData();
        data.currentHealth = health.GetCurrentHealth();
        return data;
    }

    //MAKE THIS OVERRIDE?
    private void ApplyDataToNPC(NPCData data)
    {
        health.ApplyCurrentHealth(data.currentHealth);
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
}