using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour {

    [SerializeField]
    private GameObject deathItem;
    private UnitAnimController anim;

    protected void Start()
    {
        anim = GetComponent<UnitAnimController>();
    }

    public void Die()
    {
        StartCoroutine(DeathProcess()); 
    }

    private IEnumerator DeathProcess()
    {
        DropDeathItem();
        anim.Die();
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
}
