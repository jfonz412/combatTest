using UnityEngine;
using System.Collections;

public class UnitAnimator : MonoBehaviour
{
    public Animator[] animators;
    public GameObject unitBody;

    //these might not have to be gameobjects anymore, just animators
    [HideInInspector]
    public GameObject loadedHeadArmor;
    [HideInInspector]
    public GameObject loadedTorsoArmor;
    [HideInInspector]
    public GameObject loadedLegArmor;
    [HideInInspector]
    public GameObject loadedFeetArmor;
    [HideInInspector]
    public GameObject loadedWeapon;

    float inputX = 0f;
    float inputY = -1f; //why is this -1?

    // Use this for initialization
    void Start()
    {
        animators = new Animator[7]; //will be 7, 6 enum bodyparts plus player
        animators[0] = GetComponent<Animator>(); //Get Character Animator
    }

    public void LoadEquipment(int equipSlot, int equipmentID)
    {
        GameObject loadedEquipment = null;
        int animIndex = equipSlot + 1;
        switch (equipSlot)
        {
            case 0:
                animIndex = 4;
                loadedEquipment = LoadHeadArmor(equipmentID);
                //Debug.Log("Equipping to head");
                break;
            case 1:
                loadedEquipment = LoadTorsoArmor(equipmentID);
                //Debug.Log("Equipping to torso");
                break;
            case 2:
                loadedEquipment = LoadLegArmor(equipmentID);
                //Debug.Log("Equipping to legs");
                break;
            case 3:
                animIndex = 1; //want to keep weapon at one...can probably change this later
                loadedEquipment = LoadWeapon(equipmentID);
                //Debug.Log("Equipping to main hand");
                break;
            case 5:
                loadedEquipment = LoadFeetArmor(equipmentID);
                //Debug.Log("Equipping to feet");
                break;
            default:
                loadedEquipment = null;
                //Debug.LogWarning("Bodypart not yet defined");
                break;
        }
        if (loadedEquipment != null)
        {
            loadedEquipment.transform.SetParent(unitBody.transform);
            loadedEquipment.transform.localPosition = new Vector3(0, 0, 0);

            animators[animIndex] = loadedEquipment.GetComponent<Animator>();
            ResetAnimators();
        }
    }

    #region Individual Equipment Loads

    GameObject LoadWeapon(int weaponID)
    { //should take an into for the equipment ID
        if (loadedWeapon != null)
        {
            Destroy(loadedWeapon.gameObject);
            loadedWeapon = null;
        }

        if (weaponID == 0)
        {
            loadedWeapon = Instantiate(Resources.Load("Weapons/unarmed"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (weaponID == 1)
        {
            loadedWeapon = Instantiate(Resources.Load("Weapons/Iron Dagger"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (weaponID == 2)
        {
            loadedWeapon = Instantiate(Resources.Load("Weapons/Iron Spear"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        return loadedWeapon;
    }

    GameObject LoadHeadArmor(int headID)
    {
        //Get rid of old armor if there is any
        if (loadedHeadArmor != null)
        {
            Destroy(loadedHeadArmor.gameObject);
            loadedHeadArmor = null;
        }

        if (headID == 0)
        {
            loadedHeadArmor = Instantiate(Resources.Load("Armor/naked"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (headID == 1)
        {
            loadedHeadArmor = Instantiate(Resources.Load("Armor/PlateIronHelmet"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        return loadedHeadArmor;
    }



    GameObject LoadTorsoArmor(int torsoID)
    {
        //Get rid of old armor if there is any
        if (loadedTorsoArmor != null)
        {
            Destroy(loadedTorsoArmor.gameObject);
            loadedTorsoArmor = null;
        }

        if (torsoID == 0)
        {
            loadedTorsoArmor = Instantiate(Resources.Load("Armor/naked"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (torsoID == 1)
        {
            loadedTorsoArmor = Instantiate(Resources.Load("Armor/PlateIronTorso"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        return loadedTorsoArmor;
    }

    GameObject LoadLegArmor(int LegID)
    {
        //Get rid of old armor if there is any
        if (loadedLegArmor != null)
        {
            //PutItemBackInInventory(loadedTorsoArmor);
            Destroy(loadedLegArmor.gameObject);
            loadedLegArmor = null;
        }

        if (LegID == 0)
        {
            loadedLegArmor = Instantiate(Resources.Load("Armor/naked"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (LegID == 1)
        {
            loadedLegArmor = Instantiate(Resources.Load("Armor/PlateIronLegs"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        return loadedLegArmor;
    }

    GameObject LoadFeetArmor(int FeetID)
    {
        //Get rid of old armor if there is any
        if (loadedFeetArmor != null)
        {
            //PutItemBackInInventory(loadedTorsoArmor);
            Destroy(loadedFeetArmor.gameObject);
            loadedFeetArmor = null;
        }

        if (FeetID == 0)
        {
            loadedFeetArmor = Instantiate(Resources.Load("Armor/naked"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }
        else if (FeetID == 1)
        {
            loadedFeetArmor = Instantiate(Resources.Load("Armor/PlateIronBoots"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        return loadedFeetArmor;
    }
    #endregion

    /************************* PUBLIC FUNCTIONS ****************************************/

    public void FaceDirection(Vector2 startPos, Vector2 endPos)
    {
        Vector2 relativePos = endPos - startPos;
        inputX = relativePos.x;
        inputY = relativePos.y;

        //set all animators 
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] != null)
            {
                animators[i].SetFloat("x", inputX);
                animators[i].SetFloat("y", inputY);
            }
        }
    }

    public void ToggleMovingAnimation(bool isMoving)
    {
        if (isMoving)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                if (animators[i] != null)
                {
                    animators[i].SetBool("isWalking", true);
                }
            }
        }
        else
        {
            for (int i = 0; i < animators.Length; i++)
            {
                if (animators[i] != null)
                {
                    animators[i].SetBool("isWalking", false);
                }
            }
        }
    }

    public void TriggerAttackAnimation(string attackType)
    {
        if (attackType == "slash")
        {
            AttackAnimation("isSlashing");
        }
        else if (attackType == "thrust")
        {
            AttackAnimation("isThrusting");
        }
    }

    void AttackAnimation(string triggerType)
    {
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] != null)
            {
                animators[i].SetFloat("x", inputX);
                animators[i].SetFloat("y", inputY);
                animators[i].SetTrigger(triggerType);
            }
        }
    }

    public void TriggerDeathAnimation()
    { //unless i = 1 which is weapon
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] != null && i != 1)
            {
                animators[i].SetTrigger("deathAnimation");
            }
        }
    }

    void ResetAnimators()
    {
        //might need to reset bools too
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i] != null)
            {
                animators[i].SetFloat("x", inputX);
                animators[i].SetFloat("y", inputY);
            }
        }
    }
}
