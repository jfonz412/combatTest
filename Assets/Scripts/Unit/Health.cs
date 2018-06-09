using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float currentHealth; //do not touch, just so I can see it

    private EquipmentManager equipmentManager;
    private UnitReactions unitReactions;
    private Death deathController;
    private Stats myStats;

    private void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        myStats = GetComponent<Stats>();
        currentHealth = myStats.baseHp;
        unitReactions = GetComponent<UnitReactions>();
        deathController = GetComponent<Death>();
    }

    public void TakeDamage(float damage, Transform myAttacker)
    {
        float totalDamage = DamageCalculator.CalculateTotalDamage(damage, myStats.baseDefense, PickBodyPart());

        currentHealth = currentHealth - totalDamage;
        //Debug.Log(name + " has taken " + DamageCalculator.CalculateTotalDamage(damage, myStats.baseDefense, PickBodyPart()) + " damage!");

        FloatingTextController.CreateFloatingText(totalDamage.ToString(), transform);
        GetComponent<UnitAnimController>().TakeDamage(); //cache?

        if (currentHealth > 0.0f)
        {
            unitReactions.ReactToAttackAgainstSelf(myAttacker);           
        }
        else
        {
            TriggerDeath(myAttacker);
        }
    }

    private Armor PickBodyPart()
    {
        //GetComponent<BodyParts>().RecieveAttack();
        //EquipmentSlot { Head, Chest, Legs, MainHand, OffHand, Feet}
        int[] validChoices = new int[] { 0, 1, 2, 5 };
        int num = Random.Range(0, validChoices.Length);

        //Debug.Log("Attacking bodypart #" + validChoices[num]);
        return (Armor)equipmentManager.EquipmentFromSlot(validChoices[num]);
    }

    private void TriggerDeath(Transform myAttacker)
    {
        unitReactions.isDead = true; //stop reacting
        unitReactions.ReactToAttackAgainstSelf(myAttacker); //actually alerts others, and since this unit is dead UnitReactionManager should skip over it
        deathController.Die();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ExternalHealthAdjustment(float amount)
    {
        currentHealth += amount;
    }

    public void ApplyCurrentHealth(float loadedHealthValue)
    {
        currentHealth = loadedHealthValue;
    }
}
