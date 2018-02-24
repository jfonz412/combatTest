using UnityEngine;

public class CloseOpenWindows : MonoBehaviour {

    public static void DestroyOpenMenus()
    {
        if (InteractableMenu.instance != null)
        {
            InteractableMenu.instance.CloseMenu();
        }

        if (EquipmentStats.instance != null)
        {
            EquipmentStats.instance.CloseMenu();
        }

        if (ItemMenu.instance != null)
        {
            ItemMenu.instance.CloseMenu();
        }
    }
}
