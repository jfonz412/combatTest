using UnityEngine;

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
