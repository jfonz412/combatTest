using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    [HideInInspector]
    public Transform lastKnownTarget = null;

    Health targetHealth;

    EquipmentManager equipmentManager;
    Weapon equippedWeapon;
    int weaponIndex = (int)EquipmentSlot.MainHand;

    UnitAnimator anim;
    UnitController unit;

    IEnumerator engagingEntity;

    float myAttackStat;

    void Start()
    {
        myAttackStat = GetComponent<Stats>().attack;
        anim = GetComponent<UnitAnimator>();
        unit = GetComponent<UnitController>();

        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += SwapWeapons;
        SwapWeapons(null, null); //load whatever weapon is already equipped
    }
#region MainFunctionality
    #region EngageTarget
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

            targetHealth = lastKnownTarget.GetComponent<Health>();
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
#endregion

    /****************** PRIVATE FUNCTIONS **************************/
    #region MoveToEngagement
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
#endregion

    void StopAndAttack(Transform targetTransform)
    {
        unit.StopMoving();
        anim.FaceDirection(transform.position, targetTransform.position);
        anim.TriggerAttackAnimation(equippedWeapon.attackType);
        targetHealth.TakeDamage(CalculateDamageDealt(), transform);
    }


    //Calculate the damage of the weapon + stats
    //maybe put this method in stats? Maybe...
    float CalculateDamageDealt()
    {
        //also need to consider weapon condtion? maybe condition just makes it break
        return myAttackStat + equippedWeapon.weight + equippedWeapon.sharpness * equippedWeapon.softness * equippedWeapon.weaponCondition;    
    }
#endregion

    //Player callback for weapon swaps (called from EquipmentManager)
    void SwapWeapons(Equipment oldItem, Equipment newItem)
    {
        equippedWeapon = (Weapon)equipmentManager.currentEquipment[weaponIndex];
    }
}
