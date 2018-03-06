﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float currentHealth; //do not touch, just so I can see it
    EquipmentManager equipmentManager;
    UnitReactions unitReactions;
    Stats myStats;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        myStats = GetComponent<Stats>();
        currentHealth = myStats.baseHp;
        unitReactions = GetComponent<UnitReactions>();
    }

    public void TakeDamage(float damage, Transform attacker)
    {
        float totalDamage = DamageCalculator.CalculateTotalDamage(damage, myStats.baseDefense, PickBodyPart());

        currentHealth = currentHealth - totalDamage;
        //Debug.Log(name + " has taken " + DamageCalculator.CalculateTotalDamage(damage, myStats.baseDefense, PickBodyPart()) + " damage!");

        FloatingTextController.CreateFloatingText(totalDamage.ToString(), transform);

        if (currentHealth > 0.0f)
        {
            unitReactions.ReactToAttackAgainstSelf(attacker);           
        }
        else
        {
            //IEnumerator death = Die(attacker);
            IEnumerator death = Death.HumanoidDeath(transform, attacker);
            StartCoroutine(death);
        }      
    }

    Armor PickBodyPart()
    {
        //EquipmentSlot { Head, Chest, Legs, MainHand, OffHand, Feet}
        int[] validChoices = new int[] { 0, 1, 2, 5 };
        int num = Random.Range(0, validChoices.Length);

        //Debug.Log("Attacking bodypart #" + validChoices[num]);
        return (Armor)equipmentManager.currentEquipment[validChoices[num]];
    }
}
