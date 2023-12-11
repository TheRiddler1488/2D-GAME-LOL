using Game.Interface;
using Game.LvL_Player;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Enemy
{
    public class BossHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider sliderHp;
        [SerializeField] private Transform bossTransform;
        public GameObject expPrefab;
        public int expAmountOnDeath = 400;
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int maxHealth = 120;
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
            if (bossTransform != null)
            {
               
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(bossTransform.position);
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
                animator.CrossFade("BossHurt",0.2f, 0);
                   
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
                GameObject expInstance = Instantiate(expPrefab, transform.position, Quaternion.identity);
                ExpPickUp expPickup = expInstance.GetComponent<ExpPickUp>();
                if (expPickup != null)
                { 
                    expPickup.expAmount = expAmountOnDeath;
                }
            }
        }
    }
}

