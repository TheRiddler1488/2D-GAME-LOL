using System.Collections;
using System.Collections.Generic;
using Game.UI_elements;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LvlDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Start()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (levelText != null)
        {
            levelText.text = "Location level: " + sceneName;
        }
    }
}
