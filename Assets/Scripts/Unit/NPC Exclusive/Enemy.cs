using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable {

    public override void DefaultInteraction()
    {
        base.DefaultInteraction(); //gets the reference to the player
        AttackController playerAttackController = player.GetComponent<AttackController>();
        if (playerAttackController.lastKnownTarget != transform)
        {
            playerAttackController.EngageTarget(true, transform);
        }
    }

}
