using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour {

    [HideInInspector]
    public Transform lastKnownTarget = null;

    private IEnumerator engagingEntity;

    private BodyParts targetBody;

    private EquipmentManager equipmentManager;
    public Weapon equippedWeapon;
    private int weaponIndex = (int)EquipmentSlot.MainHand;

    private CombatSkills mySkills;

    private UnitAnimController anim;
    private UnitController unit;
    private AttackTimer attackTimer;

    private float myAttackStat;

    private void Start()
    {
        mySkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();
        unit = GetComponent<UnitController>();
        attackTimer = GetComponent<AttackTimer>();

        equipmentManager = GetComponent<EquipmentManager>();
        equipmentManager.onEquipmentChanged += SwapWeapons;
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
    private IEnumerator MoveToEngagement(Transform targetTransform)
    {
        bool inRange;
        UnitReactions unitAI = targetTransform.GetComponent<UnitReactions>();
        Collider2D c = GetComponent<Collider2D>();
       
        anim.FaceDirection(transform.position, targetTransform.position);

        while (!unitAI.isDead) //targetTransform && 
        {
            //eventually this will be a function that will check if ranged or melee, then decide if in range or not
            inRange = c.IsTouching(targetTransform.GetComponent<Collider2D>()); //this would just be for melee
            //else in range will a raycast of some sort to check for obstabcles
            if (!inRange) 
            { 
                lastKnownTarget = targetTransform;
                
                PathfindingManager.RequestPath(transform.position, lastKnownTarget.position, unit.OnPathFound);
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                Attack(targetTransform);
                yield return new WaitForSeconds(attackTimer.Timer());
            }
            yield return null;
        }
        yield break;
    }

    #region Helper Functions
    private void Attack(Transform targetTransform)
    {
        if(equippedWeapon == null)
        {
            SwapWeapons(null, null); //load whatever weapon is already equipped
        }

        AttackAnimation(targetTransform);

        /*
        float damage = DamageCalculator.CalculateDamageDealt(myStats.baseAttack, equippedWeapon.totalAttack);
        targetHealth.TakeDamage(damage, transform);
        */

        AttackInfo myAttack = mySkills.RequestAttackInfo(equippedWeapon);
        targetBody.RecieveAttack(myAttack, transform);

        attackTimer.ResetAttackTimer(myAttack.speed);
    }


    private void AttackAnimation(Transform targetTransform)
    {
        unit.StopMoving();

        if(targetTransform != null)
            anim.FaceDirection(transform.position, targetTransform.position);

        anim.Attack(); //equippedWeapon.attackType
    }


    private void StopEngagingEnemy()
    {
        if (engagingEntity != null)
        {
            StopCoroutine(engagingEntity);
        }
    }

    private void EngageNewEnemy(Transform targetTransform)
    {     
        targetBody = lastKnownTarget.GetComponent<BodyParts>();
        engagingEntity = MoveToEngagement(targetTransform);

        StartCoroutine(engagingEntity);
    }

    #endregion

    //Player callback for weapon swaps (called from EquipmentManager)
    private void SwapWeapons(Equipment oldItem, Equipment newItem)
    {
        equippedWeapon = (Weapon)equipmentManager.EquipmentFromSlot(weaponIndex);
    }
}

