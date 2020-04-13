using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes; // Not mine, its only a useful tool I recommend
using UnityEngine;

[CreateAssetMenu (fileName = "New PlayerData", menuName = "TABG/CreatePlayerData", order = 2)]

public class PlayerData : CreatureData
{


    public Vector3 Location { get; set; }

    public int currentHealth = 100;
    public int currentMagicAmount = 10;

    public int level;
    public int exp;
    public int expToNextLvl;

    [Header("Add exp")]
    public int expToAdd = 20;

    public string playerName;
    public SkillData skillData;
    public int BaseDmg;
    public int BaseMagicDmg;

    public PlayerData()
    {
        level = 1;
        exp = 0;
        expToNextLvl = 100;
    }

    [System.Serializable]
    public class ProfessionLevels
    {

        public int Level;
        public int currentXP;
    }

    // Add Exp and level up player, after every level adds more health, magic, also fills up player hp/mp after leveling & adds more to work for.
    #region
    public void AddExperience(int amount)
    {
        exp += amount;
        if (exp >= expToNextLvl)
        {
            level++;
            MaxHealth += 2 * MaxHealth / 10;
            MaxMagicPoints += 2 * MaxMagicPoints / 5;
           exp -= expToNextLvl;
            expToNextLvl += 2 * expToNextLvl / 10;
            currentHealth = MaxHealth;
            currentMagicAmount = MaxMagicPoints;
            BaseDmg += 2 * BaseDmg / 8;
            BaseMagicDmg += 2 * BaseMagicDmg / 8;


        }
    }
    #endregion


    // Only created to reset player stats
    [Button]
    public void ResetStats()
    {
        level = 1;
        MaxHealth = 100;
        currentHealth = 100;
        expToNextLvl = 100;
        MaxMagicPoints = 10;
        currentMagicAmount = 10;
        exp = 0;
        BaseDmg = 10;
        BaseMagicDmg = 10;
    }
   
  



}