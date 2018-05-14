using System;

public class PlayerSaveData : DataController {
    private string fileName = "/playerData.dat";

    public override void SaveData()
    {
        base.SaveData();
        PlayerData data = PackagePlayerData();
        SaveDataToFile(data, fileName);
    }

    public override void LoadData()
    {
        base.LoadData();
        PlayerData data = (PlayerData)LoadDataFromFile(fileName);
        ApplyDataToPlayer(data);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentHealth = 1000f;
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
    }
}

[Serializable]
public class PlayerData : Data
{
    public float currentHealth;
    public string currentScene;
}