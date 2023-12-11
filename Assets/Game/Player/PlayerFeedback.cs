using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Player
{
    public class PlayerFeedback : MonoBehaviour
    {
        public TextMeshProUGUI feedbackText;
        public float displayDuration = 1.0f;

        private string[] positiveMessages = { "Great!", "Very Good!","Well Done!","Amazing!","Good job!" };
        private string[] negativeMessages = { "Pure genius!", "Bad!" , ":(","Nerd","Ha ha ha"};
        public static PlayerFeedback Instance; 
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
       
            HideFeedbackText();
        }

        public void ShowPositiveFeedback()
        {
        
            string message = positiveMessages[Random.Range(0, positiveMessages.Length)];

        
            feedbackText.text = message;
            feedbackText.color = Color.green;

       
            StartCoroutine(HideFeedbackAfterDelay());
        }

        public void ShowNegativeFeedback()
        {
       
            string message = negativeMessages[Random.Range(0, negativeMessages.Length)];

        
            feedbackText.text = message;
            feedbackText.color = Color.red;

        
            StartCoroutine(HideFeedbackAfterDelay());
        }

        private IEnumerator HideFeedbackAfterDelay()
        {
        
            ShowFeedbackText();

        
            yield return new WaitForSeconds(displayDuration);

        
            HideFeedbackText();
        }

        private void ShowFeedbackText()
        {
            feedbackText.gameObject.SetActive(true);
        }

        private void HideFeedbackText()
        {
            feedbackText.gameObject.SetActive(false);
        }
    }
}