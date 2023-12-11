using Game.Interface;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyAttack : MonoBehaviour,IAttackable
    {
        public int damageAmount = 10;
        [SerializeField] private Animator animator;

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
                    animator.CrossFade("AttackEnemy",0.2f);
                    attackable.TakeDamage(damageAmount);
                    Debug.Log("Ударил");
                }
            }
        }

    }
}
