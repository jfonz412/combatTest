using UnityEngine;

public class CheckInventorySpace : MonoBehaviour {

    #region Check for open spaces

    //USED BY SlotClick.cs and SlotClickHelper.cs
    public static bool CheckItem(Item item)
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

    static bool EmptySlots()
    {
        Inventory inv = Inventory.instance;

        for (int i = 0; i < inv.inventorySpace; i++)
        {
            if (inv.items[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    static int CheckOccupiedSpaces(Item item)
    {
        Inventory inv = Inventory.instance;

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
    #endregion
}
