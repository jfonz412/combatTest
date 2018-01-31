using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        //whenever onInventoryChanged is Invoked, it will call UpdateUI from this script
        inventory.onInventoryChanged += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>(); //might want to put this in UpdateUI if slots change
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory")) 
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}

    void UpdateUI()
    {
        Debug.Log("UPDATING UI"); 
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                //Debug.Log("Added item to slot");
            }
            else
            {
                slots[i].ClearSlot();
                //Debug.Log("Cleared item from slot");
            }
        }
    }
}
