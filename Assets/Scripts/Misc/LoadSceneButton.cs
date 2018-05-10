using UnityEngine.UI;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour {
    private Button button;
    [SerializeField]
    private string scene;
    [SerializeField]
    private bool loadPreviousScene;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ApplicationManager.GetInstance().GetLevelManager().LoadScene(scene); });
    }
}
