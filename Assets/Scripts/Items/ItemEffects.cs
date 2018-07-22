using UnityEngine;

public class ItemEffects : MonoBehaviour {

    public enum ItemEffect { Equip };

    public static void Use(ItemEffect effect)
    {
        if (effect == ItemEffect.Equip)
        {
            EquipToPlayer();
        }
    }

    static void EquipToPlayer() //maybe eventually pass a BodypartController and equip to that gameobject?
    {
        Debug.Log("Equipping item to player");
    }
}
