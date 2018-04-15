using UnityEngine;

public class InfoPanel : MonoBehaviour {
    #region Singleton
    public static InfoPanel instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
}
