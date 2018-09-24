using System.Collections.Generic;
using UnityEngine;

public class DollPartController : MonoBehaviour {
    private Body myBody;
    private List<BodyPart> bodyParts;
    private Dictionary<string, DollPart> dollParts = new Dictionary<string, DollPart>();

    public DollPart head;
    public DollPart neck;
    public DollPart chest;
    public DollPart abdomin;
    public DollPart leftArm;
    public DollPart rightArm;
    public DollPart leftHand;
    public DollPart rightHand;
    public DollPart leftLeg;
    public DollPart rightLeg;
    public DollPart leftFoot;
    public DollPart rightFoot;

    // Use this for initialization
    void Start () {
        SetDollPartNames();
        bodyParts = GetComponent<Body>().GetBodyParts(); //purposefully grabbing the ref rather than a copy
        AssignDollParts();
    }

    private void SetDollPartNames()
    {
        dollParts.Add("head", head);
        dollParts.Add("neck", neck);
        dollParts.Add("chest", chest);
        dollParts.Add("abdomin", abdomin);
        dollParts.Add("left arm", leftArm);
        dollParts.Add("left hand", leftHand);
        dollParts.Add("right arm", rightArm);
        dollParts.Add("right hand", rightHand);
        dollParts.Add("left leg", leftLeg);
        dollParts.Add("left foot", leftFoot);
        dollParts.Add("right leg", rightLeg);
        dollParts.Add("right foot", rightFoot);
    }
    
    private void AssignDollParts()
    {
        for(int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].dollPart = dollParts[bodyParts[i].name];
            //Debug.Log("assigning " + bodyParts[i].name);
        }
    }
}
