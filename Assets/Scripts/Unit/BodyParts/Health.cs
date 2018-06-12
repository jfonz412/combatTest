using UnityEngine;

public class Health : MonoBehaviour
{


    private void TriggerDeath(Transform myAttacker)
    {
        //unitReactions.isDead = true; //stop reacting
        //unitReactions.ReactToAttackAgainstSelf(myAttacker); //actually alerts others, and since this unit is dead UnitReactionManager should skip over it
        //deathController.Die();
    }

}
