using Game.UI_elements;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.LvL_Data
{
    public class LvlButton : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private int levelIndex;

        public void LoadLevel()
        {
            if (MenuManager.IsLevelUnlocked(levelIndex))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Level is locked! Complete previous levels to unlock it.");
            }
        }
    }
}
