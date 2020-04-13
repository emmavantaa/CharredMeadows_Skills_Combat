using UnityEngine;
using NaughtyAttributes;
using System.Collections;


[CreateAssetMenu(fileName = "New Skill Data", menuName = "New skill/Create Skill", order = 1)]
public class SkillData : ScriptableObject
{

    [Header("SkillSetUp")]
    public string SkillName;
    public SoundEffect Sound;
    public GameObject effectParticle;

    [Header("Physical Damage")]
    public int PhysicalHitDmg;

    [Range(0, 100)]
    public float CriticalChance;

    [Header("Magic Damage")]
    public int MagicalHitDmg;

    public int manaCost;
    public ElementData element;

    [Header("Skill Leveling")]
    public int level;
    public int expSkill;

    public int requiredLvl;
    public int maxLevel = 5;

    [SerializeField] private int expToNextLvl;
    [SerializeField] private string maxLevelHit = "Max level";



    public void AddExperience(int amount)
    {
        if (level != maxLevel)
        {
            expSkill += amount;
            if (expSkill >= expToNextLvl && level <= maxLevel)
            {
                     level++;
                     expSkill -= expToNextLvl;
                      expToNextLvl += 2 * expToNextLvl / 10;
                      MagicalHitDmg += 2;

                if (level == maxLevel)
                {
                    // What happens when max level?
                }
            }

        }
        else
            {
                 maxLevelHit = expSkill.ToString();
            }

    }

    public void Level2Skill(bool value)
    {
        if (element.level <= 2)
        {
            value = true;
        }
    }

    public void SkillCombination()
    {
        // Elemental combination (Don't have this in use yet..
    }


    // Reset skill
    [Button]
    public void ResetStats()
    {
        level = 1;
        expSkill = 0;
        expToNextLvl = 10;
        MagicalHitDmg = 20;

    }
}
