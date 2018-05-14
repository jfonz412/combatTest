using System;

public class NPCSaveData : DataController {

    public override void SaveData()
    {
        base.SaveData();

        NPCData data = PackageNPCData();
        SaveDataToFile(data, GetFileName());
    }

    public override void LoadData()
    {
        base.LoadData();
        NPCData data = (NPCData)LoadDataFromFile(GetFileName());
        ApplyDataToNPC(data);
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
        UnityEngine.Debug.Log("/NPC_" + gameObject.name + ".dat"); //should probably add an ID to this as well
        return "/NPC_" + gameObject.name + ".dat";
    }
}

[Serializable]
public class NPCData : Data
{
    public float currentHealth;
}