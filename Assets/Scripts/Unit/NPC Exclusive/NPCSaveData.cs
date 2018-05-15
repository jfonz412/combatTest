using System;
using System.IO;
using UnityEngine;

public class NPCSaveData : DataController {

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

    //MAKE THIS OVERRIDE?
    private NPCData PackageNPCData()
    {
        NPCData data = new NPCData();
        data.currentHealth = 1000f;
        return data;
    }

    //MAKE THIS OVERRIDE?
    private void ApplyDataToNPC(NPCData data)
    {
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
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