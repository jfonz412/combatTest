using System.Collections.Generic;
using UnityEngine;

public class HealthDoll : MonoBehaviour {
    private DollPart[] dollParts;
    private HumanoidBody playerBody;
    Dictionary<BodyParts.Parts, float> playerHealth;

    // Use this for initialization
    void Start () {
        playerBody = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<HumanoidBody>();
        CollectDollParts();
        LoadSavedBodyPartHealth();
        playerBody.onDamageTaken += DamageBodyPart;
	}
	
	// Update is called once per frame
	private void DamageBodyPart(BodyParts.Parts bodyPart, float damage)
    {
        for (int i = 0; i < dollParts.Length; i++)
        {
            if(dollParts[i].dollPart == bodyPart)
            {
                playerHealth[bodyPart] -= damage;
                Color color = DeterminePartColor(playerHealth[bodyPart]);
                dollParts[i].ChangeColor(color);
                return;
            }
        }
        Debug.LogError("BodyPart not found!!! Should not exit the for loop!");
    }

    private void CollectDollParts()
    {
        dollParts = transform.GetComponentsInChildren<DollPart>();
    }

    private void LoadSavedBodyPartHealth()
    {
        playerHealth = playerBody.GetBodyPartHealth();
        Debug.Log("Loading saved bodypart health to doll");
        for (int i = 0; i < dollParts.Length; i++)
        {
            Color color = DeterminePartColor(playerHealth[dollParts[i].dollPart]);
            dollParts[i].ChangeColor(color);
        }
    }

    private Color DeterminePartColor(float healthOfPart)
    {
        if (healthOfPart >= 75)
        {
            return Color.green;
        }
        else if (healthOfPart >= 50)
        {
            return Color.yellow;
        }
        else if (healthOfPart >= 25)
        {
            return new Color(0.2F, 0.3F, 0.4F); //orange
        }
        else
        {
            return Color.red;
        }
    }
}
