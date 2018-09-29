using UnityEngine;

//this state will eventually grab the scene exit they chose, and will probably have to transition into idle? 
//depends on how state saving goes
public class UnitRoutedState : State {
    Vector3 safelyOutOfRange;

    protected override void Init()
    {
        base.Init();
        safelyOutOfRange = new Vector3(100f, 100f, transform.position.z);
        //doesn't transition into anything right now
    }


    protected override void OnStateEnter()
    {
        base.Init();
        transform.position = safelyOutOfRange;
    }


    protected override void OnStateExit()
    {
        base.Init();
        //do nothing for now
    }

}
