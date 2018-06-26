using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSaveData : DataController {
    private Health health;
    private EquipmentManager equipmentManager;
    private Loadout loadOut;
    private Inventory inventory;
    private PlayerWallet wallet;
    private BodyParts bodyParts;
    private CombatSkills combatSkills;

    private string fileName = "/playerData.dat";

    protected override void GatherComponents()
    {
        base.GatherComponents();
        health = GetComponent<Health>();
        equipmentManager = GetComponent<EquipmentManager>();
        loadOut = GetComponent<Loadout>();
        inventory = InventoryManager.GetInstance().GetInventory();
        wallet = ScriptToolbox.GetInstance().GetPlayerWallet();
        bodyParts = GetComponent<BodyParts>();
        combatSkills = GetComponent<CombatSkills>();
    }

    public override string SaveData()
    {
        base.SaveData();
        PlayerData data = PackagePlayerData();
        SaveDataToFile(data, tempDirectory + fileName);
        return fileName;
    }

    //we attempt to load temp data first, because temp data is deleted after a manual save
    public override void LoadData()
    {
        base.LoadData();
        PlayerData data;

        wallet.LoadSavedBalance(); //to display wallet amount on new game, otherwise this is only called in ApplyPlayerData()

        if (File.Exists(Application.persistentDataPath + tempDirectory + fileName))
        {
            data = (PlayerData)LoadDataFromFile(tempDirectory + fileName);
        }
        else if(File.Exists(Application.persistentDataPath + permDirectory + fileName))
        {
            Debug.Log("Loading perm Player");
            data = (PlayerData)LoadDataFromFile(permDirectory + fileName);
        }
        else
        {
            loadOut.EquipLoadout(); //will load player's default loadout so we don't have null equipment
            Debug.LogWarning(gameObject.name + " save data not found!");
            return;
        }

        ApplyDataToPlayer(data);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        //data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        Vector3 pos = transform.position;
        data.currentPosition = new SavedPosition { x = pos.x, y = pos.y, z = pos.z };

        //data.currentHealth = health.GetCurrentHealth();
        data.currentEquipment = equipmentManager.GetEquipmentInfo();
        data.currentInventory = inventory.GetItemInfo();
        data.currentGold = wallet.GetCurrentBalance();
        data.bodyPartHealth = bodyParts.GetBodyPartHealth();

        data.combatSkillLevels = combatSkills.GetCombatLevels();
        data.combatSkillExperience = combatSkills.GetCombatExperience();
        data.weaponSkillLevels = combatSkills.GetWeaponLevels();
        data.weaponSkillExperience = combatSkills.GetWeaponExperience();
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {
        equipmentManager.LoadSavedEquipment(data.currentEquipment);
        inventory.LoadSavedItems(data.currentInventory);
        wallet.LoadSavedBalance(data.currentGold);
        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);
        bodyParts.LoadBodyPartHealth(data.bodyPartHealth);
        combatSkills.LoadSavedCombatLevels(data.combatSkillLevels);
        combatSkills.LoadSavedCombatExperience(data.combatSkillExperience);
        combatSkills.LoadSavedWeaponLevels(data.weaponSkillLevels);
        combatSkills.LoadSavedWeaponExperience(data.weaponSkillExperience);

        Debug.Log("Applied player data");
    }
}

[Serializable]
public class PlayerData : Data
{
    //public float currentHealth;
    public List<EquipmentInfo> currentEquipment;
    public float currentGold;
    public SavedItem[] currentInventory;
    public SavedPosition currentPosition;
    public Dictionary<BodyParts.Parts, float> bodyPartHealth;
    public Dictionary<CombatSkills.CombatSkill, int> combatSkillLevels;
    public Dictionary<CombatSkills.CombatSkill, float> combatSkillExperience;
    public Dictionary<Weapon.WeaponType, int> weaponSkillLevels;
    public Dictionary<Weapon.WeaponType, float> weaponSkillExperience;
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