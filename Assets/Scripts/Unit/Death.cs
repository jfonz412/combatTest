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
        NPCInteractionStates npcState = victim.GetComponent<NPCInteractionStates>();

        victim.GetComponent<UnitAnimController>().Die();

        //stop processing clicks if player
        if (victim.name == "Player")
        {
            PlayerState.SetPlayerState(PlayerState.PlayerStates.Dead);
            CloseOpenWindows.instance.CloseAllWindows();
        }

        //stop interactions if npc       
        if (npcState != null)
        {
            npcState.SetInteractionState(NPCInteractionStates.InteractionState.Dead);
        }
    }

    static void StopAllCombat(Transform victim, Transform attacker)
    {
        //stop the victim
        AttackController myAttackController = victim.GetComponent<AttackController>();

        victim.GetComponent<UnitController>().StopMoving();

        myAttackController.EngageTarget(false);

        //stop the attacker
        attacker.GetComponent<AttackController>().EngageTarget(false);
    }
}
