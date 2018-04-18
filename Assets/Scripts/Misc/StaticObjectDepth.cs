using UnityEngine;

//this is on pick up items because it's not worth it to have an update method
//called on every pickup item just because they might move around a little
public class StaticObjectDepth : MonoBehaviour {

    SpriteRenderer sp;
    public int modifier = 0;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        SetDepth();
    }

    void SetDepth()
    {
        sp.sortingOrder = (int)Mathf.RoundToInt(-transform.position.y * 1000) + modifier;
    }
}
