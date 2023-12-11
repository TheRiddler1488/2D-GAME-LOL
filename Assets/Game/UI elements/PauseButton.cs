using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI_elements
{
    public class PauseButton : MonoBehaviour
    {
        public void OnPauseButtonClicked()
        {
            PauseGameEndPanel.Instance.TogglePause();
        }
    }
}
