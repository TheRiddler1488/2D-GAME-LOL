using Game.Interface;
using Game.LvL_Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Enemy
{
    public class EnemyHealth : MonoBehaviour,IHealth
    {
       [SerializeField] private Animator animator;
       [SerializeField] private Slider sliderHp;
       [SerializeField] private Transform enemyTransform;
       public GameObject expPrefab;
     
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int maxHealth = 20;
        public int currentHealth;
        
        
        void Start()
        {
            MaxHealth =maxHealth;
            CurrentHealth=currentHealth;
            currentHealth = maxHealth;
            UpdateHealthUI();

        }
        private void LateUpdate()
        {
            if (enemyTransform != null)
            {
               
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(enemyTransform.position);
                sliderHp.transform.position = screenPosition;
            }
        }

        void Update()
        {
            if (currentHealth <= 0)
            {
                Die();
            }
           
        }


       

        public void TakeDamage(int damageAmount)
        {
            
            currentHealth -= damageAmount;
            UpdateHealthUI();
          
            if (animator != null) 
            {
                animator.CrossFade("HitEnemy",0.2f, 0);
                   
            }
            
        }
        private void UpdateHealthUI()
        {
            if(sliderHp != null)
            {
                sliderHp.value = (float)currentHealth / maxHealth;
            }
        }


        private void Die()
        {
            Destroy(gameObject);
            DropExperience();
            
        }
        private void DropExperience()
        {
            if (expPrefab != null)
            {
                int expAmount = Random.Range(1, 3);
                for (int i = 0; i < expAmount; i++)
                {
                    GameObject expInstance = Instantiate(expPrefab, transform.position, Quaternion.identity);
                    ExpPickUp expPickup = expInstance.GetComponent<ExpPickUp>();
                }

              
             
            }
        }
    }
}
