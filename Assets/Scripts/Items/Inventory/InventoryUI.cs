using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onInventoryChanged += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        AssignSlotNums();
    }
	
	// Update is called once per frame
	void Update ()
    {
        InventoryUIToggle();
	}

    void UpdateUI()
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

    void InventoryUIToggle()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            MouseSlot.instance.ToggleSprite(inventoryUI.activeSelf);

            CloseOpenWindows.instance.DestroyPopupMenus();
        }

        if (DialogueManager.instance.isOpen)
        {
            inventoryUI.SetActive(false);
            MouseSlot.instance.ToggleSprite(inventoryUI.activeSelf);
        }
    }

    void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }
}
