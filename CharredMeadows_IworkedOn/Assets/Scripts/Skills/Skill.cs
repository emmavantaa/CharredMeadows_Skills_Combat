using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class Skill : MonoBehaviour
{
   [SerializeField] private GameObject effect;

    //[SerializeField] EnemyController enemyController;
    [SerializeField] private ActivateCombat[] activateCombat;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private SkillData skillData;
    [SerializeField] private PlayerData playerData;

    public virtual SkillData GetSkillData(SkillData skillData)
    {
        return skillData;
    }

    public void BasicAttack(SkillData skillData)
    {
        for (var i = 0; i < activateCombat.Length; i++)
        {
            activateCombat[i].OnDamageTaken(skillData.PhysicalHitDmg + playerData.BaseDmg * 1);
            Instantiate(skillData.effectParticle, BattleManager.Instance.EnemySpawn.transform.position, Quaternion.identity);
            playerData.currentMagicAmount += 2;
        }
        return;
    }

    public void CriticalUsed(SkillData skillData)
    {
        for (var i = 0; i < activateCombat.Length; i++)
        {
            activateCombat[i].OnDamageTaken(skillData.PhysicalHitDmg + playerData.BaseDmg * 2);
            Instantiate(skillData.effectParticle, BattleManager.Instance.EnemySpawn.transform.position, Quaternion.identity);
        }
        return;
    }


    // On kaikissa skilleissä jotka menee enemyyn
    public void FirstLevelMagic(SkillData skillData) 
    {
        if (playerData.currentMagicAmount >= 10) 
        {
            for (var i = 0; i < activateCombat.Length; i++)
            {
                playerController.OnMagicUsed(skillData.manaCost);
                activateCombat[i].OnDamageTaken(skillData.MagicalHitDmg + playerData.BaseMagicDmg);
                Instantiate(skillData.effectParticle, BattleManager.Instance.EnemySpawn.transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("No magic left"); 
        }
        return;
    }

    public void Heal(SkillData skillData)
    {
        if (playerData.currentMagicAmount >= 10)
        {
            playerController.OnMagicUsed(skillData.manaCost);
            playerData.currentHealth += 40 + playerData.BaseMagicDmg;
            Instantiate(skillData.effectParticle, playerController.transform.position, Quaternion.identity);
        }
        else
        {
           
            Debug.Log("No magic left");
        }

    }
}

