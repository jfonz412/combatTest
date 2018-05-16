using UnityEngine;
using UnityEngine.UI;

public class NewGamePrompt : MonoBehaviour {
    [SerializeField]
    private GameObject warningPrompt;
    private Button button;
    [SerializeField]
    private bool activate;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { warningPrompt.SetActive(activate); });
    }
}
