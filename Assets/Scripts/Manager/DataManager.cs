using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public Map[] levels;

    public Map mapNextLevel;

    public static DataManager InstanceData { get; private set; }

    private void Awake()
    {
        if (InstanceData != null && InstanceData != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetIndexLevel();
        LoadLevel();
        LoadGold();
        LoadPowerPlayer();
        LoadLevelPlayer();
    }

    public void SetIndexLevel()
    {
        int count = 1;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].indexLevel = count;
            count++;
        }
    }

    public void SaveLevel()
    {
        for (int i =0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt(levels[i].idLevel, levels[i].isLoad);
            PlayerPrefs.Save();
        }
    }
    public void LoadLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (PlayerPrefs.HasKey(levels[i].idLevel))
            {
                levels[i].isLoad = PlayerPrefs.GetInt(levels[i].idLevel);
                levels[i].CheckLevel();
            }
        }
    }
    public void SaveGold()
    {
        GameManager.InstanceGame.ApplyGold();
        PlayerPrefs.SetInt(GameManager.InstanceGame.idGold, GameManager.InstanceGame.gold);
        PlayerPrefs.Save();
    }
    public void LoadGold()
    {
        if (PlayerPrefs.HasKey(GameManager.InstanceGame.idGold))
        {
            GameManager.InstanceGame.gold = PlayerPrefs.GetInt(GameManager.InstanceGame.idGold);
            GameManager.InstanceGame.ApplyGold();
        }
    }

    public void SavePowerPlayer()
    {
        PlayerPrefs.SetInt(PanelManager.InstancePanel.idPowerPlayer, PanelManager.InstancePanel.powerPlayer);
        PlayerPrefs.Save();
    }
    public void LoadPowerPlayer()
    {
        if (PlayerPrefs.HasKey(PanelManager.InstancePanel.idPowerPlayer))
        {
            PanelManager.InstancePanel.powerPlayer = PlayerPrefs.GetInt(PanelManager.InstancePanel.idPowerPlayer);
            PanelManager.InstancePanel.textPlayerDamage.text = PlayerPrefs.GetInt(PanelManager.InstancePanel.idPowerPlayer).ToString();
        }
    }
    public void SaveLevelPlayer()
    {
        PlayerPrefs.SetInt(PanelManager.InstancePanel.idLevelPLayer, PanelManager.InstancePanel.levelPLayer);
        PlayerPrefs.Save();
    }
    public void LoadLevelPlayer()
    {
        if (PlayerPrefs.HasKey(PanelManager.InstancePanel.idLevelPLayer))
        {
            PanelManager.InstancePanel.levelPLayer = PlayerPrefs.GetInt(PanelManager.InstancePanel.idLevelPLayer);
        }
    }
}
