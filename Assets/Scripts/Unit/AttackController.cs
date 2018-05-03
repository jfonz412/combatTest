using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour {

    [HideInInspector]
    public Transform lastKnownTarget = null;

    private IEnumerator engagingEntity;

    private Health targetHealth;

    private EquipmentManager equipmentManager;
    private Weapon equippedWeapon;
    private int weaponIndex = (int)EquipmentSlot.MainHand;

    private Stats myStats;

    private UnitAnimController anim;
    private UnitController unit;
    private AttackTimer attackTimer;

    private float myAttackStat;
    

    void Start()
    {
        myStats = GetComponent<Stats>(); //need to account for stat changes
        anim = GetComponent<UnitAnimController>();
        unit = GetComponent<UnitController>();
        attackTimer = GetComponent<AttackTimer>();

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
        bool inRange;
        Collider2D c = GetComponent<Collider2D>();

        anim.FaceDirection(transform.position, targetTransform.position);

        while (targetTransform)
        {
            //eventually this will be a function that will check if ranged or melee, then decide if in range or not
            inRange = !c.IsTouching(targetTransform.GetComponent<Collider2D>()); //this would just be for melee
            if (inRange) 
            { 
                lastKnownTarget = targetTransform;
                
                PathfindingManager.RequestPath(transform.position, lastKnownTarget.position, unit.OnPathFound);
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                yield return new WaitForSeconds(attackTimer.Timer());
                attackTimer.ResetAttackTimer(equippedWeapon.speed);

                StopAndAttack(targetTransform);
                lastKnownTarget = targetTransform;
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
        anim.Attack(); //equippedWeapon.attackType
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
