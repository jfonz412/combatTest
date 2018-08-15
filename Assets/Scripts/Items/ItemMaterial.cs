using UnityEngine;

public class ItemMaterial : MonoBehaviour {

    public enum Material { NA, Cloth, Leather, Iron, Steel, Thicket, ThickFurSkin, FurSkin, Bone, Wood, Glass, Ceramic }

    public static float HardnessValue(Material material)
    {
        //based on Vicker's Hardness Scale
        //instead of random ranges, material quality will eventually be based on craftsmanship
        if (material == Material.NA)
        {
            return 0f;
        }
        else if (material == Material.Cloth)
        {
            return Random.Range(5, 20);
        }
        else if (material == Material.Thicket || material == Material.ThickFurSkin)
        {
            return Random.Range(10, 30);
        }
        else if (material == Material.Leather || material == Material.ThickFurSkin)
        {
            return Random.Range(15, 30);
        }
        else if (material == Material.Bone || material == Material.Wood)
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
            Debug.LogError("Material hardness value not set!");
            //consider adding an additional value that would make a log less effective than a wood sword?
            return 0f;
        }
    }
}
