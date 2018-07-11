using System.Collections;
using UnityEngine;

public class AttackTimer : MonoBehaviour {

    private float timeUntilNextAttack = 0;
    private IEnumerator currentTimer;

    public bool coolingDown = false;
	
    public void ResetAttackTimer(float cooldownTime) //aka weapon speed
    {
        if (currentTimer != null)
        {
            StopCoroutine(currentTimer);
            coolingDown = false;
        }

        currentTimer = BeginCountdown(cooldownTime);
        StartCoroutine(currentTimer);
    }

    private IEnumerator BeginCountdown(float cooldownTime)
    {
        timeUntilNextAttack = cooldownTime;

        while (timeUntilNextAttack > 0)
        {
            coolingDown = true;
            timeUntilNextAttack -= Time.deltaTime;
            //Debug.Log(gameObject.name + " " + timeUntilNextAttack);
            yield return null;
        }

        coolingDown = false;
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
