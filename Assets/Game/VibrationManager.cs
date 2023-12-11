using UnityEngine;

namespace Game
{
    public class VibrationManager 
    {
        public void Vibrate()
        {
            if (SystemInfo.supportsVibration)
            {
                Handheld.Vibrate();
            }
        }
    }
}
