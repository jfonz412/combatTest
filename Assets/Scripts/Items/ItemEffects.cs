using UnityEngine;

public class ItemEffects : MonoBehaviour {

    public enum ItemEffect { Equip, NA, HealthPotion };

    public static void Use(ItemEffect effect, Item item) //maybe eventually pass unit component refs to effect specific gameobject?
    {
        if (effect == ItemEffect.Equip)
        {
            EquipToPlayer(item);
        }
        else if (effect == ItemEffect.HealthPotion)
        {
            HealthPotion(item);
        }
        else if (effect == ItemEffect.NA)
        {
            return;
        }
    }

    static void EquipToPlayer(Item item) 
    {
        ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<EquipmentManager>().FastEquip(item);
    }

    static void HealthPotion(Item item) 
    {
        Debug.Log("Healing!");
    }
}
