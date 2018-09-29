using UnityEngine;

public class NodeHarvestedState : State {
    public GameObject itemDrop;

    protected override void Init()
    {
        base.Init();
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        DropAndDestroy();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
    }

    private void DropAndDestroy()
    {
        if (itemDrop != null)
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        GetComponent<Harvestable>().isHarvested = true;
        //set back to idle or leave dead?
    }

}
