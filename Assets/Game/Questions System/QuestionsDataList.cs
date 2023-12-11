using UnityEngine;

namespace Game.Questions_System
{

    [CreateAssetMenu(fileName = "New Question Data List", menuName = "Question System/Question Data List")]
    public class QuestionsDataList : ScriptableObject
    {
        public QuestionsData[] questions;
    }
}
