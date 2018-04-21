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
        NPCInteraction npcInteractions = victim.GetComponent<NPCInteraction>();

        //the animator should be a shell for this
        SpriteRenderer spriteRend = victim.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRend.color = Color.grey;

        //stop processing clicks if player
        if (victim.name == "Player")
        {
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Dead);
            CloseOpenWindows.instance.CloseAllWindows();
        }

        //stop interactions if npc       
        if (npcInteractions != null)
        {
            npcInteractions.isDead = true;
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
