using Invector.vCharacterController; // vThirdPersonController is a free asset we used from AssetStore
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData PlayerData;
    
    private GameObject playerReference;
    private GameObject enemyReference;

    private Vector3 savedPos;

    public PlayerData GetPlayerData()
    {
        return PlayerData;
    }

    public GameObject Player
    {
        get
        {
            return playerReference;

        }
        set
        {
            playerReference = value;

        }

    }

    public GameObject Enemy
    {
        get
        {
            return enemyReference;
        }
        set
        {
            enemyReference = value;
        }
    }


    // For Storing player position in scene while jumping between World Scene and Battle scene
    public void SaveData(vThirdPersonController playerController) // vThirdPersonController is a free asset we used from AssetStore
    {
        PlayerPrefs.SetFloat("x", playerController.transform.position.x);
        PlayerPrefs.SetFloat("y", playerController.transform.position.y);
        PlayerPrefs.SetFloat("z", playerController.transform.position.z);
    }

    public PlayerData LoadData()
    {
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");

        PlayerData playerData = new PlayerData()
        {
            Location = new Vector3(x, y, z)
        };

        return playerData;
    }

    public void EnemyToBattle()
    {
        playerReference = GameObject.FindWithTag("Enemy");
        playerReference.transform.position = new Vector3(0, 0.26f, 7.03f);
    }



}
