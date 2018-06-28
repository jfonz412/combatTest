using UnityEngine;

public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public bool naked;

    public enum Material { NA, Cloth, Leather, Iron, Steel, Thicket, ThickFur, Bone, Wood }; //possibly move this to items if you want to 
    public Material material;

    [SerializeField]
    protected float hardnessValue = -1f;

    //called (manually by me) when a new object is instantiated
    public override void Init()
    {
        if(hardnessValue == -1f)
            hardnessValue = SetProtectionValue();
    }

    public override void Use() //RMB
    {
        base.Use();
        //Debug.Log("Equipping item");

        //the only entity that will equipping items after loading the scene is the player
        EquipmentManager manager = user.GetComponent<EquipmentManager>();

        manager.FastEquip(this);
    }

    public override void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);

        InfoPanel panel = InfoPanel.instance;

        if(panel != null)
            Instantiate(Resources.Load("PopUps/EquipmentStats"), panel.transform.position, Quaternion.identity, panel.transform);
    }


    //MIGHT BE ABLE TO MOVE THIS TO ARMOR
    protected virtual float GetHardnessValue()
    {
        return hardnessValue;
    }

    //based on Vicker's Hardness Scale
    private float SetProtectionValue()
    {
        //instead of random ranges, material quality will eventually be based on craftsmanship
        if(material == Material.NA)
        {
            return 0f;
        }
        else if(material == Material.Cloth)
        {
            return Random.Range(5, 20);
        }
        else if (material == Material.Thicket)
        {
            return Random.Range(10, 30);
        }
        else if (material == Material.Leather || material == Material.ThickFur)
        {
            return Random.Range(15, 30);
        }
        else if(material == Material.Bone || material == Material.Wood)
        {
            return Random.Range(20, 40);
        }
        else if (material == Material.Iron)
        {
            return Random.Range(30, 80);
        }
        else if (material == Material.Steel)
        {
            return Random.Range(55, 120);
        }
        else
        {
            return 0f;
        }
    }

    public EquipmentInfo GetEquipmentInfo()
    {
        EquipmentInfo info = new EquipmentInfo();
        info.filePath = GetResourcePath();
        info.slot = equipSlot;

        return info;
    }
}

[System.Serializable]
public struct EquipmentInfo
{
    public EquipmentSlot slot;
    public string filePath;
}

public enum EquipmentSlot { Head, Chest, Hands, Legs, MainHand, OffHand, Feet }