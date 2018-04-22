using System.Collections;
using UnityEngine;

public class UnitAnimController : MonoBehaviour {

    SpriteRenderer spriteRend;
    public bool faceRightAtStart;
    bool dead;

	// Use this for initialization
	void Start ()
    {
        spriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (faceRightAtStart)
        {
            FaceDirection(transform.position, new Vector2(99999f,0f));
        }
        //FaceDirection(transform.position, lookHereAtStart.position);
	}

    //by default we are facing all units (arbitrarily) to the left
    public void FaceDirection(Vector2 startPos, Vector2 endPos) //or transform
    {
        Vector2 relativePos = endPos - startPos;
        Debug.Log(Mathf.Sign(relativePos.x));

        if (Mathf.Sign(relativePos.x) == 1)
        {
            spriteRend.flipX = true;
        }
        else
        {
            spriteRend.flipX = false;
        }
    }

    #region Actions and Reactions

    public void Walking(bool walking)
    {
        //bob up and down
    }

    public void TakeDamage()
    {
        StartCoroutine(FlashRed());
    }

    public void Block()
    {
        //StartCoroutine(FlashBlue???());
    }

    public void Parry()
    {
        //StartCoroutine(FlashGreen???());
    }

    public void Dodge()
    {
        //StartCoroutine(FlashYellow???());
    }

    public void Miss()
    {
        //StartCoroutine(FlashSomthingElse???());
    }

    public void Die()
    {
        dead = true;
        spriteRend.color = Color.grey;
    }

    public void Attack()
    {
        //jut forward a little
    }

    //maybe split color changes into it's own static script?
    IEnumerator FlashRed()
    {
        spriteRend.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        if (dead)
        {
            spriteRend.color = Color.grey;
        }
        else
        {
            spriteRend.color = Color.white;
        }
    }

    #endregion
}
