using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {
    [SerializeField]
    private Text skillWindow;
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private GameObject skillPlate;

    private CombatSkills combatSkills;


    private Dictionary<CombatSkills.CombatSkill, int> skills;

	// Use this for initialization
	void Start ()
    {
        combatSkills = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<CombatSkills>();
        combatSkills.onSkillGained += GetCombatSkills;
        skillWindow.text = "";

    }
	
    public void Toggle(bool toggleOn)
    {
        if (toggleOn)
        {
            skillPanel.SetActive(true);
            skillPlate.SetActive(true);
            GetCombatSkills();
            DisplayCombatSkills();
        }
        else
        {
            skillPanel.SetActive(false);
            skillPlate.SetActive(false);
            skillWindow.text = "";
        }
    }

    private void DisplayCombatSkills()
    {
        foreach (KeyValuePair<CombatSkills.CombatSkill, int> skill in skills)
        {
            skillWindow.text += skill.Key + ": " + skill.Value + System.Environment.NewLine;
        }
    }

    private void GetCombatSkills()
    {
        skills = combatSkills.GetCombatLevels();
    }
}
