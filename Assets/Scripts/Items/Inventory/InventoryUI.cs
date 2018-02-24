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
	void Update () {
        if (Input.GetButtonDown("Inventory")) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            ToggleMouseSlotSprite();

            CloseOpenWindows.instance.DestroyPopupMenus();
        }

        if (DialogueManager.instance.isOpen)
        {
            inventoryUI.SetActive(false);
            ToggleMouseSlotSprite();
        }
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

    void AssignSlotNums()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }

    //public for CloseOpenWindows.cs , consider moving it there?
    public void ToggleMouseSlotSprite()
    {
        MouseSlot mouseSlot = MouseSlot.instance;

        if (inventoryUI.activeSelf == false)
        {
            mouseSlot.spriteRenderer.sprite = null;
        }
        else
        {
            if (mouseSlot.currentItem != null)
                mouseSlot.spriteRenderer.sprite = mouseSlot.currentItemSprite;
        }
    }
}
