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
    private Dictionary<Weapon.WeaponType, int> weaponSkills;
    private string newLine = System.Environment.NewLine;

    // Use this for initialization
    void Start ()
    {
        combatSkills = ScriptToolbox.GetInstance().GetPlayerManager().player.GetComponent<CombatSkills>();
        combatSkills.onSkillGained += UpdateCombatSkills;
        skillWindow.text = "";
    }
	
    public void Toggle(bool toggleOn)
    {
        if (toggleOn)
        {
            skillPanel.SetActive(true);
            skillPlate.SetActive(true);
            UpdateCombatSkills();
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
            skillWindow.text += skill.Key + ": " + skill.Value + newLine;
        }

        skillWindow.text += newLine;

        foreach (KeyValuePair<Weapon.WeaponType, int> skill in weaponSkills)
        {
            skillWindow.text += skill.Key + ": " + skill.Value + newLine;
        }
    }

    private void UpdateCombatSkills()
    {
        skills = combatSkills.GetCombatLevels();
        weaponSkills = combatSkills.GetWeaponLevels();

        skillWindow.text = ""; //clear previous stat info

        DisplayCombatSkills(); //display with newly updated skills
    }
}
