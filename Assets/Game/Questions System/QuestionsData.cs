using UnityEngine;

namespace Game.Questions_System
{
    [CreateAssetMenu(fileName = "New Question Data", menuName = "Question System/Question Data")]
    public class QuestionsData : ScriptableObject
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
    }
}