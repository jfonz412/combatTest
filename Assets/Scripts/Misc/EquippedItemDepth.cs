using UnityEngine;

public class EquippedItemDepth : MonoBehaviour {

    [HideInInspector]
    public Transform myOwner;
    SpriteRenderer sp;

    void Start()
    {
        if (transform.childCount > 0)
        {
            sp = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if(transform.childCount > 0)
        {
            SetDepth();
        }
       
    }

    void SetDepth()
    {
        
        sp.sortingOrder = (int)Mathf.RoundToInt(-myOwner.position.y * 1000) + 1;
    }
}
