using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCombat : OnEnterInteractable, IKillable
{
    [Header("Battle Data")]
    [SerializeField] private string BattleSceneName;
   
    [SerializeField] SoundEffect SoundEffectOnInteract;

    public int enemyNumber;

    public SkillData skill1, skill2, skill3;
    public EnemyData data;
   public PlayerData playerData;
    public bool isActive;

    

    [SerializeField] private GameObject[] enemyPrefab;

    public void Start()
    {
        data.currentHealth = data.MaxHealth;
        data.enemyID = enemyNumber;
    }


    // Update is called once per frame
    public override void OnEnterInteract()
    {


        Destroy(gameObject, 1f);
        data.currentHealth = data.MaxHealth;
        base.OnEnterInteract();
        isActive = true;

        LevelManager.Instance.BattleScene(BattleSceneName);
        enemyPrefab[1].SetActive(false);
        for (var i = 0; i < enemyPrefab.Length; i++)
        {
            Instantiate(enemyPrefab[i], BattleManager.Instance.EnemySpawn).GetComponent<ActivateCombat>();
            enemyPrefab[i].transform.position = new Vector3(BattleManager.Instance.EnemySpawn.position.x, BattleManager.Instance.EnemySpawn.position.y, BattleManager.Instance.EnemySpawn.position.z);
            
            break;
        }
        Debug.Log("Did something enter");

        
    }

    public void OnDeath()
    {

       
            for (var i = 0; i < enemyPrefab.Length; i++)
            {

            if (data.currentHealth <= 0)
            {
                
                gameObject.SetActive(false);

            }

            break;
        }

    }

    public bool OnDamageTaken(int Dmg)
    {
        data.currentHealth -= Dmg;
        if (data.currentHealth <= 0)
        {
            OnDeath();
            return true;
        }

        else
            return false;
    }

    public bool OnMagicUsed(int magicCost)
    {
        if (playerData.currentMagicAmount <= 0)
        {
            return true;
        }

        else
            return false;
    }
}
