using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {

    [HideInInspector]
    public Transform player;

    public Sprite icon = null;
    public int? slotNum; //allows the int to be null

    new public string name = "New Item";
    public float baseValue = 100f;
    public float currentValue;
    public int quantity = 1;
    public int maxQuantity = 99;
    public bool stackable;

    public virtual void Use()
    {
        //player is always going to be the one using the item so might as well grab this here in the base item script
        player = PlayerManager.instance.player.transform;
        Debug.Log("Using " + name);
    }

    public virtual void OpenStatWindow(string itemLocation)
    {
        DetermineValue(itemLocation);

        Vector3 spawnPoint = WindowSpawnPoint();
        Instantiate(Resources.Load("ItemMenu"), spawnPoint, Quaternion.identity, CanvasUI.instance.CanvasTransform);

        ItemMenu.instance.PopulateStats(this);
    }

    public Vector3 WindowSpawnPoint()
    {
        Vector3 spawnPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
        spawnPoint.y += 1f;
        spawnPoint.z = 0f;
        return spawnPoint;
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void DetermineValue(string itemLocation)
    {
        if (itemLocation == "Shop")
        {
            currentValue = PriceChecker.AppraiseItem(this, "Purchase");
        }
        else
        {
            currentValue = PriceChecker.AppraiseItem(this, "Sale");
        }
    }
}
