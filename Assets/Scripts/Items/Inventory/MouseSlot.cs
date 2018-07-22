using UnityEngine;

public class MouseSlot : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Sprite currentItemSprite;
    public Item currentItem;

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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; // Set this to be the distance you want the object to be placed in front of the camera.
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(temp);

    }

    public void UpdateItem(Item item)
    {
        currentItem = item;

        if(currentItem != null)
        {
            currentItemSprite = currentItem.icon;
            spriteRenderer.sprite = currentItemSprite;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    public void ToggleSprite(bool spriteOn)
    {
        if (!spriteOn)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            if (currentItem != null)
                spriteRenderer.sprite = currentItemSprite;
        }
    }

    public void DeleteItem()
    {
        if(currentItem != null)
        {
            currentItem = null;
            spriteRenderer.sprite = null;
            //Destroy(currentItem);
            Debug.Log("need to implement this");
            Debug.Log("Item destroyed");
        }
    }
}
