using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    [HideInInspector]
    public Transform lastKnownTarget = null;

    UnitAnimator anim;
    UnitController unit;

    Weapon equippedWeapon;

    IEnumerator engagingEntity;


    void Start()
    {
        anim = GetComponent<UnitAnimator>();
        unit = GetComponent<UnitController>();
        equippedWeapon = anim.loadedWeapon.GetComponent<Weapon>();
    }

    //How the player object talks to this script
    public void EngageTarget(bool hasTarget, Transform targetTransform = null)
    {
        if (hasTarget)
        {
            lastKnownTarget = targetTransform;
            if (engagingEntity != null)
            {
                StopCoroutine(engagingEntity);
            }
            engagingEntity = MoveToEngagement(targetTransform);
            StartCoroutine(engagingEntity);
        }
        else
        {
            if (engagingEntity != null)
            {
                StopCoroutine(engagingEntity);
            }
            lastKnownTarget = null;
        }
    }

    /****************** PRIVATE FUNCTIONS **************************/

    IEnumerator MoveToEngagement(Transform targetTransform)
    {
        while (targetTransform)
        {
            if (Vector3.Distance(transform.position, lastKnownTarget.position) > equippedWeapon.range)
            { //and targetTransform != null ?
                lastKnownTarget = targetTransform;
                PathfindingManager.RequestPath(transform.position, lastKnownTarget.position, unit.OnPathFound);
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                StopAndAttack(targetTransform);
                lastKnownTarget = targetTransform;
                yield return new WaitForSeconds(equippedWeapon.speed + Random.Range(0.0f, 0.2f));
            }
            yield return null;
        }
        yield break;
    }


    void StopAndAttack(Transform targetTransform)
    {
        unit.StopMoving();
        anim.FaceDirection(transform.position, targetTransform.position);
        anim.TriggerAttackAnimation(equippedWeapon.attackType);
        equippedWeapon.Attack(transform, targetTransform);
    }
}
