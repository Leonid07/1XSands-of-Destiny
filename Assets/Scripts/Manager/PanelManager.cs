using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("Кнопки в главном меню")]
    public Button buttonReward;
    public Button buttonPersonal;
    public Button buttonOptions;

    [Header("Пенели из главного меню")]
    public GameObject panelReward;
    public GameObject panelPersonal;
    public GameObject panelOptions;

    [Header("кнопки закрытия окон")]
    public Button buttonRewardClose;
    public Button buttonPersonalClose;
    public Button buttonOptionsClose;

    [Space(20)]
    [Header("Кнопки улучшения персонажа")]
    public Button buttonPlayerUpdate;
    public Button buttonPlayerPanelClose;
    public Button buttonUpdate;

    [Header("Текстовые панели в улучшении")]
    public Text textBeforeUpdate;
    public Text textAfterUpdate;
    public Text textPriceOnButton;
    public Text textPlayerDamage;

    public int damage = 350;
    public int updateCost = 150;

    public int powerPlayer;
    public string idPowerPlayer = "power";

    public int levelPLayer = 1;
    public string idLevelPLayer = "level_";

    public int countFirstUpdate;
    public double growthFactor = 1.5;

    [Header("Панель улучшения персонажа")]
    public GameObject panelUpdate;

    [Header("Персонаж")]
    public GameObject buttonStart;

    public GameObject[] panelIsActive;

    public AnimationPAnel animRightPanel;
    public AnimationPAnel animLeftPanel;

    [Header("Анимация открытия уровня")]
    public AnimUnlockLevel animUnlockLevel;

    public static PanelManager InstancePanel { get; private set; }

    private void Awake()
    {
        if (InstancePanel != null && InstancePanel != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstancePanel = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        buttonReward.onClick.AddListener(() => ActivePanel(panelReward));
        buttonPersonal.onClick.AddListener(() => ActivePanel(panelPersonal));
        buttonOptions.onClick.AddListener(() => ActivePanel(panelOptions));

        buttonRewardClose.onClick.AddListener(() => ClosePanel(panelReward));
        buttonPersonalClose.onClick.AddListener(() => ClosePanel(panelPersonal));
        buttonOptionsClose.onClick.AddListener(() => ClosePanel(panelOptions));

        buttonPlayerUpdate.onClick.AddListener(() => { PanelUpdateActive(panelUpdate); SetValueForUpdate(); });
        buttonPlayerPanelClose.onClick.AddListener(() => PanelUpdateDisActive(panelUpdate));

        buttonUpdate.onClick.AddListener(() => {  UpdatePlayer(); SetValueForUpdate(); });
    }
    public void PanelUpdateActive(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void PanelUpdateDisActive(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ActivePanel(GameObject panel)
    {
        animRightPanel.StartAnimation(panel, true);
        animLeftPanel.StartAnimation(panel, true);
    }
    public void ClosePanel(GameObject panel)
    {
        animRightPanel.StartAnimation(panel, false);
        animLeftPanel.StartAnimation(panel, false);
    }

    public void SetActivePanel(bool lose = false)
    {
        if (lose == false)
        {
            for (int i = 0; i < panelIsActive.Length; i++)
            {

                animRightPanel.StartAnimation(panelIsActive[i], true);
                animLeftPanel.StartAnimation(panelIsActive[i], true);
            }
        }
        else
        {
            for (int i = 0; i < panelIsActive.Length; i++)
            {

                animRightPanel.StartAnimationUnLockLevel(panelIsActive[i]);
                animLeftPanel.StartAnimationUnLockLevel(panelIsActive[i]);
            }
        }
    }
    public void SetDisActivePanel()
    {
        for (int i = 0; i < panelIsActive.Length; i++)
        {
            animRightPanel.StartAnimation(panelIsActive[i], false);
            animLeftPanel.StartAnimation(panelIsActive[i], false);
        }
    }
    public void SetActiveButtonStart()
    {
        animRightPanel.StartAnimation(buttonStart, true);
        animLeftPanel.StartAnimation(buttonStart, true);
    }
    public void SetDisActiveButtonStart()
    {
        animRightPanel.StartAnimation(buttonStart, false);
        animLeftPanel.StartAnimation(buttonStart, false);
    }
    public void SetValueForUpdate()
    {
        if (powerPlayer != 0)
        {
            textBeforeUpdate.text = powerPlayer.ToString(); Debug.Log("PowerPlayer: " + powerPlayer.ToString());
        }
        //else
        //{
        //    textBeforeUpdate.text = "350"; Debug.Log("PowerPlayer is zero, setting default value to 350");
        //}
        levelPLayer++;
        int calculatedDamage = Convert.ToInt32(damage * Math.Pow(growthFactor, levelPLayer - 1));
        levelPLayer--;
        int calculatedPrice = Convert.ToInt32(updateCost * Math.Pow(growthFactor, levelPLayer - 1));
        textAfterUpdate.text = $"{calculatedDamage}";
        textPriceOnButton.text = $"{calculatedPrice}";
    }

    public void UpdatePlayer()
    {
        countFirstUpdate = Convert.ToInt32(updateCost * Math.Pow(growthFactor, levelPLayer - 1));
        //Debug.Log($"{countFirstUpdate}     {GameManager.InstanceGame.gold}");
        if (countFirstUpdate <= GameManager.InstanceGame.gold)
        {
            GameManager.InstanceGame.gold -= countFirstUpdate;
            powerPlayer = Convert.ToInt32(damage * Math.Pow(growthFactor, levelPLayer - 1));
            textPlayerDamage.text = powerPlayer.ToString();
            textAfterUpdate.text = $"{Convert.ToInt32(damage * Math.Pow(growthFactor, levelPLayer - 1))}";
            DataManager.InstanceData.SaveLevelPlayer();
            DataManager.InstanceData.SaveGold();
            DataManager.InstanceData.SavePowerPlayer();
        }
        levelPLayer++;
        if (countFirstUpdate >= GameManager.InstanceGame.gold)
        {
            levelPLayer--;
        }
    }
}