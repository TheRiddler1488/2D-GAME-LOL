using Game.Interface;
using Game.UI_elements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private Slider sliderHp;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject deathPanelPrefab;
        [SerializeField] private float deathPositionY = -6f;
        
        public int MaxHealth { get; set; }
        public  int CurrentHealth { get; set; }
        public int maxHealth = 100;
        public int currentHealth;
        private bool _isDead = false;
        private GameData gameData;
       
        

        


        void Start()
        {
            
            gameData = FindObjectOfType<GameData>();
            gameData.LoadData();
            currentHealth = gameData.maxHealth;
            UpdateHealthUI();
            


        }

        
        void Update()
        {
            if (currentHealth <= 0 && !_isDead)
            {
                Die();
            }
            if (!_isDead)
            {
                
                if (transform.position.y <= deathPositionY)
                {
                    
                    Die();
                }

               
            }
           
        }
       
        


       

        public void TakeDamage(int damageAmount)
        {
           currentHealth -= damageAmount;
           UpdateHealthUI();
           if (animator != null) 
           {
                   animator.CrossFade("Hurt",0.2f);
                   
           }

        }

        private void UpdateHealthUI()
        {
            if(sliderHp != null)
            {
                sliderHp.value = (float)currentHealth / gameData.maxHealth;
            
            }

           
        }
       


       

        private void Die()
        {
            _isDead = true;
            Debug.Log("Death");
            Destroy(gameObject);
            ShowDeathPanel();
        }
        private void ShowDeathPanel()
        {
            if (deathPanelPrefab != null)
            {
                GameObject deathPanelInstance = Instantiate(deathPanelPrefab);
                PauseGameEndPanel deathGameEndPanelManager = deathPanelInstance.GetComponent<PauseGameEndPanel>();
                if (deathGameEndPanelManager != null)
                {
                    deathGameEndPanelManager.SetPlayerHealth(this);
                }
            }
        }
    }
}
