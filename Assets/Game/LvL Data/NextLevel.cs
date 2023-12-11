using Game.CoinsSystem;
using Game.UI_elements;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.LvL_Data
{
    public class NextLevel : MonoBehaviour
    {
        [SerializeField] private GameObject levelCompletionPanelPrefab;
        [SerializeField] private CoinCollector coinCollector;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ShowLevelCompletionPanel();
            }
        }

        private void ShowLevelCompletionPanel()
        {
            GameObject panelInstance = Instantiate(levelCompletionPanelPrefab);
            LevelCompletionPanel panel = panelInstance.GetComponent<LevelCompletionPanel>();

            int coinsEarned = coinCollector.GetCoinsEarned();
            panel.SetCoinsEarned(coinsEarned);
            panel.SetNextLevelButtonAction(GoToNextLevel);
            MenuManager.UnlockNextLevel();
            PlayerPrefs.Save();
        }
        

        private void GoToNextLevel()
        {
            CoinManager.Instance.SaveCoins();
            Time.timeScale = 1;
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneIndex = currentScene.buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
