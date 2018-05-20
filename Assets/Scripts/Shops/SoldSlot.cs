using UnityEngine.UI;
using UnityEngine;

public class SoldSlot : ShopSlot {
    private Button button;

    private void Start()
    {
        button = transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(InventoryManager.GetInstance().GetShopInventory().ClearLastItemSold);
    }
}
