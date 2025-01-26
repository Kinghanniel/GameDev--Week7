using UnityEngine;

namespace GameDevWithMarco.RandomStuff
{
    public class Coin : MonoBehaviour
    {
        //give each coin a value
        [SerializeField] int coinValue;


        // get coin value
        public int CoinValue
        {
            get
            {
                return coinValue;
            }
        }
    }
}
