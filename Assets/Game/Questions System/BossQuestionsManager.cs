using System.Collections;
using System.Collections.Generic;
using Game.Enemy;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Questions_System
{
    public class BossQuestionsManager : MonoBehaviour 
    {
        private List<QuestionsData> _questions;
        private List<int> _availableQuestionIndex;
        public QuestionsDataList questionDataList;


        public GameObject answerButtonPrefab;
        public GameObject questionPanelPrefab;
        public GameObject questionTextPrefab;
        public GameObject timerTextPrefab;
        private Slider _timerSlider;
        private GameObject _questionPanelInstance;

        [SerializeField] private GameObject timerSliderPrefab;
        [SerializeField] private PlayerAttack playerAttack;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private List<BossHealth> bossHealthList = new List<BossHealth>();
        [SerializeField] private List<BossAttack> bossAttackList = new List<BossAttack>();



        private TextMeshProUGUI _timerText;
        private TextMeshProUGUI _questionText;

        private readonly int _timerDuration = 6;
        private int _currentQuestionIndex;
        private float _timer;
        private bool _isQuestionDisplayed = false;
     

        private void Start()
        {
            FindAllEnemyHealth();
            FindAllEnemyAttack();
            LoadQuestionsFromScriptableObject();
        }

        private void FindAllEnemyHealth()
        {
            BossHealth[] bossHealths = FindObjectsOfType<BossHealth>();
            bossHealthList.AddRange(bossHealths);
        }

        private void FindAllEnemyAttack()
        {
            BossAttack[] bossAttacks = FindObjectsOfType<BossAttack>();
            bossAttackList.AddRange(bossAttacks);
        }

        private void LoadQuestionsFromScriptableObject()
        {
            _questions = new List<QuestionsData>(questionDataList.questions);
            _availableQuestionIndex = new List<int>();

            for (int i = 0; i < _questions.Count; i++)
            {
                _availableQuestionIndex.Add(i);
            }
        }

        public void DisplayQuestionBoss()
        {
            if (_isQuestionDisplayed)
            {
                return;
            }

            if (_availableQuestionIndex.Count == 0)
            {
                Debug.Log("No more questions!");
                return;
            }

            int randomIndex = GetRandomQuestionIndex();
            SetCurrentQuestion(randomIndex);
            CreateQuestionPanel();
            DisplayQuestionText();
            SetupQuestionTextPosition();
            CreateAnswerButtons();
            ActivateQuestionPanel();
            StartCoroutine(StartTimer());
        }

        private void SetupQuestionTextPosition()
        {
            RectTransform questionTextRectTransform = _questionText.GetComponent<RectTransform>();
            questionTextRectTransform.pivot = new Vector2(0.5f, 1f);
            questionTextRectTransform.anchoredPosition = new Vector2(0f, 90f);
        }

        public void HideQuestion()
        {
            _isQuestionDisplayed = false;
            if (_questionPanelInstance != null)
            {
                Destroy(_questionPanelInstance);
                _questionPanelInstance = null;
            }
        }

        private int GetRandomQuestionIndex()
        {
            int randomIndex = Random.Range(0, _availableQuestionIndex.Count);
            int questionIndex = _availableQuestionIndex[randomIndex];
            _availableQuestionIndex.RemoveAt(randomIndex);
            return questionIndex;
        }

        private void SetCurrentQuestion(int questionIndex)
        {
            _currentQuestionIndex = questionIndex;
        }

        private void CreateQuestionPanel()
        {
            _questionPanelInstance = Instantiate(questionPanelPrefab);
            _timerText = Instantiate(timerTextPrefab, _questionPanelInstance.transform).GetComponent<TextMeshProUGUI>();
            _timerSlider = Instantiate(timerSliderPrefab, _questionPanelInstance.transform).GetComponent<Slider>();
            _timerSlider.minValue = 0;
            _timerSlider.maxValue = 1;
        }

        private void DisplayQuestionText()
        {
            _questionText = Instantiate(questionTextPrefab, _questionPanelInstance.transform)
                .GetComponent<TextMeshProUGUI>();
            _questionText.alignment = TextAlignmentOptions.Center;
            _questionText.text = _questions[_currentQuestionIndex].question;
        }


        private void CreateAnswerButtons()
        {
            Button[] answerButtons = _questionPanelInstance.GetComponentsInChildren<Button>();

            for (int i = 0; i < _questions[_currentQuestionIndex].answers.Length; i++)
            {
                if (i < answerButtons.Length)
                {
                    Button answerButton = answerButtons[i];
                    answerButton.gameObject.SetActive(true);

                    TMP_Text answerButtonText = answerButton.GetComponentInChildren<TMP_Text>();
                    answerButtonText.text = _questions[_currentQuestionIndex].answers[i];

                    int answerIndex = i;
                    answerButton.onClick.AddListener(() => CheckAnswer(answerIndex));
                }
            }
        }

        private void ActivateQuestionPanel()
        {
            _questionPanelInstance.SetActive(true);
            _isQuestionDisplayed = true;
        }

        private IEnumerator DelayedDisplayQuestion()
        {
            yield return new WaitForSeconds(2f);

            DisplayQuestionBoss();
        }

        private IEnumerator StartTimer()
        {
            _timer = _timerDuration;

            while (_timer > 0)

            {
                if (!_isQuestionDisplayed)
                    yield break;
                _timer -= Time.deltaTime;
                _timerText.text = "Time: " + Mathf.Round(_timer).ToString();
                float sliderValue = _timer / _timerDuration;
                _timerSlider.value = sliderValue;


                yield return null;
            }

            
            CheckAnswer(-1);
        }


        private void CheckAnswer(int answerIndex)
        {

            HideQuestion();
            QuestionsData currentQuestion = _questions[_currentQuestionIndex];

            if (currentQuestion.correctAnswerIndex == answerIndex)
            {
                if (playerAttack != null)
                {
                    playerAttack.Attack();
                }

              

                if (bossHealthList.Count > 0)
                {
                    foreach (BossHealth bossHealth in bossHealthList)
                    {
                        if (bossHealth != null && bossHealth.currentHealth > 0)
                        {
                            StartCoroutine(DelayedDisplayQuestion());
                        }
                    }

                    PlayerFeedback.Instance.ShowPositiveFeedback();
                    VibrationManager vibrationManager = new VibrationManager();
                    vibrationManager.Vibrate();

                   
                }
            }
            else
            {
                    if (bossAttackList.Count > 0)
                    {
                        foreach (BossAttack bossAttack in bossAttackList)
                        {
                            if (bossAttack != null)
                            {
                                bossAttack.Attack();
                            }
                        }
                    }


                    if (playerHealth != null && playerHealth.currentHealth > 0)
                    {
                            StartCoroutine(DelayedDisplayQuestion());
                    }

                    PlayerFeedback.Instance.ShowNegativeFeedback();

                   



                        _currentQuestionIndex++;
               
                        
            }



            


        }
    }
}

