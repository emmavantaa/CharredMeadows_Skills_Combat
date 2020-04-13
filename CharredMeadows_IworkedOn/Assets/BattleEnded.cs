using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnded : MonoBehaviour
{
   public EnemyData enemyData;

    private void FixedUpdate()
    {
       if (enemyData.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        } 
    }
}
