using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;

    Inventory inventory;
    InventorySlot[] slots;

	// Use this for initialization
	void Start ()
    {
        inventory = InventoryManager.GetInstance().GetInventory();
        inventory.onInventoryChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        AssignSlotNums();

        LoadInventory();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    private void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }

    //on scene load, load saved inventory
    private void LoadInventory()
    {
        //this must happen here because we have to wait for the callback to be loaded
        PlayerSaveData playerData = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<PlayerSaveData>();
        SavedItem[] items = playerData.GetSavedInventory();

        if (items == null)
            return;

        for (int i = 0; i < items.Length - 1; i++)
        {
            if (items[i].fileName == null)
                continue;

            //Debug.Log("Loading " + items[i].fileName + " from saved invetory items" );

            //inventory will instantiate this if it doesn't have a slotNum, which I might eventually want to save but for now it's fine since
            //items should be added in order
            Item item  = (Item)Resources.Load(items[i].fileName); //needs to wait for the callback
            item.quantity = items[i].quantity;
            inventory.AddItem(item);
        }

    }
}
