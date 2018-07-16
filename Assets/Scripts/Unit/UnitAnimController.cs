using System.Collections;
using UnityEngine;

public class UnitAnimController : MonoBehaviour {

    SpriteRenderer spriteRend;
    Animator anim;
    Brain myBrain;
    public bool faceRightAtStart;
    bool dead;

    IEnumerator onTheGround;
    private Brain.State[] downedStates = new Brain.State[]
    {
        Brain.State.CantBreathe, //should only keep unit down when suffocating
        Brain.State.Dead,
        Brain.State.Downed,
        Brain.State.Shock,
        Brain.State.Unconscious
    };

    // Use this for initialization
    void Start ()
    {
        myBrain = GetComponent<Brain>();
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

    #region Main shell methods

    public void Walking(bool isWalking)
    {
        anim.SetBool("isWalking", isWalking);
    }

    public void TakeDamage(Color shadeOfRed)
    {
        StartCoroutine(Flash(shadeOfRed));
    }

    public void Die()
    {
        dead = true;
        Color color = Color.grey;
        color.a = color.a * 0.75f;
        spriteRend.color = color;
        FallOver();
    }

    //eventually pass equippedWeapon.type from AttackController.cs in order to get different motions
    //for spells, ranged, etc
    //also maybe pass direction to this method in order to get more precise looking attack movements
    public void Attack()
    {
        anim.SetTrigger("strike");
    }

    //disabled because I am coloring sprite for injuries, leaving just in case i need them later
    public void Block()
    {
        //StartCoroutine(Flash(Color.blue));
    }

    public void Parry()
    {
        //StartCoroutine(Flash(Color.yellow));
    }

    public void Dodge()
    {
        //StartCoroutine(Flash(Color.green));
    }

    #endregion

    #region Status Shell methods
    public void Rocked()
    {
        StartCoroutine(TempAnimation(Color.magenta, Brain.State.Rocked));
    }

    public void Vomit()
    {
        StartCoroutine(TempAnimation(Color.green, Brain.State.Vomitting));
    }

    public void CantBreath()
    {
        StartCoroutine(TempAnimation(Color.cyan, Brain.State.CantBreathe));
    }

    public void KnockedOut()
    {
        StartCoroutine(TempAnimation(Color.magenta, Brain.State.Unconscious));
        FallOver();
    }

    public void Shock()
    {
        StartCoroutine(TempAnimation(Color.red, Brain.State.Shock));
        FallOver();
    }

    public void Suffocation()
    {
        StartCoroutine(TempAnimation(Color.red, Brain.State.CantBreathe));
        FallOver();
    }

    public void FallOver()
    {
        //should make sure we have no overlapping coroutines here
        if(onTheGround != null)
        {
            StopCoroutine(onTheGround);
            onTheGround = IsDown();
        }
        else
        {
            onTheGround = IsDown();
        }
        StartCoroutine(onTheGround);
    }
    #endregion

    #region Coroutines

    IEnumerator IsDown()
    {
        anim.SetBool("isDown", true);

        while(myBrain.ActiveStates(downedStates))
        {
            yield return null;
        }
        anim.SetBool("isDown", false);
        onTheGround = null;
    }

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

    //to be used for things like vomiting or being knocked out
    IEnumerator TempAnimation(Color c, Brain.State brainState)
    {
        while (myBrain.ActiveState(brainState))
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
        if (dead)
        {
            Die(); //might be repeating dead anim but it doesn't matter because unit will deactivate soon
        }
        else
        {
            spriteRend.color = Color.white; //may overwrite other colors?
        }
    }

    #endregion
}
