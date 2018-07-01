using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DollPart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private InjuryLog log;

    public BodyParts.Parts dollPart;
    private Image myImage;
    private List<string> injuries = new List<string>();


    private void Awake()
    {
        myImage = transform.parent.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered " + dollPart);
        log.DisplayInjuryList(injuries);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exit " + dollPart);
        log.ClearInjuryList();
    }

    public void ChangeColor(Color color)
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

    public void LogInjury(string injury)
    {
        injuries.Add(injury);
    }

    public List<string> GetInjuryLog()
    {
        return injuries;
    }

    public void LoadInjuryLog(List<string> savedInjuries)
    {
        injuries = savedInjuries;
    }
}
