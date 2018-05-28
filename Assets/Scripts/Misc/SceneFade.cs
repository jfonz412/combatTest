using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    private Image fadePanel;
    private Color currentColor;// = Color.black;

    // Use this for initialization
    private void Start()
    {
        fadePanel = GetComponent<Image>();
        currentColor = fadePanel.color;
    }

    // Update is called once per frame
    private void Update()
    {
        FadeIn();
    }

    private void FadeIn()
    {
        if (Time.timeSinceLevelLoad < fadeTime)
        {
            float alphaChangePerFrame = Time.deltaTime / fadeTime;
            currentColor.a -= alphaChangePerFrame;
            fadePanel.color = currentColor;
        }
        else
        {
            //deactivate object once fade is finished
            gameObject.SetActive(false);
        }
    }
}