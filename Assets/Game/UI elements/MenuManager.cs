using System;
using Game.CoinsSystem;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Game.UI_elements
{
    public class MenuManager : MonoBehaviour
    {
        
        [SerializeField] private GameObject lvlPanel;
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private Button[] levelButtons;
        


        private const string LevelProgressKey = "LevelProgress";
        public const string CoinsEarnedKey = "CoinsEarned";
        public static MenuManager Instance;
       

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
   

        private void Start()
        {
            CoinManager.Instance.LoadCoins();
            UpdateCoinsText();
            UnlockLevel(1);
            
        }

        public void UpdateCoinsText()
        {
            int coins = CoinManager.Instance.GetCoins();
            coinsText.text = "Coins: " + coins.ToString();
           

        }
      
        private void OnDestroy()
        {
            CoinManager.Instance.SaveCoins();
        }

        public void ShowUpgradePanel()
        {
            
            upgradePanel.SetActive(true);
            
        }

        public void HideUpgradePanel()
        {
            if (upgradePanel != null)
            {
                upgradePanel.SetActive(false);
            }
           
        }

        public void ShowLevelPanel()
        {
            GameObject lvlPanelInstance = Instantiate(lvlPanel);
            int maxUnlockedLevel = PlayerPrefs.GetInt(LevelProgressKey, 0);
            Button[] levelButtonsInPanel = lvlPanelInstance.GetComponentsInChildren<Button>();

            for (int i = 0; i < levelButtons.Length; i++)
            {
                Image buttonImage = levelButtonsInPanel[i].GetComponent<Image>();
                if (i < maxUnlockedLevel)
                {

                    buttonImage.color = Color.green;
                }
                else
                {

                    buttonImage.color = Color.red;
                }


            }
           
        }

        public static bool IsLevelUnlocked(int levelIndex)
        {
            int maxUnlockedLevel = PlayerPrefs.GetInt(LevelProgressKey, 0);
            return levelIndex <= maxUnlockedLevel;
        }

        public static void UnlockNextLevel()
        {
            int maxUnlockedLevel = PlayerPrefs.GetInt(LevelProgressKey, 0);
            int nextLevel = maxUnlockedLevel + 1;
            PlayerPrefs.SetInt(LevelProgressKey, nextLevel);
            PlayerPrefs.Save();
            
         
        }

        public static void UnlockLevel(int levelIndex)
        {
            int maxUnlockedLevel = PlayerPrefs.GetInt(LevelProgressKey, 0);
            if (levelIndex > maxUnlockedLevel)
            {
                PlayerPrefs.SetInt(LevelProgressKey, levelIndex);
                PlayerPrefs.Save();
             
            }
        }

        public void AddCoinsEarned(int coinsEarned)
        {
            int totalCoins = CoinManager.Instance.GetCoins() + coinsEarned;
            CoinManager.Instance.SetCoins(totalCoins);
        }

        public int GetCoinsEarned()
        {
            return PlayerPrefs.GetInt(CoinsEarnedKey, 0);
        }

        public void ClearCoinsEarned()
        {
            PlayerPrefs.DeleteKey(CoinsEarnedKey);
        }


        public void QuitGame()
        {
            Application.Quit();
        }

    }
}


