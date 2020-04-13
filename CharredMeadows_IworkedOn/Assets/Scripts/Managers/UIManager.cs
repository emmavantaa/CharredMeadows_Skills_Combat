using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject BaseHUD;


    [SerializeField] private GameObject BattleHUD;
    [SerializeField] private GameObject PlayerSkillsButtonsVisibility;
    [SerializeField] private GameObject InvSelectionHUD;
    [SerializeField] private GameObject MagicSelectionHUD;
    [SerializeField] private GameObject PlayerLostBattle;
    [SerializeField] private GameObject PlayerWonBattle;
    [SerializeField] private GameObject CheatsPanel;

    [Header("In Battle")]
    [SerializeField] private TextMeshProUGUI HealthAmountText;
    [SerializeField] private TextMeshProUGUI MagicAmountText;
    [SerializeField] private TextMeshProUGUI EnemyHealthText;
    [SerializeField] private TextMeshProUGUI DisplayPlayerName;
    [SerializeField] private TextMeshProUGUI DisplayPlayerLevel;
    [SerializeField] private TextMeshProUGUI expGainText;
    [SerializeField] private TextMeshProUGUI[] skillLvlInfoText;
    [SerializeField] private TMP_Text _startText;

    [SerializeField] private GameObject closeConObj;

    [Header("Data")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ElementData[] elementData;
    [SerializeField] private SkillData[] skillDatas;
    [SerializeField] private ActivateCombat[] activateCombat;

    [SerializeField] private TextMeshProUGUI[] expGainSkillsText;

    [SerializeField] private GameObject[] skillOnPages;
    [SerializeField] private GameObject[] skillsToActivate;

    private PlayerController playerController;
    private int pages;

    private bool CheatsOpen;



    void Start()
    {
        ToggleHUD(true);
        ToggleBattleHUD(false);

    }


    public void Update()
    {
        ActiveSkills();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheatsOpen = !CheatsOpen;
            CheatsPanel.SetActive(CheatsOpen);
        }
    }



    public void ToggleHUD(bool value)
    {
        BaseHUD.SetActive(value);
    }




    // Battle related UI scripts
    #region

    public void ToggleBattleHUD(bool value)
    {
        BattleHUD.SetActive(value);
    }

    public void CloseConv(bool value)
    {
        _startText.text = "Welcome " + playerData.playerName + " to Charred Meadows!";
        closeConObj.SetActive(value);
        
    }

    public void RefreshBattleUI()
    {
        HealthAmountText.text = "" + playerData.currentHealth + "/" + playerData.MaxHealth;
        MagicAmountText.text = "" + playerData.currentMagicAmount + "/" + playerData.MaxMagicPoints;

        if (playerData.currentHealth <= 0)
        {
            playerData.currentHealth = 0;
        }
        if (playerData.currentMagicAmount <= 0)
        {
            playerData.currentMagicAmount = 0;
        }

        if (playerData.currentHealth >= playerData.MaxHealth)
        {
            playerData.currentHealth = playerData.MaxHealth;
        }

        if (playerData.currentMagicAmount >= playerData.MaxMagicPoints)
        {
            playerData.currentMagicAmount = playerData.MaxMagicPoints;
        }



        for (var i = 0; i < activateCombat.Length; i++)
        {
            EnemyHealthText.text = "" + activateCombat[i].data.currentHealth + "/" + activateCombat[i].data.MaxHealth;
            expGainText.text = "Exp: " + playerData.exp + " / " + playerData.expToNextLvl + "+" + activateCombat[i].data.ExpGainOnKill;

            if (activateCombat[i].data.currentHealth <= 0)
            {
                activateCombat[i].data.currentHealth = 0;
            
            }
            break;
        }

        DisplayPlayerName.text = "Rebecca";
        DisplayPlayerLevel.text = playerData.level.ToString();

        expGainSkillsText[0].text = "Fire: " + " Level " + elementData[0].level + "\n" + elementData[0].expSkill + " / " + elementData[0].expToNextLvl;
        expGainSkillsText[1].text = "Water: " + " Level " + elementData[1].level + "\n" + elementData[1].expSkill + " / " + elementData[1].expToNextLvl;
        expGainSkillsText[2].text = "Wind: " + " Level " + elementData[2].level + "\n" + elementData[2].expSkill + " / " + elementData[2].expToNextLvl;
        expGainSkillsText[3].text = "Earth: " + " Level " + elementData[3].level + "\n" + elementData[3].expSkill + " / " + elementData[3].expToNextLvl;

        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmount[i].text = items[i]._currentStack.ToString();
        }

        for (int i = 0; i < skillLvlInfoText.Length; i++)
        {
            skillLvlInfoText[i].text = "Lvl" + skillDatas[i].level;
        }
    }

    public void PlayerSkillsInBattle(bool value)
    {
        PlayerSkillsButtonsVisibility.SetActive(value);
    }

    public void MagicToggleSelectionHUD(bool value)
    {
        MagicSelectionHUD.SetActive(value);
    }

    public void InvToggleSelectionHUD(bool value)
    {
        InvSelectionHUD.SetActive(value);
    }

    public void YouDiedScene(bool value)
    {
        PlayerLostBattle.SetActive(value);
    }

    public void YouWonScene(bool value)
    {
        PlayerWonBattle.SetActive(value);
    }


    public void ButtonPressed()
    {
        MagicalSkills();
        pages++;
    }

    public void MagicalSkills()
    {

        Debug.Log("What page" + pages);
        if (pages == 2)
        {
            pages = 0;
        }
        if (pages == 0)
        {
            skillOnPages[1].SetActive(true);
            skillOnPages[0].SetActive(false);
        }
        else if (pages == 1)
        {
            skillOnPages[1].SetActive(false);
            skillOnPages[0].SetActive(true);

        }
    }

    #endregion

    // SKill activating
    #region
    public void ActiveSkills()
    {
        if (elementData[0].level >= 2)
        {
            skillsToActivate[0].SetActive(true);
        }

        if (elementData[1].level >= 2)
        {
            skillsToActivate[1].SetActive(true);
        }

        if (elementData[2].level >= 2)
        {
            skillsToActivate[2].SetActive(true);
        }

        if (elementData[3].level >= 2)
        {
            skillsToActivate[3].SetActive(true);
        }

    }
    #endregion


   
}
