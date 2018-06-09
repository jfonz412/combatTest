using UnityEngine;

public class PlayerDeath : Death {

    public override void StopAllCombat()
    {
        base.StopAllCombat();

        PlayerState.SetPlayerState(PlayerState.PlayerStates.Dead);
        ScriptToolbox.GetInstance().GetWindowCloser().CloseAllWindows();
    }
}
