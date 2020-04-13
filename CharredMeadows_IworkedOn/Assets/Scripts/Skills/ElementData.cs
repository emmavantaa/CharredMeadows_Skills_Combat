using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "New skill/SkillElement", order = 2)]
public class ElementData : ScriptableObject
{
    public ElementalType Type;
    public int Damage;
    public enum ElementalType
    {
        Fire,
        Water,
        Earth,
        Wind,

    }


    [Header("Skill Leveling")]
    public int level;
    public int expSkill;

    public int expToNextLvl;
    public int maxLevel = 5;
    string maxLevelHit = "Max level";

    public SkillData [] skillData;
    public GameObject skillIsActive;


    public void AddExperience(int amount)
    {
        //Bonuses[1].expElement += amount;

        if (level != maxLevel)
        {
            expSkill += amount;
            if (expSkill >= expToNextLvl && level <= maxLevel)
            {
                level++;
                expSkill -= expToNextLvl;
                expToNextLvl += 2 * expToNextLvl / 10;

                for (var i = 0; i < skillData.Length; i++)
                {
                    var item = skillData[i].MagicalHitDmg +=2;
                }

                if (level == maxLevel)
                {
                    // Dont have this yet
                }

            }

        }
        else
        {
            maxLevelHit = expSkill.ToString();
        }

    }


    public void newSkill(bool value)
    {
        skillIsActive.SetActive(true);
    }

    // Reset Element
    [Button]
    public void ResetStats()
    {
        level = 1;
        expSkill = 0;
        expToNextLvl = 20;
        Damage = 2;
    }
}
