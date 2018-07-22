using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class NPCSaveData : DataController {
    //the components we need to save
    private Loadout loadOut; //just to load npc's default equipment when scene is loaded
    private LoadShop myShop;
    //private UnitReactions myAI;
    private BodyPartController bodyParts;
    private Brain myBrain;

    //called in Start() 
    protected override void GatherComponents()
    {
        base.GatherComponents();
        loadOut = GetComponent<Loadout>();
        //myAI = GetComponent<UnitReactions>();
        myShop = GetComponent<LoadShop>(); //may be null if not shop owner
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
            loadOut.EquipLoadout();
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

        if (myShop != null)
        {
            data.currentShopInventory = myShop.GetCurrentInventory();
        }

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

        if (myShop != null)
        {
            myShop.UpdateInventory(UnpackSavedShopInventory(data.currentShopInventory));
        }

        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);
        bodyParts.LoadSavedParts(data.bodyParts);
        loadOut.EquipLoadout();
    }

    //creates file path for this individual based on it's gameObject.name
    private string GetFileName()
    {
        string filePath = "/NPC_" + gameObject.name + ".dat";
        return filePath;
    }


    private List<Item> UnpackSavedShopInventory(SavedItem[] savedInventory)
    {
        //Debug.Log("Unpacking shop inventory for " + gameObject.name);
        List<Item> loadedItems = new List<Item>();

        if (savedInventory == null)
        {
            //Debug.Log("savedInventory is null for " + gameObject.name);
            return loadedItems;
        }
            

        for (int i = 0; i < savedInventory.Length; i++)
        {
            if (savedInventory[i].fileName == null)
                continue;

            // this needs to be instantiated here because we apply the quantity
            //Item item = (Item)Instantiate(Resources.Load(savedInventory[i].fileName)); 
            //item.quantity = savedInventory[i].quantity;

            //loadedItems.Add(item);
            Debug.Log("Method is returning null list for shop inventory, need to update this method");

        }
        return loadedItems;
    }
}

[Serializable]
public class NPCData : Data
{
    public SavedPosition currentPosition;
    //public float currentHealth;
    public SavedItem[] currentShopInventory;
    public bool isDead;
    public BodyPart.PartInfo[] bodyParts;
}