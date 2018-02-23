using UnityEngine;

//[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public int equipmentID; //it's ID for the resources folder

    public override void Use()
    {
        base.Use(); //gets reference to the player
        OpenStatWindow();
    }

    public virtual void OpenStatWindow()
    {   
        Vector3 spawnPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        spawnPoint.y += 1f;
        spawnPoint.z = 0f;
        Instantiate(Resources.Load("EquipmentStats"), spawnPoint, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        Debug.Log("Opening equipment stat window");
    }
    
    
}

public enum EquipmentSlot {Head, Chest, Legs, MainHand, OffHand, Feet}