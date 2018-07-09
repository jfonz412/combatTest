using UnityEngine;

public class NPCDeath : Death {
    override public void StopAllCombat()
    {
        base.StopAllCombat();
        GetComponent<Brain>().ToggleState(Brain.State.Dead, true);
    }
}
