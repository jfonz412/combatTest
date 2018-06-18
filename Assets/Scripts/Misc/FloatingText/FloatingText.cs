using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
	Animator anim;
    Text damageText;

	void Awake () {
		anim = GetComponent<Animator>();
        AnimatorClipInfo[] clip = anim.GetCurrentAnimatorClipInfo(0);
        damageText = transform.GetChild(0).GetComponent<Text>();
        Destroy(gameObject, clip.Length);
	}

    public void SetText(string text, Color color)
    {
        damageText.color = color;
        damageText.text = text;
    }
}
