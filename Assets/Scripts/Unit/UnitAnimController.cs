using System.Collections;
using UnityEngine;

public class UnitAnimController : MonoBehaviour {

    SpriteRenderer spriteRend;
    Animator anim;
    public bool faceRightAtStart;
    bool dead;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
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
        //Debug.Log(Mathf.Sign(relativePos.x));

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

    public void Walking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    public void TakeDamage(Color shadeOfRed)
    {
        StartCoroutine(Flash(shadeOfRed));
    }

    public void Block()
    {
        StartCoroutine(Flash(Color.blue));
    }

    public void Parry()
    {
        StartCoroutine(Flash(Color.yellow));
    }

    public void Dodge()
    {
        StartCoroutine(Flash(Color.green));
    }

    public void CritHit()
    {
        StartCoroutine(Flash(Color.cyan));
    }

    public void Die()
    {
        dead = true;
        spriteRend.color = Color.grey;
    }

    //eventually pass equippedWeapon.type from AttackController.cs in order to get different motions
    //for spells, ranged, etc
    //also maybe pass direction to this method in order to get more precise looking attack movements
    public void Attack()
    {
        anim.SetTrigger("strike");
    }

    //maybe split color changes into it's own static script?
    IEnumerator Flash(Color color)
    {
        spriteRend.color = color;
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
