using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] //allows us to create Items like we can create folders, scripts, etc.
public class Item : ScriptableObject {

    [HideInInspector]
    public Transform player;

    new public string name = "New Item";
    public float value = 100f;
    public Sprite icon = null;
    public int? slotNum;
    public int quantity;

    public virtual void Use()
    {
        //player is always going to be the one using the item so might as well grab this here in the base item script
        player = PlayerManager.instance.player.transform;
        Debug.Log("Using " + name);
    }

    public virtual void OpenStatWindow()
    {
        Vector3 spawnPoint = WindowSpawnPoint();
        Instantiate(Resources.Load("ItemMenu"), spawnPoint, Quaternion.identity, CanvasUI.instance.CanvasTransform);

        Debug.Log("Opening Item menu");

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
}
