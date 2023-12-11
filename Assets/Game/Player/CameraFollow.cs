using UnityEngine;

namespace Game.Player
{
    public class CameraFollow : MonoBehaviour
    {
    
    
   
    
        public Transform target; 
        public Vector3 offset; 

        private void LateUpdate()
        {
            if (target != null)
            {
                transform.position = target.position + offset;
            }
        }
    }

    

}
