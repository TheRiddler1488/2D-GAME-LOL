using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Game.UI_elements
{
    public class LevelCompletionPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private Button button;

        private int _coinsEarned;

        public void SetCoinsEarned(int amount)
        {
            _coinsEarned = amount;
            coinsText.text = "Coins Earned: " + _coinsEarned;
            Time.timeScale = 0;
        }

        public void SetNextLevelButtonAction(Action action)
        {
            
            button.onClick.AddListener(() =>
            {
                
                MenuManager.Instance.AddCoinsEarned(_coinsEarned);
                PlayerPrefs.SetInt(MenuManager.CoinsEarnedKey, _coinsEarned);
                PlayerPrefs.Save();
                action.Invoke();
            }); 
        }
        private void SaveCoinsEarned()
        {
            PlayerPrefs.SetInt("CoinsEarned", _coinsEarned);
            PlayerPrefs.Save();
        }

        public void ShowLevelCompletionPanel()
        {
            gameObject.SetActive(true);
        }
    
    }
}
