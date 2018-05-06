using System;

public class PlayerSaveData : Data {
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
        PlayerData data = LoadDataFromFile(fileName);
        Health playerHealth = GetComponent<Health>();
        playerHealth.ExternalHealthAdjustment(data.currentHealth);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        data.currentHealth = 1000f;
        return data;
    }
}

[Serializable]
public struct PlayerData
{
    public float currentHealth;
}