using System.Collections;
using UnityEngine;

public class UnitAnimController : MonoBehaviour {

    SpriteRenderer spriteRend;
    Animator anim;
    public bool faceRightAtStart;
    bool dead;

    bool inShock;
    bool suffocating;
    IEnumerator shockAnimation;
    IEnumerator suffocationAnimation;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        spriteRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (faceRightAtStart)
        {
            FaceDirection(transform.position, new Vector2(99999f,0f));
        }
        //LoadSavedStatuses()?
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
#region Public shell methods

    public void Walking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    public void TakeDamage(Color shadeOfRed)
    {
        StartCoroutine(Flash(shadeOfRed));
    }

    //could get rid of these and leave this to floating text...
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


    public void Rocked(int duration)
    {
        StartCoroutine(TempAnimation(Color.magenta, duration));
    }

    public void Vomit(int duration)
    {
        StartCoroutine(TempAnimation(Color.green, duration));
    }

    public void FallOver(int duration)
    {
        //anim.SetBool("isDown", true);
        //wait
        //anim.SetBool("isDown", false);
        //StartCoroutine(TempAnimation(Color.magenta, duration));
    }

    public void CantBreath(int duration)
    {
        StartCoroutine(TempAnimation(Color.cyan, duration));
    }

    public void KnockedOut(int duration)
    {
        StartCoroutine(TempAnimation(Color.yellow, duration));
        FallOver(duration);
    }

    #endregion

    //maybe split color changes into it's own static script?
    IEnumerator Flash(Color color)
    {
        spriteRend.color = color;

        yield return new WaitForSeconds(0.5f);

        if (dead)
        {
            Die();
        }
        else
        {
            spriteRend.color = Color.white;
        }
    }

    public void Die()
    {
        dead = true;
        Color color = Color.grey;
        color.a = color.a * 0.75f;
        spriteRend.color = color;
    }

    //eventually pass equippedWeapon.type from AttackController.cs in order to get different motions
    //for spells, ranged, etc
    //also maybe pass direction to this method in order to get more precise looking attack movements
    public void Attack()
    {
        anim.SetTrigger("strike");
    }

    public void Shock(bool toggle)
    {
        inShock = toggle;
        if (inShock && shockAnimation == null)
        {
            shockAnimation = ShockAnimation();
            StartCoroutine(shockAnimation);
        }
        else
        {
            if(shockAnimation != null) //just in case but this shouldnt be null
                StopCoroutine(shockAnimation);
            spriteRend.color = Color.white;
        }
    }

    IEnumerator ShockAnimation()
    {
        Color c = Color.red; //make specific shade of red

        while (inShock)
        {
            while (c.a > 0.5)
            {
                c.a -= 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.1f);
            }
            while (c.a < 1)
            {
                c.a += 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.1f);
            }
        }
        shockAnimation = null; //if we've exited the while loop we are no longer in shock and clear this ienum
    }


    public void Suffocation(bool toggle)
    {
        suffocating = toggle;
        if (suffocating && suffocationAnimation == null)
        {
            suffocationAnimation = SuffocationAnimation();
            StartCoroutine(suffocationAnimation);
        }
        else
        {
            if (suffocationAnimation != null) //just in case but this shouldnt be null
                StopCoroutine(suffocationAnimation);
            spriteRend.color = Color.white;
        }
    }

    IEnumerator SuffocationAnimation()
    {
        Color c = Color.blue; //make specific shade of blue

        while (suffocating)
        {
            while (c.a > 0.5)
            {
                c.a -= 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.05f);
            }
            while (c.a < 1)
            {
                c.a += 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
        suffocationAnimation = null; //if we've exited the while loop we are no longer in suffocation and clear this ienum
    }


    //to be used for things like vomiting or being knocked out
    IEnumerator TempAnimation(Color c, int duration)
    {
        while (duration > 0)
        {
            while (c.a > 0.5)
            {
                c.a -= 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.05f);
            }
            while (c.a < 1)
            {
                c.a += 0.1f;
                spriteRend.color = c;
                yield return new WaitForSeconds(0.05f);
            }
            duration--;
        }
        //spriteRend.color = Color.white; //may overwrite other colors?
    }
}
