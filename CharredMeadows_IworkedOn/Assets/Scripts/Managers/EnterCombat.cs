using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnterCombat : OnEnterInteractable 
{
    [Header("Battle Data")]
    [SerializeField] private string BattleSceneName;

    // Update is called once per frame
    public override void OnEnterInteract()
    {
        base.OnEnterInteract();
        LevelManager.Instance.LoadLevel(BattleSceneName);
        Debug.Log("Did something enter");
    }
}
