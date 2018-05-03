using UnityEngine;

public class CheckInventorySpace : MonoBehaviour {

    private Inventory inv;

    void Start()
    {
        inv = InventoryManager.GetInstance().GetInventory();
    }

    //USED BY SlotClick.cs and SlotClickHelper.cs
    public bool CheckItem(Item item)
    {
        int leftovers = CheckOccupiedSpaces(item);

        if (leftovers == 0)
        {
            Debug.Log("Item will fit (CheckOccupiedSpaces()");
            return true;
        }
        else if (EmptySlots())
        {
            Debug.Log("Item will fit (EmpytSlots())");
            return true;
        }
        else
        {
            Debug.Log("Item will NOT fit");
            return false;
        }
    }

    bool EmptySlots()
    {
        for (int i = 0; i < inv.inventorySpace; i++)
        {
            if (inv.items[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    int CheckOccupiedSpaces(Item item)
    {

        int newItemQ = item.quantity;
        for (int i = 0; i < inv.inventorySpace; i++)
        {
            if (inv.items[i] != null)
            {
                if (inv.items[i].name == item.name)
                {
                    int q = inv.items[i].quantity;
                    int maxQ = inv.items[i].maxQuantity;

                    while (q != maxQ && newItemQ != 0)
                    {
                        q++;
                        newItemQ--;
                    }
                }
            }
        }
        return newItemQ;
    }
}
