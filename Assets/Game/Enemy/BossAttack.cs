using Game.Interface;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class BossAttack : MonoBehaviour , IAttackable
    {
        [SerializeField] private Animator animator;
        
        public int damageAmount = 30;
        public int DamageAmount => damageAmount;
      
        public void Attack()
        { 
            AttackPlayer();
        }
        private void AttackPlayer()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);

            foreach (Collider2D collider in colliders)
            {
                PlayerHealth attackable = collider.GetComponent<PlayerHealth>();
                if (attackable != null)
                {
                    animator.CrossFade("AttackBoss",0.9f);
                    attackable.TakeDamage(damageAmount);    
                    
                }
            }
        }

    }
}
