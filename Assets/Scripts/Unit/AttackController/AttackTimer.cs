using System.Collections;
using UnityEngine;

public class AttackTimer : MonoBehaviour {

    private float timeUntilNextAttack = 0;
    private IEnumerator currentTimer;
	
    public void ResetAttackTimer(float cooldownTime) //aka weapon speed
    {
        currentTimer = BeginCountdown(cooldownTime);
        StartCoroutine(currentTimer);
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

    public void ParryTriggered()
    {
        if (currentTimer != null)
        {
            StopCoroutine(currentTimer);
        }

        timeUntilNextAttack = 0f;
    }
}
