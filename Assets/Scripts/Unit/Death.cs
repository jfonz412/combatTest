using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {

    [SerializeField]
    private GameObject deathItem;

    public void Die()
    {
        StartCoroutine(DeathProcess()); 
    }

    private IEnumerator DeathProcess()
    {
        StopAllCombat();
        DropDeathItem();

        yield return new WaitForSeconds(6f); //extend death animation before destroy

        gameObject.SetActive(false);
    }

    private void DropDeathItem()
    {
        if (deathItem != null)
        {
            Instantiate(deathItem, transform.position, Quaternion.identity);
        }
    }

    public virtual void StopAllCombat()
    {
        //stop this unit
        GetComponent<UnitController>().StopMoving();
        GetComponent<AttackController>().EngageTarget(false); //should this go first?
        GetComponent<UnitAnimController>().Die();

        //stop myAttacker
        //myAttacker.GetComponent<AttackController>().EngageTarget(false);
    }
}
