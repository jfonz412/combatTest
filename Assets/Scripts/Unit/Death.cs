using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {

    [SerializeField]
    private GameObject deathItem;

    public void Die(Transform myAttacker)
    {
        StartCoroutine(DeathProcess(myAttacker)); 
    }

    private IEnumerator DeathProcess(Transform myAttacker)
    {
        StopAllCombat(myAttacker);
        DropDeathItem();

        yield return new WaitForSeconds(6f); //extend death animation before destroy
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void DropDeathItem()
    {
        if (deathItem != null)
        {
            Instantiate(deathItem, transform.position, Quaternion.identity);
        }
    }

    public virtual void StopAllCombat(Transform myAttacker)
    {
        //stop this unit
        GetComponent<UnitController>().StopMoving();
        GetComponent<AttackController>().EngageTarget(false); //should this go first?
        GetComponent<UnitAnimController>().Die();

        //stop myAttacker
        myAttacker.GetComponent<AttackController>().EngageTarget(false);
    }
}
