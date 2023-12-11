using System.Collections;
using System.Collections.Generic;
using Game.CoinsSystem;
using Game.Player;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AttributeUpgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI upgradePointsText;
    private GameData gameData;



    private int _availableUpgradePoints;

 
   private const string UpgradePointsKey = "CharacterUpgradePoints";
  

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        gameData.LoadData();
        LoadAttributesAndPoints();
        UpdateUI();
        
    }

    private void UpdateUI()
    {

        healthText.text = "Health: " + GameData.Instance.maxHealth.ToString();
        damageText.text = "Damage: " + GameData.Instance.damageAmount.ToString();
        upgradePointsText.text = "Upgrade Points: " + _availableUpgradePoints.ToString();
       
      


     
        SaveAttributesAndPoints();
    }


    
    public void UpgradeHealth()
    {
      
        if (_availableUpgradePoints > 0)
        {
            gameData.maxHealth += 10;
            _availableUpgradePoints--;
            gameData.SaveData();
            UpdateUI();
            SaveAttributesAndPoints();
            Debug.Log("Я не работаю");
        }
    }
  

    public void UpgradeDamage()
    {
        if (_availableUpgradePoints > 0)
        {
            gameData.damageAmount += 10;
            _availableUpgradePoints--;
            gameData.SaveData();
            UpdateUI();
            SaveAttributesAndPoints();
        }
    }

    private void SaveUpgradePoints()
    {
        PlayerPrefs.SetInt(UpgradePointsKey, _availableUpgradePoints);
        PlayerPrefs.Save();
    }



        private void LoadAttributesAndPoints()
     {

         _availableUpgradePoints = PlayerPrefs.GetInt(UpgradePointsKey, 0);
     }

       private void SaveAttributesAndPoints()
    {

           PlayerPrefs.SetInt(UpgradePointsKey, _availableUpgradePoints);
           PlayerPrefs.Save();

     }
 }


