using UnityEngine;
using UnityEngine.UI;

public class DollPart : MonoBehaviour {
    public BodyParts.Parts dollPart;
    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void ChangeColor(Color color)
    {
        myImage.color = color;
    }
}
