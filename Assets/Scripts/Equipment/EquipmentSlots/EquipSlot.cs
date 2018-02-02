using UnityEngine.UI;
using UnityEngine;

public class EquipSlot : MonoBehaviour {

    EquipmentManager equipmentManager;
    Equipment equipment;

    public Image icon;
    public Button removeButton;
    

    void Start()
    {
        equipmentManager = PlayerManager.instance.player.GetComponent<EquipmentManager>();
    }

    public void AddItem(Equipment newItem)
    {
        equipment = newItem;
        Debug.Log("new item being added is: " + newItem.icon);
        icon.sprite = equipment.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        equipmentManager.Unequip((int)equipment.equipSlot);
    }

    public void UseItem() 
    {
        if (equipment != null)
        {
            equipment.Use();
        }
    }
}
