using UnityEngine;

public class CanvasUI : MonoBehaviour {

    #region Singleton
    public static CanvasUI instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform CanvasTransform;
}
