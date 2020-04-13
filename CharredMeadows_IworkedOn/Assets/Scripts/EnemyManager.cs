using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ActivateCombat[] activateCombat;
    [SerializeField] private ActivateCombat oneActive;
    [SerializeField] private int EnemySkillAmount = 4;

    [Tooltip("Info about Enemy skills")]
    [TextArea]
    public string SkillInfo;


    [Header("Camera control in battle")]
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    public void SkillsToUse()
    {
       
        int skillGen = Random.Range(0, EnemySkillAmount);

        switch (skillGen)
        {
            case 0:
                BasicAttack();
                break;

            case 1:
                EnemySkills();
                break;

            case 2:
                EnemyRunAway();
                break;

            case 3:
                EnemyPowerMove();
                break;
        }
    }

    public void BasicAttack()
    {

        for (var i = 0; i < activateCombat.Length; i++)
        {
            //cameraInBattle.LookAtPlayer();
            
            Instantiate(activateCombat[i].skill1.effectParticle, playerController.transform.position, Quaternion.identity);
            playerController.OnDamageTaken(activateCombat[i].skill1.PhysicalHitDmg);
            Debug.Log("Basic attack on player");
            break;
        }
    }

    public void EnemyRunAway()
    {
        for (var i = 0; i < activateCombat.Length; i++)
        {
            if (activateCombat[i].data.currentHealth <= 0.1)
            {
                Debug.Log("Enemy tries to run");
            }
            else
            {
                BasicAttack();
            }
            break;
        }
    }

    public void EnemySkills()
    {

        for (int i = 0; i < activateCombat.Length;i +=1)
        {
            playerController.OnDamageTaken(activateCombat[i].skill2.MagicalHitDmg);
            Instantiate(activateCombat[i].skill2.effectParticle, playerController.transform.position, Quaternion.identity);
            break;
        }
    }

    public void EnemyPowerMove()
    {
        for (var i = 0; i < activateCombat.Length; i++)
        {
            playerController.OnDamageTaken(activateCombat[i].skill3.MagicalHitDmg);
            Instantiate(activateCombat[i].skill3.effectParticle, playerController.transform.position, Quaternion.identity);
            break;
        }
    }

}
