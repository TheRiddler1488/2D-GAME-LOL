using Game.Player;
using Game.UI_elements;
using UnityEngine;

namespace Game.CoinsSystem
{
    public class CoinCollector : MonoBehaviour
    {
        public LevelCompletionPanel levelCompletionPanel;

        private int _coinsEarned;

        public void CompleteLevel()
        {
            int earnedCoins = GetCoinsEarned();
            _coinsEarned = earnedCoins;
            CoinManager.Instance.SetCoins(CoinManager.Instance.GetCoins() + earnedCoins);
            levelCompletionPanel.SetCoinsEarned(earnedCoins);
            levelCompletionPanel.ShowLevelCompletionPanel();
            MenuManager.Instance.UpdateCoinsText();
            MenuManager.UnlockNextLevel();
        }

        public int GetCoinsEarned()
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth.currentHealth >= playerHealth.maxHealth)
                {
                    return 20;
                }
                else
                {
                    return Random.Range(5, 12);
                }
            }

            return 0;
        }
    }
}