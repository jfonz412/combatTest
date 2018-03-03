using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    [HideInInspector]
    public Transform lastKnownTarget = null;

    Health targetHealth;

    EquipmentManager equipmentManager;
    Weapon equippedWeapon;

    Stats myStats;

    int weaponIndex = (int)EquipmentSlot.MainHand;

    UnitAnimator anim;
    UnitController unit;

    IEnumerator engagingEntity;

    float myAttackStat;

    void Start()
    {
        myStats = GetComponent<Stats>(); //need to account for stat changes
        anim = GetComponent<UnitAnimator>();
        unit = GetComponent<UnitController>();

        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += SwapWeapons;
        SwapWeapons(null, null); //load whatever weapon is already equipped
    }


    //How the player object talks to this script
    public void EngageTarget(bool hasTarget, Transform targetTransform = null)
    {
        if (hasTarget)
        {
            lastKnownTarget = targetTransform;
            StopEngagingEnemy();
            EngageNewEnemy(targetTransform);
        }
        else
        {
            StopEngagingEnemy();
            lastKnownTarget = null;
        }
    }

    /****************** PRIVATE FUNCTIONS **************************/
    IEnumerator MoveToEngagement(Transform targetTransform)
    {
        anim.FaceDirection(transform.position, targetTransform.position);
        yield return new WaitForSeconds(equippedWeapon.speed); //initial delay to prevent enemy attack-swapping

        while (targetTransform)
        {
            if (Vector3.Distance(transform.position, lastKnownTarget.position) > equippedWeapon.range) //and targetTransform != null ?
            { 
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

#region Helper Functions
    void StopAndAttack(Transform targetTransform)
    {
        float damage = DamageCalculator.CalculateDamageDealt(myStats.baseAttack, equippedWeapon.totalAttack);

        AttackAnimation(targetTransform);
        targetHealth.TakeDamage(damage, transform);
    }


    void AttackAnimation(Transform targetTransform)
    {
        unit.StopMoving();
        anim.FaceDirection(transform.position, targetTransform.position);
        anim.TriggerAttackAnimation(equippedWeapon.attackType);
    }

    void StopEngagingEnemy()
    {
        if (engagingEntity != null)
        {
            StopCoroutine(engagingEntity);
        }
    }

    void EngageNewEnemy(Transform targetTransform)
    {     
        targetHealth = lastKnownTarget.GetComponent<Health>();
        engagingEntity = MoveToEngagement(targetTransform);

        StartCoroutine(engagingEntity);
    }
#endregion

    //Player callback for weapon swaps (called from EquipmentManager)
    void SwapWeapons(Equipment oldItem, Equipment newItem)
    {
        equippedWeapon = (Weapon)equipmentManager.currentEquipment[weaponIndex];
    }
}
