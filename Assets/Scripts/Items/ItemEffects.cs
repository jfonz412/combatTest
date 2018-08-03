using UnityEngine;

public class ItemEffects : MonoBehaviour {

    public enum ItemEffect { Equip };

    public static void Use(ItemEffect effect, Item item)
    {
        if (effect == ItemEffect.Equip)
        {
            EquipToPlayer(item);
        }
    }

    static void EquipToPlayer(Item item) //maybe eventually pass a BodypartController and equip to that gameobject?
    {
        ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<EquipmentManager>().FastEquip(item);
    }
}
