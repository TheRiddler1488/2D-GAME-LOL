using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public static GameData Instance { get; private set; }

    public int maxHealth { get; set; } = 100; 
    public int damageAmount { get; set; } = 30;
  

   
    private const string MaxHealthKey = "MaxHealth";
    private const string DamageAmountKey = "DamageAmount";

    private void Start()
    {
        Application.targetFrameRate = 60;
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("DamageAmount", damageAmount);
      
        PlayerPrefs.Save();
    }
      public void LoadData()
      {
          maxHealth = PlayerPrefs.GetInt(MaxHealthKey,100);
          damageAmount = PlayerPrefs.GetInt(DamageAmountKey,30);
     

      }
    
}


