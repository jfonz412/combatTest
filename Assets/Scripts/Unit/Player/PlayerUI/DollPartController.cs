using System.Collections.Generic;
using UnityEngine;

public class DollPartController : MonoBehaviour {
    private Body myBody;
    private List<BodyPart> bodyParts;
    private Dictionary<string, DollPart> dollParts = new Dictionary<string, DollPart>();

    public GameObject doll;

    private DollPart head;
    private DollPart neck;
    private DollPart chest;
    private DollPart abdomin;
    private DollPart leftArm;
    private DollPart rightArm;
    private DollPart leftHand;
    private DollPart rightHand;
    private DollPart leftLeg;
    private DollPart rightLeg;
    private DollPart leftFoot;
    private DollPart rightFoot;

    // Use this for initialization
    void Start () {
        GetDollParts();
        bodyParts = GetComponent<Body>().GetBodyParts(); //purposefully grabbing the ref rather than a copy
        AssignDollParts();
    }

    private void GetDollParts()
    {
        dollParts.Add("head", doll.transform.GetChild(0).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("neck", doll.transform.GetChild(1).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("chest", doll.transform.GetChild(2).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("abdomin", doll.transform.GetChild(3).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("left arm", doll.transform.GetChild(4).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("left hand", doll.transform.GetChild(5).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("right arm", doll.transform.GetChild(6).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("right hand", doll.transform.GetChild(7).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("left leg", doll.transform.GetChild(8).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("left foot", doll.transform.GetChild(9).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("right leg", doll.transform.GetChild(10).GetChild(0).GetComponent<DollPart>());
        dollParts.Add("right foot", doll.transform.GetChild(11).GetChild(0).GetComponent<DollPart>());
    }

    //match the bp name to assigned dollpart name
    //assumes bodyparts will be in this order
    private void AssignDollParts()
    {
        for(int i = 0; i < bodyParts.Count; i++)
        {
            bodyParts[i].dollPart = dollParts[bodyParts[i].name]; 
            //Debug.Log("assigning " + bodyParts[i].name);
        }
    }
}
