using Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI_elements
{
    public class PauseGameEndPanel : MonoBehaviour
    {


        public static PauseGameEndPanel Instance { get; private set; }
        private PlayerHealth _playerHealth;

        public void SetPlayerHealth(PlayerHealth health)
        {
            _playerHealth = health;
        }


        public GameObject pausePanel;

        private bool _isPaused = false;

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

        public void TogglePause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = 0f; 
                pausePanel.SetActive(true); 
            }
            else
            {
                Time.timeScale = 1f; 
                pausePanel.SetActive(false);
            }
        }

        public void ResumeGame()
        {
            TogglePause();
        }
        public void OnRetryButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}


