using UnityEngine;
using UnityEngine.UI;

public class MouseSlot : MonoBehaviour {

    //MIGHT BE GOOD TO MAKE THE GAMEOBJECT THIS SCRIPT IS ATTACHED TO A CUSTOM CURSOR, THEN JUST FIGURE OUT HOW TO CHANGE THE 
    //CURSOR'S SPRITE

    //Image image;
    public Item currentItem;
    //public Sprite itemSprite;

    #region Singleton
    public static MouseSlot instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of MouseSlot found");
            return;
        }
        instance = this;
    }
    #endregion

	// Update is called once per frame
	void Update () {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    public void UpdateItem(Item item)
    {
        currentItem = item;

        if(currentItem != null)
        {
            //image.sprite = currentItem.icon;
        }
        else
        {
            //image.sprite = null;
        }
    }
}
