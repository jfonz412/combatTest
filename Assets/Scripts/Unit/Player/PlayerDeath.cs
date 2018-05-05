using UnityEngine;

public class PlayerDeath : Death {

    public override void StopAllCombat(Transform myAttacker)
    {
        base.StopAllCombat(myAttacker);

        PlayerState.SetPlayerState(PlayerState.PlayerStates.Dead);
        ScriptToolbox.GetInstance().GetWindowCloser().CloseAllWindows();
    }
}
