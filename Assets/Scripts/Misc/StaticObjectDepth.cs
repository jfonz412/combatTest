using UnityEngine;

//this is on pick up items also
public class StaticObjectDepth : MonoBehaviour {

    SpriteRenderer sp;
    public int modifier;

    void Start()
    {

        GetSpriteRenderer();
        SetDepth();
    }

    void SetDepth()
    {
        sp.sortingOrder = (int)Mathf.RoundToInt(-transform.position.y * 1000) + modifier;
    }

    private void GetSpriteRenderer()
    {
        sp = GetComponent<SpriteRenderer>();
        if(sp == null)
        {
            GetComponentInChildren<SpriteRenderer>();
        }
    }
}
