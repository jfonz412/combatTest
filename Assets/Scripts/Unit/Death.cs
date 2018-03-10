using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {

    public static IEnumerator HumanoidDeath(Transform victim, Transform attacker)
    {
        StopAllCombat(victim, attacker);
        IncapacitateEntity(victim, attacker);

        //extend death animation before destroy
        yield return new WaitForSeconds(6f);
        Destroy(victim.gameObject);
    }

    static void IncapacitateEntity(Transform victim, Transform attacker)
    {
        UnitReactions unitReactions = victim.GetComponent<UnitReactions>();
        PlayerController player = victim.GetComponent<PlayerController>();
        NPCInteraction npcInteractions = victim.GetComponent<NPCInteraction>();

        //stop processing clicks if player
        if (player != null)
        {
            player.incapacitated = true;
            CloseOpenWindows.instance.CloseAllWindows();
        }

        //stop interactions if npc       
        if (npcInteractions != null)
        {
            npcInteractions.isDead = true;
            unitReactions.isDead = true;
        }
    }

    static void StopAllCombat(Transform victim, Transform attacker)
    {
        //stop the victim
        AttackController myAttackController = victim.GetComponent<AttackController>();
        UnitAnimator myAnim = victim.GetComponent<UnitAnimator>();

        victim.GetComponent<UnitController>().StopMoving();

        myAttackController.EngageTarget(false);
        myAnim.TriggerDeathAnimation();

        //stop the attacker
        attacker.GetComponent<AttackController>().EngageTarget(false);
    }
}
