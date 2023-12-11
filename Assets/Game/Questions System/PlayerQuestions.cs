using System;
using Game.Questions_System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Questions_System
{



     public class PlayerQuestions : MonoBehaviour
     {
         public QuestionsManager questionsManager;
         public BossQuestionsManager bossManager;
         private bool _hasTriggered;



         private void OnTriggerEnter2D(Collider2D other)
         {
             if (other.CompareTag("Enemy") && !_hasTriggered)
             {
                 
                     _hasTriggered = true;
                     questionsManager.DisplayQuestion();
                     
              
                
             }

             if (other.CompareTag("Boss") && !_hasTriggered)
             {
                 _hasTriggered = true;
                 bossManager.DisplayQuestionBoss();
             }
             
         }

         private void OnTriggerExit2D(Collider2D other)
         {
             _hasTriggered = false;
         }
     }
     

}

