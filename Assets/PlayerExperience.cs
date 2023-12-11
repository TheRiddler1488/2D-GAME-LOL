using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public int maxLevel = 10;
    public int expForNextLevel = 100;
    public int startExp = 0;

    private int CurrentLevel { get; set; }
    private int CurrentExp { get; set; }
    private int AvailableUpgradePoints { get; set; }

    public Slider levelSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradePointsText;
    private const string LevelKey = "CharacterLevel";
    private const string ExpKey = "CharacterExp";
    private const string UpgradePointsKey = "CharacterUpgradePoints";

    private void Start()
    {
        CurrentLevel = 1;
        CurrentExp = startExp;
        AvailableUpgradePoints = 0;
        LoadCharacterData();
        UpdateLevelUI();
    }
    private void LoadCharacterData()
    {
        CurrentLevel = PlayerPrefs.GetInt(LevelKey, 1);
        CurrentExp = PlayerPrefs.GetInt(ExpKey, startExp);
        AvailableUpgradePoints = PlayerPrefs.GetInt(UpgradePointsKey, 0);
    }
    private void SaveCharacterData()
    {
        PlayerPrefs.SetInt(LevelKey, CurrentLevel);
        PlayerPrefs.SetInt(ExpKey, CurrentExp);
        PlayerPrefs.SetInt(UpgradePointsKey, AvailableUpgradePoints);
    }

    public void AddExperience(int amount)
    {
        CurrentExp += amount;

        while (CurrentExp >= expForNextLevel && CurrentLevel < maxLevel)
        {
            LevelUp();
        }

        UpdateLevelUI();
        SaveCharacterData();
    }

    private void LevelUp()
    {
        CurrentExp -= expForNextLevel;
        CurrentLevel++;
        AvailableUpgradePoints++;

        
        expForNextLevel += 50;

    
    }

    private void UpdateLevelUI()
    {
        levelSlider.value = (float)CurrentExp / expForNextLevel;
        levelText.text = "Level: " + CurrentLevel.ToString();
        upgradePointsText.text = "Upgrade Points: " + AvailableUpgradePoints.ToString();

    }
}
