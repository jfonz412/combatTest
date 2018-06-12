using System.Collections;
using UnityEngine;

public class AttackTimer : MonoBehaviour {

    private float timeUntilNextAttack = 0;
	
    public void ResetAttackTimer(float cooldownTime) //aka weapon speed
    {
        StartCoroutine(BeginCountdown(cooldownTime));
    }

    private IEnumerator BeginCountdown(float cooldownTime)
    {
        timeUntilNextAttack = cooldownTime;

        while (timeUntilNextAttack > 0)
        {
            timeUntilNextAttack -= Time.deltaTime;
            yield return null;
        }
    }

    public float Timer()
    {
        return timeUntilNextAttack;
    }
}
