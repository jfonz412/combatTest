using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable {

    public override void DefaultInteraction(Transform playerTransform)
    {
        AttackController playerAttackController = playerTransform.GetComponent<AttackController>();

        if (playerAttackController.lastKnownTarget != transform)
        {
            playerAttackController.EngageTarget(true, transform);
        }

        base.DefaultInteraction(playerTransform);
    }
}
