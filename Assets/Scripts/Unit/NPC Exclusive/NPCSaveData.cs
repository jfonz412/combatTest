using System;
using System.IO;
using UnityEngine;

public class NPCSaveData : DataController
{
    private BodyPartController bodyParts;
    private Brain myBrain;

    //called in Start() 
    protected override void GatherComponents()
    {
        base.GatherComponents();
        bodyParts = GetComponent<BodyPartController>();
        myBrain = GetComponent<Brain>();
    }

    //called from DataManager to save this object
    public override string SaveData()
    {
        base.SaveData();
        NPCData data = PackageNPCData();
        SaveDataToFile(data, tempDirectory + GetFileName());
        return GetFileName();
    }

    //called from DataManager to load this object
    public override void LoadData()
    {
        base.LoadData();

        NPCData data;
        string fileName = GetFileName();

        if (File.Exists(Application.persistentDataPath + tempDirectory + fileName))
        {
            //Debug.Log("Loading temp " + gameObject.name);
            data = (NPCData)LoadDataFromFile(tempDirectory + fileName);

        }
        else if (File.Exists(Application.persistentDataPath + permDirectory + fileName))
        {
            //Debug.Log("Loading perm " + gameObject.name);
            data = (NPCData)LoadDataFromFile(permDirectory + fileName);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " save data not found!");
            return;
        }

        ApplyDataToNPC(data);
    }

    //Called by Save() to grab data from componets and put that in the Data class
    private NPCData PackageNPCData()
    {
        NPCData data = new NPCData();

        Vector3 pos = transform.position;
        data.currentPosition = new SavedPosition { x = pos.x, y = pos.y, z = pos.z };
        data.isDead = myBrain.isDead;
        data.bodyParts = bodyParts.GetBodyParts();

        return data;
    }

    //called by load to apply Data to components
    private void ApplyDataToNPC(NPCData data)
    {
        //eventually just destroy the object and remove it from the dataManager
        if (data.isDead)
        {
            myBrain.isDead = true;
            gameObject.SetActive(false);
            return;
        }

        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);
        bodyParts.LoadSavedParts(data.bodyParts);
    }

    //creates file path for this individual based on it's gameObject.name
    private string GetFileName()
    {
        string filePath = "/NPC_" + gameObject.name + ".dat";
        return filePath;
    }
}
[Serializable]
public class NPCData : Data
{
    public SavedPosition currentPosition;
    public bool isDead;
    public BodyPart.PartInfo[] bodyParts;
}