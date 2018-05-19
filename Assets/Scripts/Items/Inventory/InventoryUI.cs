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

    //on scene load
    private void LoadInventory()
    {
        //this must happen here because we have to wait for the callback to be loaded
        string[] items = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<PlayerSaveData>().GetCurrentInventory(); 
        for (int i = 0; i < items.Length - 1; i++)
        {
            if (items[i] != null)
            {
                Debug.Log(items[i]);
                inventory.AddItem((Item)Instantiate(Resources.Load(items[i]))); //needs to wait for the callback
            }
        }

    }
}
