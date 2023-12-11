using UnityEngine;

namespace Game.CoinsSystem  
{
    public class CoinManager 
    {
        private const string CoinsKey = "Coins";
        private  int _coins;
        private static CoinManager _instance;
        
      
        public static CoinManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CoinManager();
                    _instance._coins = PlayerPrefs.GetInt(CoinsKey, 0);
                }
                return _instance;
            }
        }
        private CoinManager()
        {
            LoadCoins();
        }
       

        public  int GetCoins()
        { 
            
            return _coins;
            
        }

        public  void SetCoins(int value)
         {
            _coins = value;
            SaveCoins();
       }
        public void LoadCoins()
        {
            _coins = PlayerPrefs.GetInt(CoinsKey, 0);
        }

        public void SaveCoins()
        {
            PlayerPrefs.SetInt(CoinsKey, _coins);
            PlayerPrefs.Save();
        }
    }
}
