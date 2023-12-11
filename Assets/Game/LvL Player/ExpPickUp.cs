using UnityEngine;

namespace Game.LvL_Player
{
    public class ExpPickUp : MonoBehaviour
    {
        public int expAmount = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerExperience playerExperience = other.GetComponent<PlayerExperience>();
                if (playerExperience != null)
                {
                    Random.Range(1, 3);
                    playerExperience.AddExperience(expAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}
