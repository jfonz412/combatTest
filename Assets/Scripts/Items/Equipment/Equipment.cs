using UnityEngine;

//[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public int equipmentID; //it's ID for the resources folder

    public override void Use()
    {
        base.Use();

        Debug.Log("Equipping item");

        //the only entity that will equipping items after loading the scene is the player
        EquipmentManager manager = player.GetComponent<EquipmentManager>();

        manager.FastEquip(this);
    }

    public override void OpenStatWindow()
    {
        Vector3 spawnPoint = WindowSpawnPoint();
        Instantiate(Resources.Load("EquipmentStats"), spawnPoint, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        Debug.Log("Opening equipment stat window");
    }
    
    
}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}