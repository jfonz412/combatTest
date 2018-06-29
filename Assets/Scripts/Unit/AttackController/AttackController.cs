using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour {

    [HideInInspector]
    public Transform lastKnownTarget = null;

    private IEnumerator engagingEntity;

    private BodyParts targetBody;
    private BodyParts myBody;
    private EquipmentManager equipmentManager;
    public Weapon mainHand;
    public Weapon offHand;

    private CombatSkills mySkills;

    private UnitAnimController anim;
    private UnitController unit;
    private AttackTimer attackTimer;

    [SerializeField]
    private BodyParts.Parts[] mainBodyPartsNeeded, offBodyPartsNeeded;// = new BodyParts.Parts[] { BodyParts.Parts.RightArm, BodyParts.Parts.RightHand };

    private float myAttackStat;

    private void Start()
    {
        mySkills = GetComponent<CombatSkills>();
        anim = GetComponent<UnitAnimController>();
        unit = GetComponent<UnitController>();
        attackTimer = GetComponent<AttackTimer>();
        myBody = GetComponent<BodyParts>();
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
                while(attackTimer.Timer() > 0f)
                {
                    yield return null;
                }

            }
            yield return null;
        }
        yield break;
    }

    #region Helper Functions
    private void Attack(Transform targetTransform)
    {
        if(mainHand == null || offHand == null)
        {
            SwapWeapons(null, null); //load whatever weapon is already equipped
        }

        AttackInfo myAttack;

        if (Random.Range(0, 100) <= 75)
        {
            if (!myBody.VitalPartInjured(mainBodyPartsNeeded))
            {
                AttackAnimation(targetTransform);
                myAttack = mySkills.RequestAttackInfo(mainHand);
                Debug.Log(gameObject.name + "MAIN HAND");
            }
            else
            {
                string line = "<color=red>" + gameObject.name + " is too injured to attack " + targetTransform.name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer
                                                  //unit reactions -> run away if not player?

                return;
            }
        }
        else
        {
            if (!myBody.VitalPartInjured(offBodyPartsNeeded))
            {
                AttackAnimation(targetTransform);
                myAttack = mySkills.RequestAttackInfo(offHand);
                Debug.Log(gameObject.name + " OFF HAND");
            }
            else
            {

                string line = "<color=red>" + gameObject.name + " is too injured to attack " + targetTransform.name + "</color>";
                FloatingTextController.CreateFloatingText("Too injured!", transform, Color.red);
                BattleReport.AddToBattleReport(line);
                attackTimer.ResetAttackTimer(7f); //arbitrarily reset attack timer
                                                  //unit reactions -> run away if not player?
                return;
            }
        }

        targetBody.RecieveAttack(myAttack, transform); //should reset back to whatever weapon was used
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
        mainHand = (Weapon)equipmentManager.EquipmentFromSlot(EquipmentSlot.MainHand);
        offHand  = (Weapon)equipmentManager.EquipmentFromSlot(EquipmentSlot.OffHand);
    }
}

