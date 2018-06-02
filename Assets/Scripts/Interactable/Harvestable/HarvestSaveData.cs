using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class HarvestSaveData : DataController {
    private Harvestable harvestable;

    //called from DataManager to save this object
    public override string SaveData()
    {
        base.SaveData();
        Debug.Log("saving harvestable");
        HarvestableData data = PackageHarvestableData();
        SaveDataToFile(data, tempDirectory + GetFileName());
        return GetFileName();
    }

    //called from DataManager to load this object
    public override void LoadData()
    {
        base.LoadData();

        HarvestableData data;
        string fileName = GetFileName();

        if (File.Exists(Application.persistentDataPath + tempDirectory + fileName))
        {
            Debug.Log("Loading temp " + gameObject.name);
            data = (HarvestableData)LoadDataFromFile(tempDirectory + fileName);

        }
        else if (File.Exists(Application.persistentDataPath + permDirectory + fileName))
        {
            Debug.Log("Loading perm " + gameObject.name);
            data = (HarvestableData)LoadDataFromFile(permDirectory + fileName);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
            return;
        }

        ApplyDataToHarvestable(data);
    }


    //creates file path for this individual based on it's gameObject.name
    private string GetFileName()
    {
        string filePath = "/Harvestable_" + gameObject.name + ".dat";
        return filePath;
    }


    private HarvestableData PackageHarvestableData()
    {
        Debug.Log("packaging harvestable");
        HarvestableData data = new HarvestableData();
        data.isHarvested = harvestable.isHarvested;
        return data;
    }

    private void ApplyDataToHarvestable(HarvestableData data)
    {
        Debug.Log("applying data to harvestable");

        if (data.isHarvested)
        {
            Debug.Log("this node was harvested: " + gameObject.name);
            harvestable.isHarvested = true;
            gameObject.SetActive(false);
        }
    }

    protected override void GatherComponents()
    {
        base.GatherComponents();
        harvestable = GetComponent<Harvestable>();
    }
}

[Serializable]
public class HarvestableData : Data
{
    public bool isHarvested;
}