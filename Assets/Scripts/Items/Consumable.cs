using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable Item")]
public class Consumable : Item {

    [SerializeField]
    private ItemEffects.ItemEffect myEffect;

    public override void Use()
    {
        base.Use();
        ItemEffects.TriggerEffect(myEffect);
        InventoryManager.GetInstance().GetInventory().CondenseStackables(this, 1);
        Debug.Log("Using " + name);
    }
}
