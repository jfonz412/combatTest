using UnityEngine;

public class PlayerDeath : Death {

    public override void StopAllCombat()
    {
        base.StopAllCombat();

        GetComponent<Brain>().ToggleState(Brain.State.Dead, true);
        //ScriptToolbox.GetInstance().GetWindowCloser().CloseAllWindows();
    }
}
