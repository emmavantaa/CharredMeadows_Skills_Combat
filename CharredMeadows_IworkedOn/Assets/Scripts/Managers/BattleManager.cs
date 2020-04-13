using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


    public class BattleManager : Singleton<BattleManager>
    {
        public Transform EnemySpawn;
        public Transform PlayerSpawn;
        public EnemyData[] enemyData;
        bool playerWon = false;
   
        [SerializeField] private float TimeBetweenTurns = 1;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] PlayerController playerController;
        [SerializeField] PlayerData playerData;
        [SerializeField] private Skill skill;
        [SerializeField] private ActivateCombat[] activateCombat;  // Brings enemy battle to battle

        [SerializeField] private GameObject bag;


    private BattleState state;


        public BattleState State 
        {
            get { return state; }

            set
            {
                state = value;

                switch (State)
                {
                    case BattleState.PlayerTurn:

                    DoPlayerTurn();
                        break;
                    case BattleState.EnemyTurn:
                        DoEnemyTurn();
                        break;
                    case BattleState.Win:
                        Debug.Log("You Won");
                        break;
                    case BattleState.Lose:
                        PlayerLost();
                        break;
                    default:
                        break;
                }
            }
        }


        private void FixedUpdate()
        {
            UIManager.Instance.RefreshBattleUI();
        }

        public void InitializeBattle()
        {
            bag.SetActive(false);
            State = BattleState.Initializing;
            UIManager.Instance.Inv(false);
            UIManager.Instance.BuildUI(false);
            UIManager.Instance.ToggleBattleHUD(true);
            UIManager.Instance.PlayerSkillsInBattle(true);

            for (var i = 0; i < activateCombat.Length; i++)
            {
                activateCombat[activateCombat[i].enemyNumber].data.currentHealth = activateCombat[i].data.MaxHealth;
            }

            GameObject playerGO = Instantiate(playerPrefab, PlayerSpawn);
            playerController = playerGO.GetComponent<PlayerController>();
            playerController.transform.position = new Vector3(PlayerSpawn.position.x, PlayerSpawn.position.y, PlayerSpawn.position.z);
            state = BattleState.PlayerTurn;
        }

        public void DoPlayerTurn()
        {
            UIManager.Instance.PlayerSkillsInBattle(true);
            UIManager.Instance.RefreshBattleUI();
        }


        public void DoPlayerSkill()
        {
            UIManager.Instance.PlayerSkillsInBattle(false);
            UIManager.Instance.MagicToggleSelectionHUD(false);

            for (int i = 0; i < activateCombat.Length; i++)
            {

                if (activateCombat[i].data.currentHealth <= 0)
                {
                    DestroyImmediate(activateCombat[i].gameObject);
                    bag.SetActive(true);
                    playerWon = true;
                    Invoke("PlayerWon", 3.5f);
                }

                if (activateCombat[i].data.currentHealth >= 0)
                    StartCoroutine(SwitchTurn());
                    break;

            }

        }

        void DoEnemyTurn()
        {

            EnemyManager.Instance.SkillsToUse();

            if (playerData.currentHealth <= 0)
            {
                Invoke("PlayerLost", 1f);
            }

            StartCoroutine(SwitchTurn());
        }

        void PlayerWon()
        {

            playerData.currentHealth += 10;
            playerData.currentMagicAmount += 10;
            for (var i = 0; i < activateCombat.Length; i++)
            {

                playerData.AddExperience(enemyData[i].ExpGainOnKill);
                break;
            }

            bag.SetActive(false);
            UIManager.Instance.ToggleBattleHUD(false);
            UIManager.Instance.YouWonScene(true);

        }

       void PlayerLost()
        {
            UIManager.Instance.YouDiedScene(true);
            SceneManager.UnloadSceneAsync(LevelManager.Instance.BattleSceneRef);
            LevelManager.Instance.WorldRoot.SetActive(true);

            Destroy(playerController.gameObject);
            UIManager.Instance.YouWonScene(false);
            Invoke("RespawnPlayer", 2.5f);
            UIManager.Instance.Inv(true);
            UIManager.Instance.BuildUI(true);
            UIManager.Instance.ToggleBattleHUD(false);


        }


        IEnumerator SwitchTurn()
        {
            yield return new WaitForSeconds(TimeBetweenTurns);

            switch (State)
            {
                case BattleState.PlayerTurn:
                    State = BattleState.EnemyTurn;
                    break;
                case BattleState.EnemyTurn:
                    State = BattleState.PlayerTurn;
                    break;
            }
        }

        public void BacktoGame()
        {
            Destroy(playerController.gameObject);
            UIManager.Instance.YouWonScene(false);
            LevelManager.Instance.WorldRoot.SetActive(true);
            UIManager.Instance.Inv(true);
            UIManager.Instance.BuildUI(true);
            SceneManager.UnloadSceneAsync(LevelManager.Instance.BattleSceneRef);
        }

        public void RespawnPlayer()
        {
            UIManager.Instance.YouDiedScene(false);
        }

    }


    public enum BattleState
    {
        Initializing,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

