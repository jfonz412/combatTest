using UnityEngine;

public class ItemEffects : MonoBehaviour {

    public enum ItemEffect { HealPlayer, AttackBoost };

    public static void TriggerEffect(ItemEffect effect)
    {
        if(effect == ItemEffect.HealPlayer)
        {
            HealPlayer();
        }
        if (effect == ItemEffect.HealPlayer)
        {
            BoostAttack();
        }
    }

    static void HealPlayer()
    {
        Health health = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<Health>();
        health.ExternalHealthAdjustment(25f); //replace this with health method that "fixes" player
    }

    static void BoostAttack()
    {
        Debug.Log("Boosting attack");
    }
}
