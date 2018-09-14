using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class SelectInteractable : MonoBehaviour {

    private static GameObject currentMenu;

	// Use this for initialization
	public static void SpawnMenu (List<Collider2D> colliders) {
        Vector3 groupPos = colliders[0].transform.position;
        Vector3 menuSpawnPoint = new Vector3(groupPos.x, groupPos.y + 1f, groupPos.z);

        currentMenu = Instantiate(Resources.Load("PopUps/Interactable/InteractableChoice/ChooseInteractableMenu"), menuSpawnPoint, Quaternion.identity, CanvasUI.instance.transform) as GameObject; 

        PopulateOptions(colliders);
    }
	
	// Update is called once per frame
	static void PopulateOptions (List<Collider2D> colliders)
    {
		for(int i = 0; i < colliders.Count; i++)
        {
            Collider2D col = colliders[i];
            GameObject button = Instantiate(Resources.Load("PopUps/Interactable/InteractableChoice/InteractableChoice"), Vector3.zero, Quaternion.identity, currentMenu.transform) as GameObject;
            button.GetComponent<Text>().text = col.name;
            button.GetComponent<Button>().onClick.AddListener(() => PassCollider(col));
        }
	}

    static void PassCollider(Collider2D col)
    {
        PlayerMoveState player = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<PlayerMoveState>();
        player.CheckForInteractableMenu(col);
        Destroy(currentMenu);
    }

    public static void Destroy()
    {
        Destroy(currentMenu);
    }
}
