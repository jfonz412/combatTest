using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DollPart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private InjuryLog log;

    private Dictionary<Item.AttackType, string[]> injuryStrings = new Dictionary<Item.AttackType, string[]>();

    private Image myImage;
    private List<string> injuries;

    private void Awake()
    {
        myImage = transform.parent.GetComponent<Image>();       
    }

    public void Initialize(BodyPart.PartInfo info)
    {
        LoadInjuryStrings();
        Color color = DeterminePartColor(info.severityLevel);
        ChangeColor(color);
        injuries = info.injuryLog; //should hold a ref so list will update itself
    }

    public void SeverityColor(int severity)
    {
        Color color = DeterminePartColor(severity);
        ChangeColor(color);
    }

    public string LogInjury(int severity, Item.AttackType damageType)
    {
        //Debug.Log("Damage type " + damageType);
        string line = injuryStrings[damageType][severity];
        injuries.Add(line);
        return line;
    }

    private void ChangeColor(Color color)
    {
        if(myImage == null)
        {
            myImage = transform.parent.GetComponent<Image>(); //needed if this object is deactivated when this is called
        }

        if (myImage == null)
        {
            Debug.LogError("still null!"); //needed if this object is deactivated when this is called
        }

        myImage.color = color;
    }

    private Color DeterminePartColor(int severityLevel)
    {
        if (severityLevel == 0 || severityLevel == 1)
        {
            return Color.green;
        }
        else if (severityLevel == 2)
        {
            return Color.yellow;
        }
        else if (severityLevel == 3)
        {
            return new Color32(255, 140, 0, 255); //orange
        }
        else if (severityLevel == 4)
        {
            return Color.red;
        }
        else if (severityLevel == 5)
        {
            Color transparent = Color.red;
            transparent.a = 0.5f;
            return transparent;
        }
        else
        {
            Debug.LogError("Shouldn't reach this!");
            return Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        log.DisplayInjuryList(injuries);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        log.ClearInjuryList();
    }

#region Injury strings
    private void LoadInjuryStrings()
    {
        injuryStrings[Item.AttackType.BluntImpact] = bluntImpact;
        injuryStrings[Item.AttackType.Stab] = stabbed;
        injuryStrings[Item.AttackType.Bite] = bitten;
        injuryStrings[Item.AttackType.Claw] = clawed;
    }

    private string[] stabbed = new string[]
    {
        "poked",
        "broken flesh",
        "punctured",
        "stabbed",
        "gored",
        "skewered"
    };

    private string[] bluntImpact = new string[]
    {
        "light bruise",
        "bruised",
        "heavily bruised",
        "internal damage",
        "crushed",
        "pulverized"
    };

    private string[] clawed = new string[]
{
        "scratched",
        "slashed",
        "gouged",
        "flesh hanging",
        "partially stripped",
        "completely shredded"
};

    private string[] bitten = new string[]
    {
        "nipped",
        "bitten",
        "chomped",
        "lacerated",
        "badly mangled",
        "torn apart"
    };
    #endregion
}
