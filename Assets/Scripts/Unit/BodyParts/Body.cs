using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    //base class so bodypart controller can access the attatched body via GetComponent()
    public virtual List<BodyPart> GetBodyParts()
    {
        Debug.LogError("Should not touch base method");
        return new List<BodyPart>();
    }
}
