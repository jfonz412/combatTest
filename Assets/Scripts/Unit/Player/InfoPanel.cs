using UnityEngine;

public class InfoPanel : MonoBehaviour {
    #region Singleton
    public static InfoPanel instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public void InstantiatePanel()
    {
        gameObject.SetActive(true);
        Instantiate(Resources.Load("PopUps/ItemMenu"), transform.position, Quaternion.identity, transform);
    }
}
