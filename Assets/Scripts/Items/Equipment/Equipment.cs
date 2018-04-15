using UnityEngine;

//[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public int equipmentID; //it's ID for the resources folder

    public override void Use()
    {
        //base.Use();
        Transform player = PlayerManager.instance.player.transform;

        Debug.Log("Equipping item");

        //the only entity that will equipping items after loading the scene is the player
        EquipmentManager manager = player.GetComponent<EquipmentManager>();

        manager.FastEquip(this);
    }

    public override void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);
        Instantiate(Resources.Load("EquipmentStats"), InfoPanel.instance.transform.position, Quaternion.identity, InfoPanel.instance.transform);
    }
    
    
}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}