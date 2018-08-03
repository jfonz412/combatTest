using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSaveData : DataController {

    private HealthDoll healthDoll;
    private EquipmentManager equipmentManager;
    private Inventory inventory;
    private PlayerWallet wallet;
    private BodyPartController myBody;
    private CombatSkills combatSkills;

    private string fileName = "/playerData.dat";

    protected override void GatherComponents()
    {
        base.GatherComponents();
        equipmentManager = GetComponent<EquipmentManager>();
        inventory = InventoryManager.GetInstance().GetInventory();
        wallet = ScriptToolbox.GetInstance().GetPlayerWallet();
        myBody = GetComponent<BodyPartController>();
        combatSkills = GetComponent<CombatSkills>();

        healthDoll = FindObjectOfType<HealthDoll>(); //should only be one
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
            Debug.LogWarning(gameObject.name + " save data not found!");
            return;
        }

        ApplyDataToPlayer(data);
    }

    private PlayerData PackagePlayerData()
    {
        PlayerData data = new PlayerData();
        //data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; //is this old code??

        Vector3 pos = transform.position;
        data.currentPosition = new SavedPosition { x = pos.x, y = pos.y, z = pos.z };

        data.currentInventory = inventory.GetInventoryItems();
        data.currentGold = wallet.GetCurrentBalance();
        data.bodyParts = myBody.GetBodyParts();
        //data.injuryList = healthDoll.SaveInjuryLog();

        data.combatSkillLevels = combatSkills.GetCombatLevels();
        data.combatSkillExperience = combatSkills.GetCombatExperience();
        data.weaponSkillLevels = combatSkills.GetWeaponLevels();
        data.weaponSkillExperience = combatSkills.GetWeaponExperience();
        return data;
    }

    private void ApplyDataToPlayer(PlayerData data)
    {

        inventory.LoadSavedItems(data.currentInventory);
        wallet.LoadSavedBalance(data.currentGold);
        transform.position = new Vector3(data.currentPosition.x, data.currentPosition.y, data.currentPosition.z);

        myBody.LoadSavedParts(data.bodyParts);
        equipmentManager.LoadSavedEquipment(data.bodyParts);

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
    //public List<EquipmentInfo> currentEquipment;
    public List<Item> currentInventory;
    public float currentGold;
    public SavedPosition currentPosition;
    public BodyPart.PartInfo[] bodyParts;
    public Dictionary<CombatSkills.CombatSkill, int> combatSkillLevels;
    public Dictionary<CombatSkills.CombatSkill, float> combatSkillExperience;
    public Dictionary<Item.WeaponSkill, int> weaponSkillLevels;
    public Dictionary<Item.WeaponSkill, float> weaponSkillExperience;
    //public Dictionary<BodyPart, List<string>> injuryList;
}

[Serializable]
public struct SavedPosition
{
    public float x, y, z;
}