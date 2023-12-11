using Game.Enemy;
using Game.Interface;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAttack : MonoBehaviour, IAttackable
    {

        [SerializeField] private Animator animator;
        public int damageAmount = 30;

        private GameData gameData;

        public int DamageAmount => damageAmount;

        void Start()
        {
            gameData = FindObjectOfType<GameData>();
            gameData.LoadData();
            damageAmount = gameData.damageAmount;
            gameData.SaveData();


        }


        public void Attack()
        { 
            AttackEnemy();
        }





        private void AttackEnemy()
        {

            var transform1 = transform;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform1.position, transform1.localScale, 0f);

            foreach (Collider2D collider in colliders)
            {
                EnemyHealth attackable = collider.GetComponent<EnemyHealth>();
                if (attackable != null)
                {

                    animator.CrossFade("Attack", 0.2f);
                    attackable.TakeDamage(damageAmount);

                }

                BossHealth bossHealth = collider.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    animator.CrossFade("Attack", 0.2f);
                    bossHealth.TakeDamage(damageAmount);

                }






            }
        }

    }
}



